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
    public class GenderRepository : IRepository<Gender>
    {
        private ApplicationDbContext db;

        public GenderRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Gender>> GetItemListAsync()
        {
            var result = new List<Gender>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Genders.ToListAsync();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Gender> GetItemAsync(int id)
        {
            Gender result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Genders.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Gender> CreateAsync(Gender item)
        {
            Gender result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Genders.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Gender> UpdateAsync(Gender item)
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
            Gender gender = await db.Genders.FindAsync(id);
            if (gender != null)
                db.Genders.Remove(gender);
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

        public List<Gender> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
