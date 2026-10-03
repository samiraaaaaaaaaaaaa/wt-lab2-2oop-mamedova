using System.Collections.Generic;
using System.Threading.Tasks;
using wt_lab2_2oop_mamedova.Models;

namespace wt_lab2_2oop_mamedova.Interfaces
{
    public interface IItemService<T> where T : BaseEntity
    {
        void Add(T item);
        List<T> GetAll();
        T GetById(int id);
        void Remove(int id);
        Task ProcessRoomsAsync();
        Task PerformBulkOperationsAsync();
        Task<List<T>> GetAllAsync();
        Task ProcessWithExceptionHandlingAsync(bool triggerError); // <--- Добавлено
    }
}