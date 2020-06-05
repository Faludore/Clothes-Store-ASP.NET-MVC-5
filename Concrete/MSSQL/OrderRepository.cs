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
    public class OrderRepository : IRepository<Order>
    {
        private ApplicationDbContext db;

        public OrderRepository()
        {
            this.db = new ApplicationDbContext();
        }

        // получение всех объектов
        public async Task<List<Order>> GetItemListAsync()
        {
            var result = new List<Order>();
            using (db = new ApplicationDbContext())
            {
                result = await db.Orders.ToListAsync();
            }
            return result;
        }

        // получение одного объекта по id
        public async Task<Order> GetItemAsync(int id)
        {
            Order result = null;
            using (db = new ApplicationDbContext())
            {
                result = await db.Orders.FirstOrDefaultAsync(c => c.Id == id);
            }
            return result;
        }

        // создание объекта
        public async Task<Order> CreateAsync(Order item)
        {
            Order result = null;
            using (db = new ApplicationDbContext())
            {
                result = db.Orders.Add(item);
                await db.SaveChangesAsync();
            }
            return result;
        }

        // обновление объекта
        public async Task<Order> UpdateAsync(Order item)
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
            Order item = await db.Orders.FindAsync(id);
            if (item != null)
                db.Orders.Remove(item);
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

        public List<Order> GetItemList()
        {
            var result = new List<Order>();
            using (db = new ApplicationDbContext())
            {
                result =  db.Orders.ToList(); 
                result = db.Orders.Include(p => p.TypeBuy).ToList();
            }
            return result;
        }
    }
}
