using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels;
using System.Data.Entity;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Concurrent;
using OnStoreModels.Checkout;
using OnStoreModels.Common;

namespace OnStoreBusinessLayer
{
    public class StoreManager
    {
        private static object storeCacheLock = new object();
        private static ConcurrentDictionary<int, List<ItemStoreMap>> store = new ConcurrentDictionary<int, List<ItemStoreMap>>();

        public static List<ItemStoreMap> GetAvailableItemStoreMaps(int storeId)
        {
            List<ItemStoreMap> cachedStoreItems = GetCachedStoreItems(storeId);
            if (cachedStoreItems == null || cachedStoreItems.Count <= 0)
            {
                return cachedStoreItems;
            }

            List<ItemStoreMap> availableStoreItems = FilterStoreItemsByAvailability(cachedStoreItems);
            if (availableStoreItems == null || availableStoreItems.Count <= 0)
            {
                return availableStoreItems;
            }

            return availableStoreItems;
        }

        public static List<StoreItem> GetAvailableStoreItems(int storeId)
        {
            List<StoreItem> availableStoreItems = new List<StoreItem>();

            List<ItemStoreMap> isms = GetAvailableItemStoreMaps(storeId);
            if (isms == null)
            {
                return null;
            }

            foreach (ItemStoreMap ism in isms)
            {
                StoreItem si = new StoreItem();
                si.ID = ism.ItemId;
                si.Name = ism.Item.ItemName;
                si.Desc = ism.Item.Description;
                si.ISID = ism.ItemStoreMapId;

                if (ism.Item.ItemImageMaps != null && ism.Item.ItemImageMaps.Count > 0)
                {
                    si.ImageUrl = ism.Item.ItemImageMaps.ElementAt(0).Image.ImageUrl;
                }
                if (ism.Item.ItemVideoMaps != null && ism.Item.ItemVideoMaps.Count > 0)
                {
                    //si.VideoUrl = ism.Item.ItemVideoMaps.ElementAt(0).Video.VideoName;
                }

                if (ism.Item.ItemCategoryMaps != null && ism.Item.ItemCategoryMaps.Count > 0)
                {
                    si.CatID = ism.Item.ItemCategoryMaps.ElementAt(0).ItemCategoryId;
                }

                if (ism.Item.ItemPriceMaps != null && ism.Item.ItemPriceMaps.Count > 0)
                {
                    List<ItemPrice> priceList = new List<ItemPrice>();
                    foreach (ItemPriceMap ipm in ism.Item.ItemPriceMaps)
                    {
                        ItemPrice ip = new ItemPrice(ipm.ItemPriceMapId, ipm.Price, ipm.Quantity, ipm.MetricUnit);
                        priceList.Add(ip);
                    }
                    si.PriceList = priceList;
                }
                
                if (ism.ItemStorePriceDiscounts != null && ism.ItemStorePriceDiscounts.Count > 0)
                {
                    List<Discount> discountList = new List<Discount>();
                    foreach (ItemStorePriceDiscount ispd in ism.ItemStorePriceDiscounts)
                    {
                        if(ispd.PriceDiscount.StartDate > DateTime.UtcNow || ispd.PriceDiscount.EndDate < DateTime.UtcNow)
                        {
                            continue;
                        }

                        Discount discount = new Discount(ispd.PriceDiscount.PriceDiscountId, 
                                                                        ispd.PriceDiscount.Name, 
                                                                        ispd.PriceDiscount.Description,
                                                                        ispd.PriceDiscount.Percentage);
                        discountList.Add(discount);
                    }

                    si.DiscountList = discountList;
                }

                availableStoreItems.Add(si);
            }

            return availableStoreItems;
        }

        public static List<ItemStoreMap> GetCachedStoreItems(int storeId)
        {
            List<ItemStoreMap> cachedStoreItems = new List<ItemStoreMap>();

            if (store.TryGetValue(storeId, out cachedStoreItems))
            {
                return cachedStoreItems;
            }

            List<ItemStoreMap> storeItemsDB = null;

            lock (storeCacheLock)
            {
                if (storeItemsDB == null)
                {
                    storeItemsDB = StoreDbManager.GetItemsInStore(storeId);
                }
            }
            if (storeItemsDB == null || storeItemsDB.Count <= 0)
            {
                return cachedStoreItems;
            }
            if (store.TryAdd(storeId, storeItemsDB))
            {
                store.TryGetValue(storeId, out cachedStoreItems);
            }

            return cachedStoreItems;
        }

        private static List<ItemStoreMap> FilterStoreItemsByAvailability(List<ItemStoreMap> storeItems)
        {
            List<ItemStoreMap> availableStoreItems = new List<ItemStoreMap>();

            foreach (ItemStoreMap storeItem in storeItems)
            {
                foreach (ItemsAvailability ia in storeItem.ItemsAvailabilities)
                {
                    if (ia.StartDate <= DateTime.UtcNow && ia.EndDate >= DateTime.UtcNow)
                    {
                        availableStoreItems.Add(storeItem);
                        break;
                    }
                }
            }
            return availableStoreItems;
        }

        public static Status ValidateItem(CheckoutItem purchaseItem)
        {
            Decimal hundred = 100.00M;
            Status status = Status.Success;

            List<StoreItem> availableStoreItems = GetAvailableStoreItems(purchaseItem.SID);
            if (availableStoreItems == null || availableStoreItems.Count <= 0)
            {
                status = Status.InvalidInput;
                return status;
            }

            StoreItem storeItem = availableStoreItems.Find(si => si.ID == purchaseItem.ID);
            if(storeItem == null)
            {
                status = Status.InvalidItem;
                return status;
            }
            ItemPrice itemPrice = storeItem.PriceList.Find(ip => ip.ID == purchaseItem.IPID);
            if (itemPrice == null)
            {
                status = Status.InvalidItem;
                return status;
            }
            if (itemPrice.Price <= 0)
            {
                status = Status.InvalidPrice;
                return status;
            }

            Decimal itemDiscount = CalculateDiscount(itemPrice.Price, storeItem);
            if (itemDiscount >= hundred)
            {
                status = Status.InvalidPrice;
                return status;
            }
           Decimal discountedPrice = itemPrice.Price - itemDiscount;

           return  discountedPrice == purchaseItem.CalcPrice? Status.Success: Status.InvalidPrice;
        }

        private static Decimal CalculateDiscount(decimal itemPrice, StoreItem storeItem)
        {
            Decimal discount = 0;
            storeItem.DiscountList.ForEach(dl=>{ discount += (itemPrice * dl.Percentage) / 100; });
            return discount;
        }
    }
}
