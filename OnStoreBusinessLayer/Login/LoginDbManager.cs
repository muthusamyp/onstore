using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;
using OnStoreModels;
using OnStoreBusinessLayer.Utility;

namespace OnStoreBusinessLayer.Login
{
    public class LoginDbManager
    {
        private static int EXPIRATION_DURATION = 2;

        public static string CreateToken(User userInfo)
        {
            string token = string.Empty;

            using (var context = new OnStoreEntities())
            {
                UserLogin userLogin = context.UserLogins.Create();
                userLogin.UserId = userInfo.UserId;
                userLogin.LoggedInFrom = "PC";
                userLogin.Token = System.Guid.NewGuid().ToString();
                userLogin.ExpiresAt = DateTime.UtcNow.AddHours(EXPIRATION_DURATION);
                userLogin.CreatedDate = DateTime.UtcNow;
                context.UserLogins.Add(userLogin);

                if (context.SaveChanges() > 0)
                {
                    token = userLogin.Token;
                }             
            }
            return token;
        }
    }
}
