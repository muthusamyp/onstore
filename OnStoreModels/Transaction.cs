//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnStoreModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public long TransactionId { get; set; }
        public System.Guid UserId { get; set; }
        public byte TransactionType { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public byte Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ItemData { get; set; }
        public byte ItemDataFormat { get; set; }
    }
}
