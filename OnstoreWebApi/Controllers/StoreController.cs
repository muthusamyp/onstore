using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnStoreBusinessLayer;
using OnStoreModels;
using System.Text;
using System.Collections.Generic;

namespace OnstoreWebApi.Controllers
{
    public class StoreController : BaseController
    {
        [HttpGet()]
        public HttpResponseMessage GetStore()
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

                GetStoreRequest getStoreRequest = (GetStoreRequest)serializer.Deserialize(strRequest, typeof(GetStoreRequest));
                if (getStoreRequest == null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                GetStoreResponse response = new GetStoreResponse();
                List<StoreItem> storeItems = StoreManager.GetAvailableStoreItems(getStoreRequest.StoreID);
                if (storeItems == null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.PROCESSING_ERROR);
                    return httpResponseMessage;
                }

                response.StoreItems = storeItems;
                response.Success = true;
                string responseContent = serializer.Serialize(response);
                httpResponseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");

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
