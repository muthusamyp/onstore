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
    
    public partial class ItemsAvailability
    {
        public int ItemAvailabilityID { get; set; }
        public int ItemStoreMapId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ItemStoreMap ItemStoreMap { get; set; }
    }
}
