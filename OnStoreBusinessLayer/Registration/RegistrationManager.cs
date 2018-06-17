using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels;
using OnStoreModels.Common;
using OnStoreBusinessLayer.Utility;
using OnStoreModels.Registration;
using OnStoreBusinessLayer.Login;

namespace OnStoreBusinessLayer.Registration
{
    public class RegistrationManager
    {
        public static RegistrationResponse Register(RegistrationRequest registrationRequest)
        {
            RegistrationResponse response = new RegistrationResponse();
            response.Status = RegistrationStatus.Failure;
           
            if (registrationRequest == null
                || string.IsNullOrWhiteSpace(registrationRequest.UserName)
                || string.IsNullOrWhiteSpace(registrationRequest.Password)
                || string.IsNullOrWhiteSpace(registrationRequest.FirstName)
                || string.IsNullOrWhiteSpace(registrationRequest.PrimaryEmail)
                || string.IsNullOrWhiteSpace(registrationRequest.PrimaryContactNo))
            {
                response.Status = RegistrationStatus.InvalidInput;
                return response;
            }

            if (!Validator.IsValidEmail(registrationRequest.PrimaryEmail))
            {
                response.Status = RegistrationStatus.InvalidPrimaryEmail;
                return response;
            }

            if (!string.IsNullOrWhiteSpace(registrationRequest.SecondaryEmail) 
                && !Validator.IsValidEmail(registrationRequest.SecondaryEmail))
            {
                response.Status = RegistrationStatus.InvalidSecondaryEmail;
                return response;
            }

            /*if (!Validator.IsValidPhoneNumber(registrationRequest.PrimaryContactNo))
            {
                response.Status = RegistrationStatus.InvalidPrimaryContactNo;
                return response;
            } */

            if (!string.IsNullOrWhiteSpace(registrationRequest.SecondaryContactNo) 
                && !Validator.IsValidPhoneNumber(registrationRequest.SecondaryContactNo))
            {
                response.Status = RegistrationStatus.InvalidSecondaryContactNo;
                return response;
            }

            if (!IsUserNameAvailable(registrationRequest.UserName))
            {
                response.Status = RegistrationStatus.UserNameNotAvailable;
                return response;
            }

            User user = RegistrationDbManager.CreateUser(registrationRequest);
            if(user == null)
            {
                //TODO log the request
                response.Status = RegistrationStatus.Failure;
                return response;
            }
            
            string token = LoginDbManager.CreateToken(user);
            if (string.IsNullOrWhiteSpace(token))
            {
                response.Status = RegistrationStatus.InvalidToken;
                return response;
            }
            response.Status = RegistrationStatus.Success;
            response.Token = token;
            response.User = user;

            return response;
        }

        public static bool IsUserNameAvailable(string userName)
        {
            bool available = false;

            if (string.IsNullOrWhiteSpace(userName)) { return available; }
            User user = DbCommonManager.GetUser(userName);
            available = user == null ? true : false;

            return available;
        }
    }
}
