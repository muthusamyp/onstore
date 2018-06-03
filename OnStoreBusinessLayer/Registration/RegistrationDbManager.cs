using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels;
using OnStoreModels.Registration;
using OnStoreBusinessLayer.Utility;

namespace OnStoreBusinessLayer.Registration
{
    internal class RegistrationDbManager
    {
        public static User CreateUser(RegistrationRequest registrationRequest)
        {
            User user = null;

            using (var context = new OnStoreEntities())
            {
                user = context.Users.Create();

                user.FirstName = registrationRequest.FirstName;
                if (!string.IsNullOrWhiteSpace(registrationRequest.LastName))
                {
                    user.LastName = registrationRequest.LastName;
                }
                user.PrimaryEmail = registrationRequest.PrimaryEmail;
                if (!string.IsNullOrWhiteSpace(registrationRequest.SecondaryEmail))
                {
                    user.SecondaryEmail = registrationRequest.SecondaryEmail;
                }
                user.PrimaryContactNo = registrationRequest.PrimaryContactNo;
                if (!string.IsNullOrWhiteSpace(registrationRequest.SecondaryContactNo))
                {
                    user.SecondaryContactNo = registrationRequest.SecondaryContactNo;
                }
                user.Name = registrationRequest.UserName;
                user.Password = CryptoHelper.ComputeSHA256Hash(registrationRequest.Password.ToLowerInvariant());
                user.UserId = System.Guid.NewGuid();
                user.IsActive = true;
                user.IsMember = false;
                user.CreatedDate = DateTime.UtcNow;
                context.Users.Add(user);

                int affectedRecords = context.SaveChanges();
                if (affectedRecords <= 0)
                {
                    user = null;
                }
            }

            return user;
        }
    }
}
