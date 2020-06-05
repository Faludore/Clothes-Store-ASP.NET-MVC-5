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
    public class SizeRepository : IRepository<Size>
    {
        private ApplicationDbContext db;

        public SizeRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Size>> GetItemListAsync()
        {
            var result = new List<Size>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Sizes.ToListAsync();
            }
            return result;
        }
        public List<Size> GetItemList()
        {
            var result = new List<Size>();
            using (db = new ApplicationDbContext())
            {
                result =  db.Sizes.ToList();
            }
            return result;
        }
        // получение одного объекта по id
        public async Task<Size> GetItemAsync(int id)
        {
            Size result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Sizes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Size> CreateAsync(Size item)
        {
            Size result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Sizes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Size> UpdateAsync(Size item)
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
            Size item = await db.Sizes.FindAsync(id);
            if (item != null)
                db.Sizes.Remove(item);
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

    
    }
}
