﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Checkout
{
    [Serializable]
    public class Payment
    {
        public string split_info { get; set; }
        public string customerName { get; set; }
        public decimal additionalCharges { get; set; }
        public string paymentMode { get; set; }
        public string hash { get; set; }
        public string status { get; set; }
        public string error_Message { get; set; }
        public string paymentId { get; set; }
        public string productInfo { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string merchantTransactionId { get; set; }
        public decimal amount { get; set; }
        public string notificationId { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }

        public Payment()
        {
        }
    }
}
