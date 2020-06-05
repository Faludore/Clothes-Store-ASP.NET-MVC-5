using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreIdent.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public int PictureTypeID { get; set; } // название картинки
        public PictureType PictureType { get; set; }
        public byte[] Image { get; set; }
        public int ClotheID { get; set; }
        public Clothe Clothe { get; set; }
    }
}