using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class PopularClotheForUser
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
        public string UserEmail { get; set; }
        public string DnT { get; set; }
    }
}