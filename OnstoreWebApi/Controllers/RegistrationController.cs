using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OnStoreBusinessLayer;
using OnStoreBusinessLayer.Registration;
using OnStoreModels;
using OnStoreModels.Registration;
using OnStoreModels.Common;
using System.Text;

namespace OnstoreWebApi.Controllers
{
    public class RegistrationController : BaseController
    {

        [HttpPost()]
        public HttpResponseMessage Register()
        {
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
            string strRequest = string.Empty;

            try
            {
                strRequest = Request.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrWhiteSpace(strRequest))
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                JsonSerializerHelper serializer = new JsonSerializerHelper();
                RegistrationRequest registrationRequest = (RegistrationRequest)serializer.Deserialize(strRequest, typeof(RegistrationRequest));

                if (registrationRequest != null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                RegistrationResponse response = RegistrationManager.Register(registrationRequest);
                if (response != null && response.Status == RegistrationStatus.Success)
                {
                    string responseContent = serializer.Serialize(response);
                    httpResponseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");
                }
                else
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.PROCESSING_ERROR);
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex, strRequest);
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpResponseMessage.Content = new StringContent(Constants.PROCESSING_ERROR);
            }

            return httpResponseMessage;
        }
    }
}
