using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnStoreBusinessLayer;
using OnStoreModels;
using System.Text;
using System.Collections.Generic;
using OnStoreModels.Checkout;
using OnStoreBusinessLayer.Checkout;
using OnStoreModels.Common;

namespace OnstoreWebApi.Controllers
{
    public class CheckoutController : BaseController
    {
        [HttpPost()]
        public HttpResponseMessage InitiatePayUCheckout()
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
                CheckoutRequest checkoutRequest = (CheckoutRequest)serializer.Deserialize(strRequest, typeof(CheckoutRequest));
                if (checkoutRequest == null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                Configuration config = new Configuration();
                config.PayUFailureUrl = "FailureUrl";
                config.PayUSuccessUrl = "SuccessUrl";
                config.PayUSalt = "Salt";
                config.PayUKey = "Key";

                PayUManager payUManager = new PayUManager(config);
                CheckoutResponse response = payUManager.InitiateCheckout(checkoutRequest);
                if (response != null && response.Status == Status.Success)
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

        [HttpPost()]
        public HttpResponseMessage ProcessPayment()
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
                Payment paymentRequest = (Payment)serializer.Deserialize(strRequest, typeof(Payment));
                if (paymentRequest == null)
                {
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    httpResponseMessage.Content = new StringContent(Constants.INVALID_REQUEST);
                    return httpResponseMessage;
                }

                Configuration config = new Configuration();
                PayUManager payUManager = new PayUManager(config);
                Status status = payUManager.ProcessPayment(paymentRequest);
                if (status != Status.Success)
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
