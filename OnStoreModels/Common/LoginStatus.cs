using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Common
{
    public enum LoginStatus 
    {
        Success = 1,
        InvalidInput = 2,
        InvalidUserNamePassword = 3,
        InvalidPrice = 4,
        InvalidToken = 5,
        InvalidUser = 6,
        Failure = 7
    }
}
