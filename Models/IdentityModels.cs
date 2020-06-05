using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoreIdent.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        //public int ClientID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DnTBD { get; set; }
        //public string Number { get; set; }       
        public string DnTReg { get; set; }
        public string DnTDel { get; set; }
        public int CurrentBucket { get; set; }
        //public string Email { get; set; }

        //public int RoleID { get; set; }
        //public Role Role { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }
        public DbSet<Clothe> Clothes { get; set; }
        public DbSet<TypeClothe> TypeClothes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Available> Availables { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderOne> OrderOnes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PictureType> PictureTypes { get; set; }
        public DbSet<PopularClothe> PopularClothes { get; set; }
        public DbSet<PopularClotheForUser> PopularClotheForUsers { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<TypeBuy> TypeBuys { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}