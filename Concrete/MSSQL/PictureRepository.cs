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
    public class PictureRepository : IRepository<Picture>
    {
        private ApplicationDbContext db;

        public PictureRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Picture>> GetItemListAsync()
        {
            var result = new List<Picture>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Pictures.ToListAsync();
                result = db.Pictures.Include(p => p.Clothe).ToList();
                result = db.Pictures.Include(p => p.PictureType).ToList();
            }
            return result;
        }
        public List<Picture> GetItemList()
        {
            var result = new List<Picture>();
            using (db = new ApplicationDbContext())
            {
                result = db.Pictures.ToList();
                result = db.Pictures.Include(p => p.Clothe).ToList();
                result = db.Pictures.Include(p => p.PictureType).ToList();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Picture> GetItemAsync(int id)
        {
            Picture result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Pictures.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Picture> CreateAsync(Picture item)
        {
            Picture result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Pictures.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Picture> UpdateAsync(Picture item)
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
            Picture item = await db.Pictures.FindAsync(id);
            if (item != null)
            {
                db.Pictures.Remove(item);
                await this.SaveAsync();
            }
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
