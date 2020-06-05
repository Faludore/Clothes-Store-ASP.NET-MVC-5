using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class PopularClothe
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
        public int Views { get; set; }
    }
}