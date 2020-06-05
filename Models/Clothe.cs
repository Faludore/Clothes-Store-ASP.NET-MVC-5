using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Clothe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeClotheID { get; set; }
        public TypeClothe TypeClothe { get; set; }
        public int MaterialID { get; set; }
        public Material Material { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
        public string DnTStart { get; set; }
        public string DnTEnd { get; set; }
        public decimal Price { get; set; }
        public string Info { get; set; }
        public int? Mark { get; set; }


        ICollection<Available> Availables { get; set; }
        ICollection<Picture> Pictures { get; set; }
        ICollection<OrderOne> OrderOnes { get; set; }
        ICollection<Favorite> Favorites { get; set; }
        ICollection<PopularClothe> PopularClothes { get; set; }
        ICollection<PopularClotheForUser> PopularClotheForUsers { get; set; }
        
        public Clothe()
        {
            Availables = new List<Available>();
            Pictures = new List<Picture>();
            OrderOnes = new List<OrderOne>();
            Favorites = new List<Favorite>();
            PopularClothes = new List<PopularClothe>();
            PopularClotheForUsers = new List<PopularClotheForUser>();
        }
    }
}