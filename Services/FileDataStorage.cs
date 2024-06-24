using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class FileDataStorage<T> : IDataStorage<T> where T : class
{
    private readonly string _filePath;
    private List<T> _items;

    public FileDataStorage(string filePath)
    {
        _filePath = filePath;
        _items = new List<T>();

        if (File.Exists(_filePath))
        {
            var jsonData = File.ReadAllText(_filePath);
            _items = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_items);
    }

    public async Task<T> GetAsync(string id)
    {
        return await Task.FromResult(_items.FirstOrDefault(i => CompareId(i, id)));
    }

    public async Task AddAsync(T item)
    {
        _items.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(T item)
    {
        var existingItem = _items.FirstOrDefault(i => (i as dynamic).Id == (item as dynamic).Id);
        if (existingItem != null)
        {
            _items.Remove(existingItem);
            _items.Add(item);
            await SaveAsync();
        }
    }

    public async Task DeleteAsync(string id)
    {
        var item = _items.FirstOrDefault(i => (i as dynamic).Id == id);
        if (item != null)
        {
            _items.Remove(item);
            await SaveAsync();
        }
    }

    private async Task SaveAsync()
    {
        var jsonData = JsonSerializer.Serialize(_items);
        await File.WriteAllTextAsync(_filePath, jsonData);
    }
    private bool CompareId(T item, string id)
    {
        var itemId = (item as dynamic).Id;
        if (itemId is int intId)
        {
            return intId == int.Parse(id);
        }
        if (itemId is string strId)
        {
            return strId == id;
        }
        throw new InvalidOperationException("Unsupported Id type");
    }
}
