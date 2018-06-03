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

namespace OnStoreBusinessLayer
{
    internal class StoreDbManager
    {
        internal static List<ItemStoreMap> GetItemsInStore(int storeId)
        {
            List<ItemStoreMap> storeItems = new List<ItemStoreMap>();
            using (var context = new OnStoreEntities())
            {
                storeItems = context.ItemStoreMaps
                         .Where(ism => ism.StoreId == storeId && ism.Item.Active)
                         .Include(ism => ism.Item)
                         .Include(ism => ism.Item.ItemCategoryMaps)
                         .Include(ism => ism.Item.ItemPriceMaps)
                         .Include(ism => ism.Item.ItemVendorPrices)
                         .Include(ism => ism.Item.ItemVideoMaps)
                         .Include(ism => ism.Item.ItemImageMaps)
                         .Include(ism => ism.Store)
                         .Include(ism => ism.ItemsAvailabilities)
                         .Include(ism => ism.ItemStorePriceDiscounts)
                         .Include(ism => ism.ItemStorePriceDiscounts.Select(ispc => ispc.PriceDiscount))
                         .ToList();
            }

            return storeItems;
        }
    }
}
