using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreIdent.Interface
{
    public interface IRepository<T> : IDisposable
       where T : class
    {
         
        Task<List<T>> GetItemListAsync(); // получение всех объектов
        Task<T> GetItemAsync(int id); // получение одного объекта по id
        Task<T> CreateAsync(T item); // создание объекта
        Task<T> UpdateAsync(T item); // обновление объекта
        Task DeleteAsync(int id);// удаление объекта по id
        Task SaveAsync(); // сохранение изменений
        List<T> GetItemList();
    }
}
