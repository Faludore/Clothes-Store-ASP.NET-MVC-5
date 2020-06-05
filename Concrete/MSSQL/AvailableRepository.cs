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
    public class AvailableRepository : IRepository<Available>
    {
        private ApplicationDbContext db;

        public AvailableRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Available>> GetItemListAsync()
        {
            var result = new List<Available>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Availables.ToListAsync();
                result = db.Availables.Include(p => p.Clothe).ToList();
                result = db.Availables.Include(p => p.Size).ToList();
               
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Available> GetItemAsync(int id)
        {
            Available result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Availables.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Available> CreateAsync(Available item)
        {
            Available result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Availables.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Available> UpdateAsync(Available item)
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
            Available item = await db.Availables.FindAsync(id);
            if (item != null)
                db.Availables.Remove(item);
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

        public List<Available> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
