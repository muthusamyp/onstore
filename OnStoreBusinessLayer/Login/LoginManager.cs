using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Common;
using OnStoreModels;
using OnStoreModels.Login;
using OnStoreBusinessLayer.Utility;

namespace OnStoreBusinessLayer.Login
{
    public class LoginManager
    {
        public static LoginResponse Login(LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            response.Status = LoginStatus.Failure;

            if (loginRequest == null)
            {
                response.Status = LoginStatus.InvalidInput;
                return response;
            }
            if (string.IsNullOrWhiteSpace(loginRequest.UserName) 
                || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                response.Status = LoginStatus.InvalidUserNamePassword;
                return response;
            }

            User user = DbCommonManager.GetUser(loginRequest.UserName);
            if (user == null )
            {
                response.Status = LoginStatus.InvalidUser;
                return response;
            }

            string hashedPassword = CryptoHelper.ComputeSHA256Hash(loginRequest.Password.ToLowerInvariant());
            if (!user.Password.Equals(hashedPassword))
            {
                response.Status = LoginStatus.InvalidUserNamePassword;
                return response;
            }

            string token = LoginDbManager.CreateToken(user);
            if (string.IsNullOrWhiteSpace(token))
            {
                response.Status = LoginStatus.InvalidToken;
                return response;
            }
            response.Status = LoginStatus.Success;
            response.Token = token;
            response.User = user;

            return response;
        }
    }
}
