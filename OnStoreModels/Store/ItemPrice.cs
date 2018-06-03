using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels
{
    [Serializable]
    public class ItemPrice
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string MetricUnit { get; set; }

        public ItemPrice(){ }
        public ItemPrice(int id, decimal price, int quantity, string metricUnit)
        {
            this.ID = id;
            this.Price = price;
            this.Quantity = quantity;
            this.MetricUnit = metricUnit;
        }
    }
}
