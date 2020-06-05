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
    public class TypeBuyRepository : IRepository<TypeBuy>
    {
        private ApplicationDbContext db;

        public TypeBuyRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<TypeBuy>> GetItemListAsync()
        {
            var result = new List<TypeBuy>();
            using (db = new ApplicationDbContext())
            {
                result = await db.TypeBuys.ToListAsync();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<TypeBuy> GetItemAsync(int id)
        {
            TypeBuy result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.TypeBuys.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<TypeBuy> CreateAsync(TypeBuy item)
        {
            TypeBuy result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.TypeBuys.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<TypeBuy> UpdateAsync(TypeBuy item)
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
            TypeBuy item = await db.TypeBuys.FindAsync(id);
            if (item != null)
                db.TypeBuys.Remove(item);
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

        public List<TypeBuy> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}
