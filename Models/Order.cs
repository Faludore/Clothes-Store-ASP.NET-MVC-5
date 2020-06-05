using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string Notes { get; set; }
        public string DnT { get; set; }
        public int TypeBuyID { get; set; }
        public TypeBuy TypeBuy { get; set; }             
        public decimal TotalSum { get; set; }
    
       
        ICollection<OrderOne> OrderOnes { get; set; }
        public Order()
        {
            OrderOnes = new List<OrderOne>();
        }
    }
}