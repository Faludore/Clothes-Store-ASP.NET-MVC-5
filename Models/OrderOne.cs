using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class OrderOne
    {
        public int Id { get; set; }
        public int? OrderID { get; set; }
        public Order Order { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
        public decimal Price { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select country Name")]
        public int Amount { get; set; }
        public string UserEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select country Name")]
        public int SizeID { get; set; }
        public Size Size { get; set; }

    }
}