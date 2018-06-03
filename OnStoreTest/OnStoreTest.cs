using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnStoreModels;
using OnStoreBusinessLayer;
using System.Collections;
using System.Collections.Generic;
using OnStoreBusinessLayer.Registration;
using OnStoreModels.Registration;
using OnStoreModels.Checkout;
using OnStoreBusinessLayer.Checkout;
using OnStoreModels.Common;

namespace OnStoreTest
{
    [TestClass]
    public class StoreManagerTest
    {
        [TestMethod]
        public void TestStoreOne()
        {
           List<StoreItem> storeItems = StoreManager.GetAvailableStoreItems(1);
        }

        [TestMethod]
        public void TestStoreNotOne()
        {
            List<StoreItem> storeItems = StoreManager.GetAvailableStoreItems(11);
        }

        [TestMethod]
        public void TestSerializeStoreOne()
        {
            List<StoreItem> storeItems = StoreManager.GetAvailableStoreItems(1);

            JsonSerializerHelper jsh = new JsonSerializerHelper();
            string serializedData = jsh.Serialize(storeItems);

        }

        [TestMethod]
        public void TestRegistrationValid()
        {
            RegistrationRequest request = new RegistrationRequest();
            request.UserName = "Muthu" + DateTime.Now.Ticks;
            request.FirstName = "Muthusamy";
            request.LastName = "P";
            request.Password = "123456";
            request.PrimaryEmail = "pitchiah.muthusamy@gmail.com";
            request.PrimaryContactNo = "+919986249131";

            RegistrationResponse response = RegistrationManager.Register(request);
        }

        [TestMethod]
        public void TestInitiateCheckout()
        {
            Guid userId = new Guid("92A8AA97-294C-487F-8B82-4DA9836C1136");
            User user = DbCommonManager.GetUser(userId);

            Configuration config = new Configuration();
            PayUManager payUManager = new PayUManager(config);

            CheckoutRequest cr = new CheckoutRequest();
            cr.User = user;
            cr.TotalCalcPrice = 135;

            List<CheckoutItem> cil = new List<CheckoutItem>();
            CheckoutItem ci = new CheckoutItem();
            ci.SID = 1;
            ci.Qty = 1;
            ci.ID = 1;
            ci.IPID = 1;
            cil.Add(ci);
            cr.Items = cil.ToArray();

            CheckoutResponse response = payUManager.InitiateCheckout(cr);
        }
    }
}
