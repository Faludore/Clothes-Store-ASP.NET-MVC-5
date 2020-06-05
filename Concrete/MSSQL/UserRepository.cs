using StoreIdent.Interface;
using StoreIdent.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StoreIdent.Concrete.MSSQL
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext db;

        public UserRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<ApplicationUser>> GetItemListAsync()
        {
            var result = new List<ApplicationUser>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Users.ToListAsync();              
            }
            return result;
        }

        Task<ApplicationUser> IRepository<ApplicationUser>.GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> CreateAsync(ApplicationUser item)
        {
            throw new NotImplementedException();
        }
        

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser item)
        {
            using(db = new ApplicationDbContext())
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return item;
        }

        Task IRepository<ApplicationUser>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IRepository<ApplicationUser>.SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}