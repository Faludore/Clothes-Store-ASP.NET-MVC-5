using StoreIdent.Concrete.MSSQL;
using StoreIdent.Interface;
using StoreIdent.Models;
using StoreIdent.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Data.Entity;

namespace StoreIdent.Controllers
{
    public class ScaseController : Controller
    {
        public int pagesize;

        IRepository<Clothe> dbClothe;
        IRepository<Picture> dbPicture;
        IRepository<PictureType> dbPictureType;
        IRepository<Brand> dbBrand;
        IRepository<TypeClothe> dbTypeClothe;
        IRepository<Size> dbSize;
        IRepository<Available> dbAvailable;
        IRepository<Gender> dbGender;
        IRepository<Material> dbMaterial;
        IRepository<OrderOne> dbOrderOne;
        IRepository<PopularClotheForUser> dbPopularClotheForUser;
        IRepository<Favorite> dbFavorite;
        IRepository<PopularClothe> dbPopularClothe;
        IRepository<Order> dbOrder;
        IRepository<TypeBuy> dbTypeBuy;
        //telegram
        static ITelegramBotClient botClient;


        public static bool my_checkinternet()
        {
            try
            {
                var me = botClient.GetMeAsync().Result;
                return true;
            }
            catch
            {

                return false;
            }
        }
        private async Task Bot_SendMessage(string id, string sms)
        {
            await botClient.SendTextMessageAsync(chatId: id, text: sms);

        }

        //

        public ScaseController()
        {
            dbClothe = new ClotheRepository();
            dbPicture = new PictureRepository();
            dbPictureType = new PictureTypeRepository();
            dbBrand = new BrandRepository();
            dbTypeClothe = new TypeClotheRepository();
            dbSize = new SizeRepository();
            dbAvailable = new AvailableRepository();
            dbGender = new GenderRepository();
            dbMaterial = new MaterialRepository();
            dbOrderOne = new OrderOneRepository();
            dbPopularClotheForUser = new PopularClotheForUserRepository();
            dbFavorite = new FavoriteRepository();
            dbPopularClothe = new PopularClotheRepository();
            dbOrder = new OrderRepository();
            dbTypeBuy = new TypeBuyRepository();

            botClient = new TelegramBotClient("1197569417:AAEfpJvha4nnuCRWuHMJ_Usd235e0Q4MKdA");
        }




        public ActionResult Main()
        {
            return View();
        }
        public async Task<ActionResult> Shop()
        {
            ViewBag.PageSort = new List<SelectListItem>()
            {
                new SelectListItem() { Value="1", Text= "Default sorting" },
                new SelectListItem() { Value="2", Text= "Sort by popularity" },
                new SelectListItem() { Value="3", Text= "Sort by rating" },
                new SelectListItem() { Value="4", Text= "Sort by newness" },
                new SelectListItem() { Value="5", Text= "Sort by price: low to high" },
                new SelectListItem() { Value="6", Text= "Sort by price: high to low" },
            };
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3" },
                new SelectListItem() { Value="6", Text= "6" },
                new SelectListItem() { Value="9", Text= "9" },
            };

