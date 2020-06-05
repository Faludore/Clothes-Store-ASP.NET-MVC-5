using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        ICollection<Clothe> Clothes { get; set; }
        public Brand()
        {
            Clothes = new List<Clothe>();
        }
    }
}