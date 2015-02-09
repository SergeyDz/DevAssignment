using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevAssignment.MVC.Models
{
    [Table("dbo.User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Pleaase enter user login.")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        [Editable(true)]
        [Remote("IsUserAvailable", "User")]
        public string Login { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "Please enter user name")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}