using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Common
{
   public enum Status
    {
        Success = 1,
        InvalidInput = 2,
        InvalidReceipt = 3,
        InvalidItem = 4,
        InvalidPrice = 5,
        InvalidTransaction = 6,
        Failure = 7,
        InvalidUserID = 8,
        InvalidAmount = 9
    }
}
