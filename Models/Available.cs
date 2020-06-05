using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Available
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
        public int SizeID { get; set; }
        public Size Size { get; set; }
        public int Amount { get; set; }
    }
}