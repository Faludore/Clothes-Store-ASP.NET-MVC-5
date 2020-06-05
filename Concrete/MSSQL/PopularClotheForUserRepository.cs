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
    public class PopularClotheForUserRepository : IRepository<PopularClotheForUser>
    {
        private ApplicationDbContext db;

        public PopularClotheForUserRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<PopularClotheForUser>> GetItemListAsync()
        {
            var result = new List<PopularClotheForUser>();
            using (db = new ApplicationDbContext())
            {
                result = await db.PopularClotheForUsers.ToListAsync();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<PopularClotheForUser> GetItemAsync(int id)
        {
            PopularClotheForUser result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.PopularClotheForUsers.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<PopularClotheForUser> CreateAsync(PopularClotheForUser item)
        {
            PopularClotheForUser result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.PopularClotheForUsers.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<PopularClotheForUser> UpdateAsync(PopularClotheForUser item)
        {
            using (db = new ApplicationDbContext())
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return item;
        }

        // удаление объекта по id
        public async Task DeleteAsync(int id)
        {
            PopularClotheForUser item = await db.PopularClotheForUsers.FindAsync(id);
            if (item != null)
                db.PopularClotheForUsers.Remove(item);
        }

        // сохранение изменений
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<PopularClotheForUser> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
