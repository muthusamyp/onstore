using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OnStoreBusinessLayer;
using OnStoreBusinessLayer.Login;
using OnStoreModels;
using OnStoreModels.Login;
using OnStoreModels.Common;
using System.Text;

namespace OnstoreWebApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        [HttpPost()]
        public HttpResponseMessage Login()
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
                LoginRequest loginRequest = (LoginRequest)serializer.Deserialize(strRequest, typeof(LoginRequest));

                if (loginRequest != null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                LoginResponse response = LoginManager.Login(loginRequest);
                if (response != null && response.Status == LoginStatus.Success)
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
