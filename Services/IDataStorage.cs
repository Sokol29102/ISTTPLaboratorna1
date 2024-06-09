using System.Text.Json;

namespace PharmacyApp.Services
{
    public interface IDataStorage<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(string id);
    }
}

