using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDataStorage<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(string id);
}
