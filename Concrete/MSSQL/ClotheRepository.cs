using StoreIdent.Interface;
using StoreIdent.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StoreIdent.Concrete.MSSQL
{
    public class ClotheRepository : IRepository<Clothe>
    {
        private ApplicationDbContext db;
        
        public ClotheRepository()
        {
            this.db = new ApplicationDbContext();          
        }
        
        // получение всех объектов
        public async Task<List<Clothe>> GetItemListAsync()
        {
            var result = new List<Clothe>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Clothes.ToListAsync();
                result = db.Clothes.Include(p => p.TypeClothe).ToList();
                result = db.Clothes.Include(p => p.Material).ToList();
                result = db.Clothes.Include(p => p.Brand).ToList();
                result = db.Clothes.Include(p => p.Gender).ToList();
            }
            return result;
        }

        public List<Clothe> GetItemList()
        {
            var result = new List<Clothe>();
            using (db = new ApplicationDbContext())
            {
                result = db.Clothes.ToList();
                result = db.Clothes.Include(p => p.TypeClothe).ToList();
                result = db.Clothes.Include(p => p.Material).ToList();
                result = db.Clothes.Include(p => p.Brand).ToList();
                result = db.Clothes.Include(p => p.Gender).ToList();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Clothe> GetItemAsync(int id)
        {
            Clothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Clothe> CreateAsync(Clothe item)
        {
            Clothe result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Clothes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Clothe> UpdateAsync(Clothe item)
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
            Clothe clothe = await db.Clothes.FindAsync(id);
            if (clothe != null)
                db.Clothes.Remove(clothe);
        }

        // сохранение изменений
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }     

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}