using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class PictureType
    {
        public int Id { get; set; }
        public string Name { get; set; } // название картинки
        ICollection<Picture> Pictures { get; set; }
        public PictureType()
        {
            Pictures = new List<Picture>();
        }
    }
}