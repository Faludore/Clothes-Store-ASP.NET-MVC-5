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
    public class TypeClotheRepository : IRepository<TypeClothe>
    {
        private ApplicationDbContext db;
        public TypeClotheRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<TypeClothe>> GetItemListAsync()
        {
            var result = new List<TypeClothe>();
            using (db = new ApplicationDbContext())
            {
                result = await db.TypeClothes.ToListAsync();
               
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<TypeClothe> GetItemAsync(int id)
        {
            TypeClothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.TypeClothes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<TypeClothe> CreateAsync(TypeClothe item)
        {
            TypeClothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.TypeClothes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<TypeClothe> UpdateAsync(TypeClothe item)
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
            TypeClothe typeClothe = await db.TypeClothes.FindAsync(id);
            if (typeClothe != null)
                db.TypeClothes.Remove(typeClothe);
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

        public List<TypeClothe> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}