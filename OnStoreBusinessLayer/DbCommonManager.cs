using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels;

namespace OnStoreBusinessLayer
{
    public class DbCommonManager
    {
        public static User GetUser(string userName)
        {
            using (var context = new OnStoreEntities())
            {
                User user = context.Users.SingleOrDefault(usr => usr.Name == userName);
                return user;
            }
        }

        public static User GetUser(Guid userId)
        {
            using (var context = new OnStoreEntities())
            {
                User user = context.Users.SingleOrDefault(usr => usr.UserId == userId);
                return user;
            }
        }
    }
}
