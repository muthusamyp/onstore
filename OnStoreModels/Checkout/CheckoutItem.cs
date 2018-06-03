using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels;

namespace OnStoreModels.Checkout
{
    [Serializable]
    public class CheckoutItem
    {
        public int ID { get; set; }
        public int SID { get; set; }
        public int IPID { get; set; }
        public int Qty { get; set; }
        public Decimal CalcPrice { get; set; }
    }
  
}
