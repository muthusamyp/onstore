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
    
    public partial class PurchaseReceipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseReceipt()
        {
            this.PurchaseRawReceipts = new HashSet<PurchaseRawReceipt>();
        }
    
        public int PurchaseReceiptId { get; set; }
        public System.Guid UserId { get; set; }
        public string VendorTransactionId { get; set; }
        public byte TransactionType { get; set; }
        public byte TransactionProvider { get; set; }
        public byte TransactionStatus { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public long TransactionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseRawReceipt> PurchaseRawReceipts { get; set; }
    }
}
