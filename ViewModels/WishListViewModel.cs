using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.ViewModels
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public string Info { get; set; }
        public int ClotheID { get; set; }

    }
}