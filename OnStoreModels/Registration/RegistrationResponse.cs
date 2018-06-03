using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;

namespace OnStoreModels.Registration
{
    [Serializable]
    public class RegistrationResponse
    {
        public RegistrationStatus Status { get; set; }
        public string Token { get; set; }
        public User User { get; set; }

        public RegistrationResponse()
        {
        }
    }
}
