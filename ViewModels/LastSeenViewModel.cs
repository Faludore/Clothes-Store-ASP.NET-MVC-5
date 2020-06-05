using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.ViewModels
{
    public class LastSeenViewModel
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }       
        public int? Mark { get; set; }
        public string DnT { get; set; }
    }
}