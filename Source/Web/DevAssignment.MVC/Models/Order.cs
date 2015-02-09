using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevAssignment.MVC.Models
{
    [Table("dbo.Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public int AccountId { get; set; }

    }
}