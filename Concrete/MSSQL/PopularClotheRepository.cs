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
    public class PopularClotheRepository : IRepository<PopularClothe>
    {
        private ApplicationDbContext db;

        public PopularClotheRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<PopularClothe>> GetItemListAsync()
        {
            var result = new List<PopularClothe>();
            using (db = new ApplicationDbContext())
            {
                result = await db.PopularClothes.ToListAsync();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<PopularClothe> GetItemAsync(int id)
        {
            PopularClothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.PopularClothes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<PopularClothe> CreateAsync(PopularClothe item)
        {
            PopularClothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.PopularClothes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<PopularClothe> UpdateAsync(PopularClothe item)
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
            PopularClothe item = await db.PopularClothes.FindAsync(id);
            if (item != null)
                db.PopularClothes.Remove(item);
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

        public List<PopularClothe> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
