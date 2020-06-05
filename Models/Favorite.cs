using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
        public string UserEmail { get; set; }
    }
}