using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;
using OnStoreModels;

namespace OnStoreModels.Login
{
    [Serializable]
    public class LoginResponse
    {
        public LoginStatus Status { get; set; }
        public string Token { get; set; }
        public User User { get; set; }

        public LoginResponse()
        {
        }
    }
}
