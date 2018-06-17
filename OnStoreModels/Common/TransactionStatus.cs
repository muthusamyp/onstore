using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Common
{
    public enum TransactionStatus
    {
        Initiated = 1,
        Paid = 2,
        Failed = 3,
        Refund = 4,
        Dispute = 5,
        Shipped = 6,
        Delivered = 7
    }
}
