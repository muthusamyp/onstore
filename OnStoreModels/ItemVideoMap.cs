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
    
    public partial class ItemVideoMap
    {
        public int ItemVideoMapId { get; set; }
        public int VideoId { get; set; }
        public int ItemId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Video Video { get; set; }
    }
}