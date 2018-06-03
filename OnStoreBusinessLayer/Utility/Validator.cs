using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnStoreBusinessLayer.Utility
{
    public class Validator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string strPhone)
        {
            Regex phoneRegex = new Regex("^\\+[9][1][-][\\d]{10}$");
            return phoneRegex.Match(strPhone).Success ? true : false;
        }
    }
}
