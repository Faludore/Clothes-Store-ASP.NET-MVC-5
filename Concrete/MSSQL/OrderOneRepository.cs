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
    public class OrderOneRepository : IRepository<OrderOne>
    {
        private ApplicationDbContext db;

        public OrderOneRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<OrderOne>> GetItemListAsync()
        {
            var result = new List<OrderOne>();
            using (db = new ApplicationDbContext())
            {
                result = await db.OrderOnes.ToListAsync();
            }
            return result;
        }
        public List<OrderOne> GetItemList()
        {
            var result = new List<OrderOne>();
            using (db = new ApplicationDbContext())
            {
                result = db.OrderOnes.ToList();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<OrderOne> GetItemAsync(int id)
        {
            OrderOne result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.OrderOnes.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<OrderOne> CreateAsync(OrderOne item)
        {
            OrderOne result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.OrderOnes.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<OrderOne> UpdateAsync(OrderOne item)
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
            OrderOne item = await db.OrderOnes.FindAsync(id);
            if (item != null)
                db.OrderOnes.Remove(item);
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

