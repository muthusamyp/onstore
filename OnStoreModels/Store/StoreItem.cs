using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels
{
    [Serializable]
    public class StoreItem
    {
        public int ID { get; set; }
        public int ISID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public List<ItemPrice> PriceList { get; set; }
        public List<Discount>  DiscountList { get; set; }

        public StoreItem()
        {
            this.PriceList = new  List<ItemPrice>();
            this.DiscountList = new List<Discount>();
        }    
    }
}
