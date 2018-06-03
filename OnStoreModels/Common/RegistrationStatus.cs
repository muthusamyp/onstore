using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Common
{
    public enum RegistrationStatus
    {
        Success = 1,
        InvalidInput = 2,
        UserNameNotAvailable = 3,
        InvalidPrimaryEmail = 3,
        InvalidSecondaryEmail = 4,
        InvalidPrimaryContactNo = 5,
        InvalidSecondaryContactNo = 6,
        InvalidToken = 7,
        Failure = 7
    }
}
