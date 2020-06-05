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
    public class BrandRepository : IRepository<Brand>
    {
        private ApplicationDbContext db;

        public BrandRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Brand>> GetItemListAsync()
        {
            var result = new List<Brand>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Brands.ToListAsync();              
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Brand> GetItemAsync(int id)
        {
            Brand result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Brands.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Brand> CreateAsync(Brand item)
        {
            Brand result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Brands.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Brand> UpdateAsync(Brand item)
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
            Brand brand = await db.Brands.FindAsync(id);
            if (brand != null)
                db.Brands.Remove(brand);
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

        public List<Brand> GetItemList()
        {
            var result = new List<Brand>();
            using (db = new ApplicationDbContext())
            {
                result = db.Brands.ToList();            
            }
            return result;
        }
    }
}
