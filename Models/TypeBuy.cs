using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class TypeBuy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Order> Orders { get; set; }
        public TypeBuy()
        {
            Orders = new List<Order>();
        }
        
    }
}