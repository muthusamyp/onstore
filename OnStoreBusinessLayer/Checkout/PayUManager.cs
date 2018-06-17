using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreModels.Checkout;
using OnStoreModels.Common;
using OnStoreBusinessLayer.Utility;
using OnStoreModels;

namespace OnStoreBusinessLayer.Checkout
{
    public class PayUManager
    {
        private Configuration configuration = null;
        public PayUManager(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public CheckoutResponse InitiateCheckout(CheckoutRequest checkoutRequest)
        {
            CheckoutResponse response = new CheckoutResponse();

            Status status = ValidateCheckout(checkoutRequest);
            if (status != Status.Success)
            {
                response.Status = status;
                return response;
            }

            string productInfo = "ProductInfo";

            JsonSerializerHelper jsh = new JsonSerializerHelper();
            string serializedItems = jsh.Serialize(checkoutRequest.CheckoutItems);
            if (string.IsNullOrWhiteSpace(serializedItems))
            {
                response.Status = Status.InvalidItem;
                return response;
            }

            long transactionId = CheckoutDbManager.CreateTransaction(checkoutRequest.User
                                                , serializedItems
                                                , ItemDataFormat.JSON
                                                , TransactionType.Purchase
                                                , TransactionStatus.Initiated
                                                );

            if (transactionId <= -1)
            {
                response.Status = Status.Failure;
                return response;
            }

            long orderDeliveryAddressMapId = CheckoutDbManager.CreateOrderDeliveryAddressMap(transactionId, checkoutRequest.DeliveryAddress);

            if (orderDeliveryAddressMapId <= -1)
            {
                response.Status = Status.Failure;
                return response;
            }

            string hashSequence = GeneratePaymentRequestHashSequence(checkoutRequest.User.UserId
                                                            , this.configuration.PayUKey
                                                            , transactionId
                                                            , checkoutRequest.CheckoutItems.TotalCalcPrice
                                                            , productInfo
                                                            , checkoutRequest.User.FirstName
                                                            , checkoutRequest.User.PrimaryEmail
                                                            , this.configuration.PayUSalt);

            string hash = CryptoHelper.ComputeSHA512Hash(hashSequence);

            response.Status = Status.Success;
            response.Hash = hash;
            response.TransactonId = transactionId.ToString();
            response.User = checkoutRequest.User;

            return response;
        }

        private Status ValidateCheckout(CheckoutRequest checkoutRequest)
        {
            Status status = Status.Success;

            if (checkoutRequest == null
                || checkoutRequest.CheckoutItems == null
                || checkoutRequest.CheckoutItems.TotalCalcPrice <= 0
                || checkoutRequest.CheckoutItems.Items == null
                || checkoutRequest.CheckoutItems.Items.Length <= 0)
            {
                status = Status.InvalidInput;
                return status;
            }

            Decimal totalCalcPrice = 0;

            foreach (CheckoutItem pi in checkoutRequest.CheckoutItems.Items)
            {
                totalCalcPrice += pi.CalcPrice;
            }
            if (totalCalcPrice != checkoutRequest.CheckoutItems.TotalCalcPrice)
            {
                status = Status.InvalidPrice;
                return status;
            }

            foreach (CheckoutItem pi in checkoutRequest.CheckoutItems.Items)
            {
                Status validationStatus = StoreManager.ValidateItem(pi);
                if (validationStatus != Status.Success)
                {
                    //TODO log the purchase failure
                    return validationStatus;
                }
            }

            return status;
        }

        private string GeneratePaymentRequestHashSequence(Guid userId, string key, long transactionId, decimal amount, string productInfo, string firstname, string email, string salt)
        {
            string hashSequence = string.Empty;
            List<string> hashParams = new List<string>();

            hashParams.Add(key);
            hashParams.Add(transactionId.ToString());
            hashParams.Add(amount.ToString());
            hashParams.Add(productInfo);
            hashParams.Add(firstname);
            hashParams.Add(email);

            hashParams.Add(userId.ToString());
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(string.Empty);
            hashParams.Add(salt);

            hashSequence = string.Join("|", hashParams.ToArray());

            return hashSequence;
        }

        public Status ProcessPayment(Payment payment)
        {
            if (payment == null
                || string.IsNullOrWhiteSpace(payment.paymentId)
                || string.IsNullOrWhiteSpace(payment.merchantTransactionId))
            {
                return Status.InvalidInput;
            }

            long transactionId = -1;
            long.TryParse(payment.merchantTransactionId, out transactionId);
            Transaction transaction = CheckoutDbManager.GetTransaction(transactionId);
            if (transaction == null)
            {
                return Status.InvalidTransaction;
            }
            Guid userId = Guid.Empty;
            if (!Guid.TryParse(payment.udf1, out userId))
            {
                return Status.InvalidUserID;
            }
            if (string.IsNullOrWhiteSpace(transaction.ItemData))
            {
                return Status.InvalidItem;
            }

            JsonSerializerHelper jsh = new JsonSerializerHelper();
            CheckoutItems checkoutItems = (CheckoutItems)jsh.Deserialize(transaction.ItemData, typeof(CheckoutItems));
            if (checkoutItems == null
                || checkoutItems.Items == null
                || checkoutItems.Items.Length <= 0)
            {
                return Status.InvalidItem;
            }

            if (payment.amount != checkoutItems.TotalCalcPrice)
            {
                return Status.InvalidAmount;
            }

            if (payment.status == "Success")
            {

                CheckoutDbManager.UpdateTransactionStatus(transactionId
                                                           , TransactionStatus.Paid);

                int receiptId = CheckoutDbManager.CreateReceipt(transaction.UserId
                                                                , transaction.TransactionId
                                                                , payment.paymentId
                                                                , (TransactionType)transaction.TransactionType
                                                                , TransactionProvider.PayU
                                                                , TransactionStatus.Paid);

                string rawReceipt = string.Empty;
                if (receiptId > 0)
                {
                    rawReceipt = jsh.Serialize(payment);
                    CheckoutDbManager.CreateRawReceipt(receiptId, rawReceipt);
                }

            }

            return Status.Success;
        }
    }
}
