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
        public CheckoutItems CheckoutItems { get; set; }
        public User User { get; set; }
        public OrderDeliveryAddressMap DeliveryAddress { get; set; }
        public CheckoutRequest()
        {
        }
    }
}
