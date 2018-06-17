using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnStoreBusinessLayer;
using OnStoreModels;
using OnStoreBusinessLayer.Registration;
using OnStoreModels.Registration;
using OnStoreModels.Login;
using OnStoreBusinessLayer.Login;
using OnStoreModels.Checkout;
using OnStoreBusinessLayer.Checkout;
using OnStoreModels.Common;

namespace OnStoreTodayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Register();
            ProcessPaymentNotification();
           // InitiateCheckout();
            Console.WriteLine("Press any key to continue:");
            Console.Read();
        }
        public static void TestSerialize()
        {
            List<StoreItem> storeItems = StoreManager.GetAvailableStoreItems(1);
            JsonSerializerHelper jsh = new JsonSerializerHelper();
            string serializedData = jsh.Serialize(storeItems);
        }

        public static void Register()
        {
            RegistrationRequest request = new RegistrationRequest();
            request.UserName = "MuthuPPP";
            request.FirstName = "Muthusamy";
            request.LastName = "P";
            request.Password = "123456";
            request.PrimaryEmail = "pitchiah.muthusamy@gmail.com";
            request.PrimaryContactNo = "+919986249131";

            RegistrationResponse response = RegistrationManager.Register(request);
        }

        public static void Login()
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest.UserName = "MuthuPP";
            loginRequest.Password = "123456";
            LoginResponse response = LoginManager.Login(loginRequest);
        }

        public static void InitiateCheckout()
        {
            Guid userId = new Guid("92A8AA97-294C-487F-8B82-4DA9836C1136");
            User user = DbCommonManager.GetUser(userId);

            Configuration config = new Configuration();
            config.PayUFailureUrl = "FailureUrl";
            config.PayUSuccessUrl = "SuccessUrl";
            config.PayUSalt = "Salt";
            config.PayUKey = "Key";

            PayUManager payUManager = new PayUManager(config);

            CheckoutRequest cr = new CheckoutRequest();
            cr.User = user;
            cr.DeliveryAddress = new OrderDeliveryAddressMap();
            cr.DeliveryAddress.City = "Bangalore";
            cr.DeliveryAddress.ZipCode = "560068";
            cr.DeliveryAddress.State = "Karnataka";
            cr.DeliveryAddress.Address = "No 204 Sunnyvale apartment, 15th main road, AECS layout block A, Singasandra/Kudlu";

            CheckoutItems coi = new CheckoutItems();
            cr.CheckoutItems = coi;
            List<CheckoutItem> cil = new List<CheckoutItem>();
            CheckoutItem ci = new CheckoutItem();
            ci.SID = 1;
            ci.Qty = 1;
            ci.ID = 1;
            ci.IPID = 1;
            ci.CalcPrice = 135;
            cil.Add(ci);
            cr.CheckoutItems.Items = cil.ToArray();
            cr.CheckoutItems.TotalCalcPrice = 135;

            CheckoutResponse response = payUManager.InitiateCheckout(cr);
        } 

        public static void ProcessPaymentNotification()
        {
            Guid userId = new Guid("92A8AA97-294C-487F-8B82-4DA9836C1136");
            User user = DbCommonManager.GetUser(userId);

            Configuration config = new Configuration();
            config.PayUFailureUrl = "FailureUrl";
            config.PayUSuccessUrl = "FailureUrl";
            config.PayUSalt = "Salt";
            config.PayUKey = "Key";

            PayUManager payUManager = new PayUManager(config);
            Payment payment = new Payment();
            payment.customerEmail = "pitchiah.muthusamy@gmail.com";
            payment.customerPhone = "9986249131";
            payment.customerName = "MuthuPP";
            payment.amount  = 135.00M;
            payment.notificationId = "123456";
            payment.paymentId = "12345678";
            payment.paymentMode = "DC";
            payment.productInfo = "ProductInfo";
            payment.split_info = "split_info";
            payment.status = "Success";
            payment.merchantTransactionId = "1";
            payment.udf1 = "92A8AA97-294C-487F-8B82-4DA9836C1136";

            Status status = payUManager.ProcessPayment(payment);
        }

        public static void TestCheckout()
        {
            List<CheckoutItem> cil = new List<CheckoutItem>();
            CheckoutItem ci = new CheckoutItem();
            ci.SID = 1;
            ci.Qty = 1;
            ci.ID = 1;
            ci.IPID = 1;
            ci.CalcPrice = 135;
            cil.Add(ci);

            JsonSerializerHelper jsh = new JsonSerializerHelper();
            string serializedItems = jsh.Serialize(ci);

            //cr.Items = cil.ToArray();
        }
    }
}
