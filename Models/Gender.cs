using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        ICollection<Clothe> Clothes { get; set; }
        public Gender()
        {
            Clothes = new List<Clothe>();
        }
    }
}