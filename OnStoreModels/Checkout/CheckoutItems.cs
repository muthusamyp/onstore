using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Checkout
{
    public class CheckoutItems
    {
        public CheckoutItem[] Items { get; set; }
        public Decimal TotalCalcPrice { get; set; }
    }
}
