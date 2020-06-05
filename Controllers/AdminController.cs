using StoreIdent.Concrete.MSSQL;
using StoreIdent.Interface;
using StoreIdent.Models;
using StoreIdent.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoreIdent.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext dbCon;

        IRepository<Clothe> dbClothe;
        IRepository<TypeClothe> dbTypeClothe;
        IRepository<Material> dbMaterial;
        IRepository<Brand> dbBrand;
        IRepository<ApplicationUser> dbUser;
        IRepository<Available> dbAvailable;
        IRepository<Favorite> dbFavorite;
        IRepository<PictureType> dbPictureType;
        IRepository<Picture> dbPicture;
        IRepository<Gender> dbGender;
        public AdminController()
        {
            dbClothe = new ClotheRepository();
            dbTypeClothe = new TypeClotheRepository();
            dbMaterial = new MaterialRepository();
            dbBrand = new BrandRepository();
            dbUser = new UserRepository();
            dbCon = new ApplicationDbContext();
            dbAvailable = new AvailableRepository();
            dbFavorite = new FavoriteRepository();
            dbPictureType = new PictureTypeRepository();
            dbPicture = new PictureRepository();
            dbGender = new GenderRepository();
        }

      
        //Lists

        [Authorize(Roles = "admin")]
        public List<string> ListTables()
        {
            List<string> listOfTables = new List<string>();
           
            var metadata = ((IObjectContextAdapter)dbCon).ObjectContext.MetadataWorkspace;
            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .Single()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                || s.MetadataProperties["Type"].ToString() == "Tables");
           
            foreach (var table in tables)
            {
                var tableName = table.MetadataProperties.Contains("Table")
                    && table.MetadataProperties["Table"].Value != null
                    ? table.MetadataProperties["Table"].Value.ToString()
                    : table.Name;          
                listOfTables.Add(tableName);
            }          
            return listOfTables;
        }
        
        public async Task<ActionResult> ListAspNetUsers()
        {
            ViewBag.tables = ListTables();
            var items = await dbUser.GetItemListAsync();
            return View(items);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListBrands()
        {
            ViewBag.tables = ListTables();
            var items = await dbBrand.GetItemListAsync();
            return View(items);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListMaterials()
        {
            ViewBag.tables = ListTables();
            var items = await dbMaterial.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListTypeClothes()
        {
            ViewBag.tables = ListTables();
            var items = await dbTypeClothe.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListClothes()
        {
            ViewBag.tables = ListTables();
            var items = await dbClothe.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListAvailables()
        {
            ViewBag.tables = ListTables();
            var items = await dbAvailable.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListFavorites()
        {
            ViewBag.tables = ListTables();
            var items = await dbFavorite.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListPictureTypes()
        {
            ViewBag.tables = ListTables();
            var items = await dbPictureType.GetItemListAsync();
            return View(items);
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListPictures()
        {
            ViewBag.tables = ListTables();
            var items = await dbPicture.GetItemListAsync();
            return View(items);
        }

        //Creates
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateClothe()
        {
            ViewBag.tables = ListTables();
            var clotheViewModel = new ClotheViewModel{};
                                    
            ViewBag.TC = new SelectList(await dbTypeClothe.GetItemListAsync(), "Id", "Name");
            ViewBag.M = new SelectList(await dbMaterial.GetItemListAsync(), "Id", "Name");
            ViewBag.B = new SelectList(await dbBrand.GetItemListAsync(), "Id", "Name");
            ViewBag.G = new SelectList(await dbGender.GetItemListAsync(), "Id", "Name");

            return View(clotheViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateClothe(ClotheViewModel clotheViewModel, string redirectUrl)
        {
            ViewBag.tables = ListTables();
            if (!ModelState.IsValid)
            {
                return View(clotheViewModel);
            }

            var clothe = new Clothe
            {
                Name = clotheViewModel.Name,
                TypeClotheID = clotheViewModel.TypeClotheID,
                MaterialID = clotheViewModel.MaterialID,
                BrandID = clotheViewModel.BrandID,
                GenderID = clotheViewModel.GenderID,
                DnTStart = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                Price = clotheViewModel.Price,
                Info = clotheViewModel.Info
            };

            await dbClothe.CreateAsync(clothe);
            return RedirectToAction("ListClothes", "Admin");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreatePicture()
        {
            ViewBag.tables = ListTables();         
            ViewBag.Clothes = new SelectList(dbClothe.GetItemList().OrderByDescending(c => c.Id), "Id", "Name");
            ViewBag.Picturetypes = new SelectList(await dbPictureType.GetItemListAsync(), "Id", "Name");
            
            return View();           
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreatePicture(Picture pic, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                pic.Image = imageData;

                await dbPicture.CreateAsync(pic);
               

                return RedirectToAction("ListPictures");
            }
            return View(pic);
        }

        //Edits

        [HttpGet]
        public async Task<ActionResult> EditClothe(int id)
        {
            ViewBag.TC = new SelectList(await dbTypeClothe.GetItemListAsync(), "Id", "Name");
            ViewBag.M = new SelectList(await dbMaterial.GetItemListAsync(), "Id", "Name");
            ViewBag.B = new SelectList(await dbBrand.GetItemListAsync(), "Id", "Name");
            ViewBag.G = new SelectList(await dbGender.GetItemListAsync(), "Id", "Name");
            ViewBag.tables = ListTables();
            var clothe = await dbClothe.GetItemAsync(id);
            var clotheViewModel = new ClotheViewModel
            {
                Id = clothe.Id,
                Name = clothe.Name,
                TypeClotheID = clothe.TypeClotheID,
                MaterialID = clothe.MaterialID,
                BrandID = clothe.BrandID,
                Price = clothe.Price,
                Info = clothe.Info,


            };
            return View(clotheViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditClothe(ClotheViewModel clotheViewModel)
        {
            ViewBag.tables = ListTables();
            if (!ModelState.IsValid)
            {
                return View(clotheViewModel);
            }
            var clothe = await dbClothe.GetItemAsync(clotheViewModel.Id);
            if (clothe != null)
            {
                clothe.Name = clotheViewModel.Name;
                clothe.TypeClotheID = clotheViewModel.TypeClotheID;
                clothe.MaterialID = clotheViewModel.MaterialID;
                clothe.BrandID = clotheViewModel.BrandID;
                clothe.DnTStart = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                clothe.Price = clotheViewModel.Price;
                clothe.Info = clotheViewModel.Info;
                await dbClothe.UpdateAsync(clothe);
            }

            return RedirectToAction("ListClothes", "Admin");
        }
        //Deletes
        public async Task<ActionResult> DeleteClothe(int id)
        {
            await dbClothe.DeleteAsync(id);
            await dbClothe.SaveAsync();
            return RedirectToAction("ListClothes");
        }
        public async Task<ActionResult> DeletePicture(int id)
        {
            await dbPicture.DeleteAsync(id);
            await dbPicture.SaveAsync();
            return RedirectToAction("ListPictures");
        }


    }
}