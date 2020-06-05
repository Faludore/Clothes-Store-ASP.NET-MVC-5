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
    public class PictureTypeRepository : IRepository<PictureType>
    {
        private ApplicationDbContext db;

        public PictureTypeRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<PictureType>> GetItemListAsync()
        {
            var result = new List<PictureType>();
            using (db = new ApplicationDbContext())
            {
                result = await db.PictureTypes.ToListAsync();
            }
            return result;
        }
        public List<PictureType> GetItemList()
        {
            var result = new List<PictureType>();
            using (db = new ApplicationDbContext())
            {
                result = db.PictureTypes.ToList();
            }
            return result;
        }
        // получение одного объекта по id
        public async Task<PictureType> GetItemAsync(int id)
        {
            PictureType result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.PictureTypes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<PictureType> CreateAsync(PictureType item)
        {
            PictureType result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.PictureTypes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<PictureType> UpdateAsync(PictureType item)
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
            PictureType item = await db.PictureTypes.FindAsync(id);
            if (item != null)
                db.PictureTypes.Remove(item);
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
