using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Checkout
{
    [Serializable]
    public class CheckoutRequest
    {
        public CheckoutItem[] Items { get; set; }
        public Decimal TotalCalcPrice { get; set; }
        public User User { get; set; }

        public CheckoutRequest()
        {
        }
    }
}
