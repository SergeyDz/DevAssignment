//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SD.CodeProblem.DevAssignment.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedById { get; set; }
    
        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
