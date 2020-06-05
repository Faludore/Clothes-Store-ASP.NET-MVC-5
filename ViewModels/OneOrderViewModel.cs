using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.ViewModels
{
    public class OneOrderViewModel
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Size { get; set; }
    }
}