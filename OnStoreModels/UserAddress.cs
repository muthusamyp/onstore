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
    
    public partial class UserAddress
    {
        public int UserAddressId { get; set; }
        public System.Guid UserId { get; set; }
        public string Address { get; set; }
        public bool IsDeliveryAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int AddressType { get; set; }
    }
}
