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
    public class MaterialRepository : IRepository<Material>
    {
        private ApplicationDbContext db;

        public MaterialRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Material>> GetItemListAsync()
        {
            var result = new List<Material>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Materials.ToListAsync();              
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Material> GetItemAsync(int id)
        {
            Material result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Materials.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Material> CreateAsync(Material item)
        {
            Material result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Materials.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Material> UpdateAsync(Material item)
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
            Material material = await db.Materials.FindAsync(id);
            if (material != null)
                db.Materials.Remove(material);
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

        public List<Material> GetItemList()
        {
            throw new NotImplementedException();
        }
    }
}