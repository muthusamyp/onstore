using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;

namespace OnStoreModels.Checkout
{
    [Serializable]
    public class CheckoutResponse
    {
        public Status Status { get; set; }
        public string Hash { get; set; }
        public string Key { get; set; }
        public string TransactonId { get; set; }
        public User User { get; set; }

        public CheckoutResponse()
        {
        }
    }
}
