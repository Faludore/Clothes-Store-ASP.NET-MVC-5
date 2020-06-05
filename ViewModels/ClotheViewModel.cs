using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreIdent.ViewModels
{
    public class ClotheViewModel
    {

        
        public int Id { get; set; }

        [Display(Name = "Clothe Name")]
        [Required(ErrorMessage = ("Clothe Name is required")), RegularExpression(@"^[a-zA-Z_ .,]*$", ErrorMessage ="Only alphabetic charaters are  allowed.")]
        public string Name { get; set; }     
        public int TypeClotheID { get; set; }       
        public int MaterialID { get; set; }       
        public int BrandID { get; set; }
        public int GenderID { get; set; }

        public decimal Price { get; set; }
        [Display(Name = "Info")]
        //[Required(ErrorMessage = ("Clothe Name is required")), RegularExpression(@"^[a-zA-Z_ .,]*$", ErrorMessage = "Only alphabetic charaters are  allowed.")]
        public string Info { get; set; }
    }
}