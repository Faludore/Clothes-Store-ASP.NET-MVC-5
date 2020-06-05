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
    public class FavoriteRepository : IRepository<Favorite>
    {
        private ApplicationDbContext db;

        public FavoriteRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Favorite>> GetItemListAsync()
        {
            var result = new List<Favorite>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Favorites.ToListAsync();
                result = db.Favorites.Include(p => p.Clothe).ToList();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Favorite> GetItemAsync(int id)
        {
            Favorite result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Favorites.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Favorite> CreateAsync(Favorite item)
        {
            Favorite result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Favorites.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Favorite> UpdateAsync(Favorite item)
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
            Favorite item = await db.Favorites.FindAsync(id);
            if (item != null)
                db.Favorites.Remove(item);
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

        public List<Favorite> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
