using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Available> Availables { get; set; }
        ICollection<OrderOne> OrderOnes { get; set; }
        public Size()
        {
            OrderOnes = new List<OrderOne>();
            Availables = new List<Available>();
        }
    }
}