using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.Text;

namespace OnstoreWebApi.Controllers
{
    public class BaseController : ApiController
    {
        public void HandleException(Exception ex, string request)
        {

        }
        public void HandleException(DbEntityValidationException ex, string request)
        {

            foreach (var eve in ex.EntityValidationErrors)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:"
                    , eve.Entry.Entity.GetType().Name
                    , eve.Entry.State));

                foreach (var ve in eve.ValidationErrors)
                {
                    sb.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage));
                }
                string errorMessage = sb.ToString();
            }
        }
    }
}