            var formin = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price ascending
                          select c).First().Price;
            var fofmax = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price descending
                          select c).First().Price;
            ViewBag.Min = formin;
            ViewBag.Max = fofmax;

            var some = from brands in await dbBrand.GetItemListAsync()
                       select brands;
            ViewBag.Brands = some;
            var categories = from c in await dbTypeClothe.GetItemListAsync()
                             select c;
            ViewBag.Categories = categories;

            var selectedPictures = await GetPictures();
            var favorites = from pc in await dbPopularClothe.GetItemListAsync()
                            orderby pc.Views descending
                            select pc;

            var pic = (from sp in selectedPictures
                       join f in favorites
                       on sp.ClotheID equals f.ClotheID
                       join cl in dbClothe.GetItemList()
                       on sp.ClotheID equals cl.Id
                       select new { f.Id, cl.Name, sp.Image, cl.Price, cl.Info, ClotheID = cl.Id }).Take(4);

            List<WishListViewModel> wishlistViewModel = new List<WishListViewModel>();
            foreach (var item in pic)
            {
                WishListViewModel cartView = new WishListViewModel() { Id = item.Id, Name = item.Name.Substring(0, 20) + "...", Image = item.Image, Price = item.Price, Info = item.Info, ClotheID = item.ClotheID };
                wishlistViewModel.Add(cartView);
            }
            ViewBag.PopularClothes = wishlistViewModel;

            return View();
        }

        public async Task<ActionResult> Men()
        {
            ViewBag.Gender = "Men";
            ViewBag.GenderID = 1;
            ViewBag.PageSort = new List<SelectListItem>()
            {
                new SelectListItem() { Value="1", Text= "Default sorting" },
                new SelectListItem() { Value="2", Text= "Sort by popularity" },
                new SelectListItem() { Value="3", Text= "Sort by rating" },
                new SelectListItem() { Value="4", Text= "Sort by newness" },
                new SelectListItem() { Value="5", Text= "Sort by price: low to high" },
                new SelectListItem() { Value="6", Text= "Sort by price: high to low" },
            };
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3" },
                new SelectListItem() { Value="6", Text= "6" },
                new SelectListItem() { Value="9", Text= "9" },
            };

            var formin = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price ascending
                          select c).First().Price;
            var fofmax = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price descending
                          select c).First().Price;
            ViewBag.Min = formin;
            ViewBag.Max = fofmax;

            var some = from brands in await dbBrand.GetItemListAsync()
                       select brands;
            ViewBag.Brands = some;
            var categories = from c in await dbTypeClothe.GetItemListAsync()
                             select c;
            ViewBag.Categories = categories;

            var selectedPictures = await GetPictures();
            var favorites = from pc in await dbPopularClothe.GetItemListAsync()
                            orderby pc.Views descending
                            select pc;

            var pic = (from sp in selectedPictures
                       join f in favorites
                       on sp.ClotheID equals f.ClotheID
                       join cl in dbClothe.GetItemList()
                       on sp.ClotheID equals cl.Id
                       select new { f.Id, cl.Name, sp.Image, cl.Price, cl.Info, ClotheID = cl.Id }).Take(4);

            List<WishListViewModel> wishlistViewModel = new List<WishListViewModel>();
            foreach (var item in pic)
            {
                WishListViewModel cartView = new WishListViewModel() { Id = item.Id, Name = item.Name.Substring(0, 20) + "...", Image = item.Image, Price = item.Price, Info = item.Info, ClotheID = item.ClotheID };
                wishlistViewModel.Add(cartView);
            }
            ViewBag.PopularClothes = wishlistViewModel;

            return View();
        }
        public async Task<ActionResult> Women()
        {
            ViewBag.Gender = "Women";
            ViewBag.GenderID = 2;
            ViewBag.PageSort = new List<SelectListItem>()
            {
                new SelectListItem() { Value="1", Text= "Default sorting" },
                new SelectListItem() { Value="2", Text= "Sort by popularity" },
                new SelectListItem() { Value="3", Text= "Sort by rating" },
                new SelectListItem() { Value="4", Text= "Sort by newness" },
                new SelectListItem() { Value="5", Text= "Sort by price: low to high" },
                new SelectListItem() { Value="6", Text= "Sort by price: high to low" },
            };
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3" },
                new SelectListItem() { Value="6", Text= "6" },
                new SelectListItem() { Value="9", Text= "9" },
            };

            var formin = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price ascending
                          select c).First().Price;
            var fofmax = (from c in await dbClothe.GetItemListAsync()
                          orderby c.Price descending
                          select c).First().Price;
            ViewBag.Min = formin;
            ViewBag.Max = fofmax;

            var some = from brands in await dbBrand.GetItemListAsync()
                       select brands;
            ViewBag.Brands = some;
            var categories = from c in await dbTypeClothe.GetItemListAsync()
                             select c;
            ViewBag.Categories = categories;

            var selectedPictures = await GetPictures();
            var favorites = from pc in await dbPopularClothe.GetItemListAsync()
                            orderby pc.Views descending
                            select pc;

            var pic = (from sp in selectedPictures
                       join f in favorites
                       on sp.ClotheID equals f.ClotheID
                       join cl in dbClothe.GetItemList()
                       on sp.ClotheID equals cl.Id
                       select new { f.Id, cl.Name, sp.Image, cl.Price, cl.Info, ClotheID = cl.Id }).Take(4);

            List<WishListViewModel> wishlistViewModel = new List<WishListViewModel>();
            foreach (var item in pic)
            {
                WishListViewModel cartView = new WishListViewModel() { Id = item.Id, Name = item.Name.Substring(0, 20) + "...", Image = item.Image, Price = item.Price, Info = item.Info, ClotheID = item.ClotheID };
                wishlistViewModel.Add(cartView);
            }
            ViewBag.PopularClothes = wishlistViewModel;

            return View();
        }

        public async Task<ActionResult> ClotheFilter(int? page, int? PageSize, decimal? max, decimal? min, int? pageSort, string brands, string categories, int? gender)
        {
            ViewBag.psort = (pageSort ?? 3);
            var Pictures = await dbPictureType.GetItemListAsync();
            var employee = Pictures.OrderBy(e => e.Id).Count();
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 3);

            ViewBag.psize = pagesize;
            ViewBag.Count = employee;

            var typemain = from picturetypes in await dbPictureType.GetItemListAsync()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in (await dbPicture.GetItemListAsync())
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;

            foreach (Picture ordero in selectedPictures)
            {
                if (ordero.Clothe.Name.Length > 23)
                    ordero.Clothe.Name = ordero.Clothe.Name.Substring(0, 23) + "...";
            }

            selectedPictures = from pictures in (selectedPictures)
                               where pictures.Clothe.Price >= min && pictures.Clothe.Price <= max
                               orderby pictures.ClotheID
                               select pictures;
            if (pageSort == 3)
            {
                selectedPictures = (from pictures in (selectedPictures)
                                    select pictures).OrderByDescending(item => item.Clothe.Mark);
            }
            else if (pageSort == 4)
            {
                selectedPictures = from pictures in (selectedPictures)
                                   orderby pictures.Clothe.DnTStart descending
                                   select pictures;
            }
            else if (pageSort == 5)
            {
                selectedPictures = (from pictures in (selectedPictures)
                                    select pictures).OrderBy(item => item.Clothe.Price);
            }
            else if (pageSort == 6)
            {
                selectedPictures = (from pictures in (selectedPictures)
                                    select pictures).OrderByDescending(item => item.Clothe.Price);
            }

            selectedPictures.ToList();

            List<Picture> pic = selectedPictures.ToList();
            if (brands != null && brands.Length > 0)
            {
                string[] arr_brands = brands.Split(',');

                ViewBag.curBrand = brands;
                foreach (var pictures in selectedPictures)
                {
                    if (!arr_brands.Contains(Convert.ToString(pictures.Clothe.BrandID)))
                    {
                        pic.Remove(pictures);
                    }

                }
            }
            if (categories != null && categories.Length > 0)
            {
                string[] arr_categories = categories.Split(',');

                ViewBag.curCategory = categories;
                foreach (var pictures in selectedPictures)
                {
                    if (!arr_categories.Contains(Convert.ToString(pictures.Clothe.TypeClotheID)))
                    {
                        pic.Remove(pictures);
                    }

                }
            }
            if (gender != null && gender > 0)
            {
                foreach (var pictures in selectedPictures)
                {
                    if (pictures.Clothe.GenderID != gender)
                    {
                        pic.Remove(pictures);
                    }

                }
            }
            return PartialView(pic.ToList().ToPagedList(pageNumber, pagesize));

        }

        [HttpGet]
        public async Task<ActionResult> CreateOrderOne(int id)
        {

            ViewBag.Pic = from pictures in (await dbPicture.GetItemListAsync())
                          where pictures.ClotheID == id
                          select pictures;
            Clothe clothe = await dbClothe.GetItemAsync(id);
            if (clothe == null)
            {
                return HttpNotFound();
            }
            var sss = from s in await dbSize.GetItemListAsync()
                      join pet in (from a in await dbAvailable.GetItemListAsync()
                                   where a.ClotheID == id
                                   select a) on s.Id equals pet.SizeID
                      select new { s.Id, s.Name };

            ViewBag.Sizes = new SelectList(sss, "Id", "Name");
            ViewBag.Clothe = clothe;
            ViewBag.Type = await dbTypeClothe.GetItemAsync(clothe.TypeClotheID);
            ViewBag.Brand = await dbBrand.GetItemAsync(clothe.BrandID);
            ViewBag.Gender = await dbGender.GetItemAsync(clothe.GenderID);
            ViewBag.Material = await dbMaterial.GetItemAsync(clothe.MaterialID);

            var typemain = from picturetypes in dbPictureType.GetItemList()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in dbPicture.GetItemList()
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;



            string name = User.Identity.Name;

            var lastseen = from sp in selectedPictures
                           join cl in await dbClothe.GetItemListAsync()
                           on sp.ClotheID equals cl.Id
                           join ls in (from a in await dbPopularClotheForUser.GetItemListAsync()
                                       where a.UserEmail == name
                                       select a)
                           on cl.Id equals ls.ClotheID
                           select new { cl.Id, cl.Name, sp.Image, cl.Price, cl.Mark, ls.DnT };




            List<LastSeenViewModel> lastseenViewModel = new List<LastSeenViewModel>();
            foreach (var item in lastseen)
            {
                if (item.Id != id)
                {
                    LastSeenViewModel lastseenView = new LastSeenViewModel() { Id = item.Id, Name = item.Name.Substring(0, 13) + "...", Image = item.Image, Price = item.Price, Mark = item.Mark, DnT = item.DnT };
                    lastseenViewModel.Add(lastseenView);
                }
            }
            ViewBag.LastSeen = lastseenViewModel.OrderByDescending(item => item.DnT).Take(8);

            int pos = 0;
            bool chk = false;
            List<PopularClotheForUser> popularClotheForUser = await dbPopularClotheForUser.GetItemListAsync();
            foreach (var item in popularClotheForUser)
            {
                if (item.ClotheID == id && item.UserEmail == name)
                {
                    chk = true;
                    break;
                }
                pos++;
            }
            PopularClotheForUser pc;
            if (chk)
            {
                popularClotheForUser[pos].DnT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                await dbPopularClotheForUser.UpdateAsync(popularClotheForUser[pos]);
            }
            else if (!chk)
            {
                pc = new PopularClotheForUser() { ClotheID = id, UserEmail = name, DnT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") };
                await dbPopularClotheForUser.CreateAsync(pc);
            }

            ////
            chk = false;
            pos = 0;
            List<PopularClothe> popularClothes = await dbPopularClothe.GetItemListAsync();
            foreach (var item in popularClothes)
            {
                if (item.ClotheID == id)
                {
                    chk = true;
                    break;
                }
                pos++;
            }
            if (chk)
            {
                popularClothes[pos].Views++;
                await dbPopularClothe.UpdateAsync(popularClothes[pos]);
            }
            else if (!chk)
            {
                PopularClothe popularClothe = new PopularClothe() { ClotheID = id, Views = 1 };
                await dbPopularClothe.CreateAsync(popularClothe);
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateOrderOne(int clotheID, int sizeID, int amount, decimal price)
        {
            OrderOne orderone = new OrderOne()
            {
                OrderID = null,
                ClotheID = clotheID,
                Price = price * amount,
                Amount = amount,
                UserEmail = User.Identity.Name,
                SizeID = sizeID
            };

            //var some = (from av in await dbAvailable.GetItemListAsync()
            //                      where av.ClotheID == clotheID && av.SizeID == sizeID
            //                      select av).FirstOrDefault();
            //Available available = await dbAvailable.GetItemAsync(some.Id);

            //if (available != null)
            //{
            //    available.Amount = available.Amount - amount;             
            //    await dbAvailable.UpdateAsync(available);
            //}

            await dbOrderOne.CreateAsync(orderone);
            return RedirectToAction("CreateOrderOne", "Scase", orderone.ClotheID);
        }
        public async Task<JsonResult> GetAmountList(int sizeID, int clotheID)
        {
            IEnumerable<int> some = from av in await dbAvailable.GetItemListAsync()
                                    where av.ClotheID == clotheID && av.SizeID == sizeID
                                    select av.Amount;

            int am = some.FirstOrDefault();

            var amount = new List<SelectListItem>();

            for (int i = 1; i <= am; i++)
            {
                amount.Add(new SelectListItem() { Value = Convert.ToString(i) });
            }
            return Json(amount, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public async Task<ActionResult> Mark(int id, int mark)
        {
            var clothe = await dbClothe.GetItemAsync(id);
            if (clothe != null)
            {
                if (clothe.Mark == 0) clothe.Mark = mark;
                else clothe.Mark = Convert.ToInt32((mark + clothe.Mark) / 2);
                await dbClothe.UpdateAsync(clothe);
            }
            return RedirectToAction("CreateOrderOne", "Scase", new { id });
        }
        [Authorize]
        public int GetCurrentBucket()
        {
            string name = User.Identity.Name;
            var orderone = from or in dbOrderOne.GetItemList()
                           where or.OrderID == null && or.UserEmail == name
                           select or;
            int total = 0;
            if (orderone != null)
            {
                foreach (var item in orderone)
                {
                    total++;
                }
            }
            else
            {
                total = 0;
            }

            return total;
        }
        [Authorize]
        public ActionResult GetCart()
        {
            string name = User.Identity.Name;
            var Pictures = dbPictureType.GetItemList();
            var employee = Pictures.OrderBy(e => e.Id).Count();
            var typemain = from picturetypes in dbPictureType.GetItemList()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in dbPicture.GetItemList()
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;

            var orderone = from or in dbOrderOne.GetItemList()
                           where or.OrderID == null && or.UserEmail == name
                           select or;

            var pic = from sp in selectedPictures
                      join or in orderone
                      on sp.ClotheID equals or.ClotheID
                      join cl in dbClothe.GetItemList()
                      on sp.ClotheID equals cl.Id
                      join sz in dbSize.GetItemList()
                      on or.SizeID equals sz.Id
                      select new { Id = or.Id, ClotheID = cl.Id, Image = sp.Image, Name = cl.Name, Price = or.Price, Amount = or.Amount, SizeName = sz.Name };

            decimal total = 0;

            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            foreach (var item in pic)
            {
                CartViewModel cartView = new CartViewModel() { Id = item.Id, ClotheID = item.ClotheID, Name = item.Name.Substring(0, 13) + "...", Image = item.Image, Price = item.Price, Amount = item.Amount, Size = item.SizeName };
                cartViewModel.Add(cartView);
                total += item.Price;
            }

            ViewBag.CB = GetCurrentBucket();

            ViewBag.Pic = cartViewModel;
            ViewBag.TotalSum = total;
            return PartialView();
        }
        public async Task<ActionResult> GetCartList()
        {
            string name = User.Identity.Name;
            var selectedPictures = from pictures in await dbPicture.GetItemListAsync()
                                   where pictures.PictureType.Name == "main"
                                   orderby pictures.ClotheID
                                   select pictures;

            var orderone = from or in dbOrderOne.GetItemList()
                           where or.OrderID == null && or.UserEmail == name
                           select or;

            var pic = from sp in selectedPictures
                      join or in orderone
                      on sp.ClotheID equals or.ClotheID
                      join cl in dbClothe.GetItemList()
                      on sp.ClotheID equals cl.Id
                      join sz in dbSize.GetItemList()
                      on or.SizeID equals sz.Id
                      select new { Id = or.Id, ClotheID = cl.Id, Image = sp.Image, Name = cl.Name, Price = or.Price, Amount = or.Amount, SizeName = sz.Name };

            decimal total = 0;

            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            foreach (var item in pic)
            {
                CartViewModel cartView = new CartViewModel() { Id = item.Id, ClotheID = item.ClotheID, Name = item.Name.Substring(0, 13) + "...", Image = item.Image, Price = item.Price, Amount = item.Amount, Size = item.SizeName };
                cartViewModel.Add(cartView);
                total += item.Price;
            }

            ViewBag.CB = GetCurrentBucket();
            ViewBag.TotalSum = total;


            return View(cartViewModel);
        }

        public async Task<ActionResult> DeleteCart(int id)
        {
            await dbOrderOne.DeleteAsync(id);
            await dbOrderOne.SaveAsync();
            return RedirectToAction("GetCart", "Scace");
        }
        public async Task DeleteCartList(int id)
        {
            await dbOrderOne.DeleteAsync(id);
            await dbOrderOne.SaveAsync();

        }
        [HttpGet]
        public async Task<ActionResult> CheckOut()
        {
            ViewBag.TypeBuy = new SelectList(await dbTypeBuy.GetItemListAsync(), "Id", "Name");
            string name = User.Identity.Name;
            var typemain = from picturetypes in dbPictureType.GetItemList()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in dbPicture.GetItemList()
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;

            var orderone = from or in dbOrderOne.GetItemList()
                           where or.OrderID == null && or.UserEmail == name
                           select or;

            var pic = from sp in selectedPictures
                      join or in orderone
                      on sp.ClotheID equals or.ClotheID
                      join cl in dbClothe.GetItemList()
                      on sp.ClotheID equals cl.Id
                      join sz in dbSize.GetItemList()
                      on or.SizeID equals sz.Id
                      select new { Id = or.Id, ClotheID = cl.Id, Image = sp.Image, Name = cl.Name, Price = or.Price, Amount = or.Amount, SizeName = sz.Name };

            decimal total = 0;

            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            foreach (var item in pic)
            {
                CartViewModel cartView = new CartViewModel() { Id = item.Id, ClotheID = item.ClotheID, Name = item.Name.Substring(0, 13) + "...", Image = item.Image, Price = item.Price, Amount = item.Amount, Size = item.SizeName };
                cartViewModel.Add(cartView);
                total += item.Price * item.Amount;
            }

            ViewBag.CB = GetCurrentBucket();
            ViewBag.TotalSum = total;
            ViewBag.CartViewModel = cartViewModel;

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CheckOut(Order order)
        {
            decimal total = 0;
            string name = User.Identity.Name;
            IEnumerable<OrderOne> orderones = await dbOrderOne.GetItemListAsync();
            foreach (var item in orderones)
            {
                if (item.OrderID == null && item.UserEmail == name)
                {
                    total += item.Price;
                }
            }

            order.DnT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            order.TotalSum = 0.01m;
            order.UserEmail = User.Identity.Name;
            order.TotalSum = total;
            await dbOrder.CreateAsync(order);

            foreach (var item in orderones)
            {
                if (item.OrderID == null && item.UserEmail == name)
                {
                    item.OrderID = order.Id;
                    await dbOrderOne.UpdateAsync(item);
                }
            }

            //telegram message
            if (my_checkinternet())
            {
                await Bot_SendMessage("425899743", "Order_Id: " + order.Id + " FirstName: " + order.FirstName + " LastName: " + order.LastName);
            }

            return RedirectToAction("Shop", "Scase");
        }
        public async Task AddToWishlist(int id)
        {
            string name = User.Identity.Name;
            List<Favorite> lf = await dbFavorite.GetItemListAsync();
            bool chk = true;
            foreach (var item in lf)
            {
                if (item.ClotheID == id && item.UserEmail == name) chk = false;
            }
            if (chk)
            {
                Favorite f = new Favorite() { ClotheID = id, UserEmail = name };
                await dbFavorite.CreateAsync(f);
            }

        }
        public async Task<ActionResult> Wishlist()
        {
            string name = User.Identity.Name;

            var Pictures = dbPictureType.GetItemList();
            var employee = Pictures.OrderBy(e => e.Id).Count();
            var typemain = from picturetypes in dbPictureType.GetItemList()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in dbPicture.GetItemList()
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;

            var favorites = from or in await dbFavorite.GetItemListAsync()
                            where or.UserEmail == name
                            select or;

            var pic = from sp in selectedPictures
                      join f in favorites
                      on sp.ClotheID equals f.ClotheID
                      join cl in dbClothe.GetItemList()
                      on sp.ClotheID equals cl.Id
                      select new { f.Id, cl.Name, sp.Image, cl.Price, cl.Info, ClotheID = cl.Id };


            List<WishListViewModel> wishlistViewModel = new List<WishListViewModel>();
            foreach (var item in pic)
            {
                WishListViewModel cartView = new WishListViewModel() { Id = item.Id, Name = item.Name.Substring(0, 20) + "...", Image = item.Image, Price = item.Price, Info = item.Info, ClotheID = item.ClotheID };
                wishlistViewModel.Add(cartView);
            }


            return View(wishlistViewModel);
        }
        public async Task DeleteWishList(int id)
        {
            await dbFavorite.DeleteAsync(id);
            await dbFavorite.SaveAsync();

        }
        public async Task<List<Picture>> GetPictures()
        {
            var typemain = from picturetypes in await dbPictureType.GetItemListAsync()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in (await dbPicture.GetItemListAsync())
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;
            return selectedPictures.ToList();
        }

        public List<Picture> GetPicturesS()
        {
            var typemain = from picturetypes in  dbPictureType.GetItemList()
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            var selectedPictures = from pictures in  dbPicture.GetItemList()
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   orderby pictures.ClotheID
                                   select pictures;
            return selectedPictures.ToList();
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //ViewBag.search = search;
            //var typemain = from picturetypes in db.PictureTypes
            //               where picturetypes.Name == "main"
            //               select picturetypes.Id;
            //typemain.ToList();

            //var selectedPictures = from pictures in db.Pictures
            //                       where pictures.PictureTypeID == (typemain.FirstOrDefault())
            //                       select pictures;

            //selectedPictures.ToList();
            //selectedPictures.Include(p => p.Clothe).ToList();
            //selectedPictures.Include(p => p.Clothe.Brand);

            //if (!String.IsNullOrEmpty(search))
            //{
            //    selectedPictures = selectedPictures.Where(s => s.Clothe.Name.Contains(search));
            //}

            //return View(selectedPictures.ToList());
            //List<Clothe> clothes = dbClothe.GetItemList();
            //List<Clothe> clothes1 = new List<Clothe>();
            //if (!String.IsNullOrEmpty(search))
            //{
            //    clothes1 = (from c in clothes
            //                where c.Name.Contains(search)
            //                select c).ToList();
            //}
          
            //if (!String.IsNullOrEmpty(search))
            //{              
            //    clothes1 = clothes.Where(c => c.Name.Contains(search)).ToList();                     
            //}
            //List<Picture> selectedPictures = GetPicturesS();
            //if (!String.IsNullOrEmpty(search))
            //{
            //    selectedPictures = selectedPictures.Where(c => c.Clothe.Name.Contains(search)).ToList();
            //}
            //List<Picture> pic = (from sp in selectedPictures
            //                     join c in clothes1
            //                     on sp.ClotheID equals c.Id
            //                     select sp).ToList();

            //return View(clothes1);


            var typemain = from picturetypes in db.PictureTypes
                           where picturetypes.Name == "main"
                           select picturetypes.Id;
            typemain.ToList();

            var selectedPictures = from pictures in db.Pictures
                                   where pictures.PictureTypeID == (typemain.FirstOrDefault())
                                   select pictures;

            selectedPictures.ToList();

            selectedPictures.Include(p => p.Clothe);

            var searchPictures = from spictures in selectedPictures
                                 where spictures.Clothe.Name.Contains(search)
                                 select spictures;

            List<Picture> pic = searchPictures.Include(p => p.Clothe).ToList(); 
            pic.ToList();


            foreach (Picture ordero in pic)
            {
                ordero.Clothe.Name = ordero.Clothe.Name.Substring(0, 17) + "..."; ;
            }
       
            return View(pic.ToList());


        }

        //orer list, change pass, personal info
        public ActionResult MyAcc()
        {
            return View();
        }
        public ActionResult OrdersList()
        {
            string name = User.Identity.Name;

            var orderslist = dbOrder.GetItemList().Where(o => o.UserEmail == name).ToList();
            ViewBag.Count = orderslist.Count;
            return PartialView(orderslist);
        }
        public async Task<ActionResult> GetOneOrder(int id)
        {
            string name = User.Identity.Name;
            ViewBag.OrederID = id;
            var selectedPictures = await GetPictures();
            var orderone = from or in dbOrderOne.GetItemList()
                           where or.OrderID == id && or.UserEmail == name
                           select or;

            var pic = from sp in selectedPictures
                      join or in orderone
                      on sp.ClotheID equals or.ClotheID
                      join cl in dbClothe.GetItemList()
                      on sp.ClotheID equals cl.Id
                      join sz in dbSize.GetItemList()
                      on or.SizeID equals sz.Id
                      select new { Id = or.Id, ClotheID = cl.Id, Image = sp.Image, Name = cl.Name, Price = or.Price, Amount = or.Amount, SizeName = sz.Name };

            decimal total = 0;

            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            foreach (var item in pic)
            {
                CartViewModel cartView = new CartViewModel() { Id = item.Id, ClotheID = item.ClotheID, Name = item.Name.Substring(0, 13) + "...", Image = item.Image, Price = item.Price, Amount = item.Amount, Size = item.SizeName };
                cartViewModel.Add(cartView);
                total += item.Price * item.Amount;
            }

            ViewBag.TotalSum = total;

            return PartialView(cartViewModel);
        }

        public async Task<ActionResult> List()
        {

            var clothes = await dbClothe.GetItemListAsync();
            return View(clothes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clotheViewModel = new ClotheViewModel
            {

            };
            return View(clotheViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Create(ClotheViewModel clotheViewModel, string redirectUrl)
        {
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
                DnTStart = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                Price = clotheViewModel.Price,
                Info = clotheViewModel.Info
            };

            await dbClothe.CreateAsync(clothe);
            return RedirectToAction("List", "Scase");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
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
        public async Task<ActionResult> Edit(ClotheViewModel clotheViewModel)
        {
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

            return RedirectToAction("List", "Scase");
        }





        //protected override void Dispose(bool disposing)
        //{
        //    dbClothe.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}