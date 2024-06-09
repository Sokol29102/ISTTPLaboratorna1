using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PharmacyApp.Services
{
    public class FileDataStorage<T> : IDataStorage<T> where T : class
    {
        private readonly string _filePath;

        public FileDataStorage(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            var jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData);
        }

        public async Task<T> GetAsync(string id)
        {
            var items = await GetAllAsync();
            return items.FirstOrDefault(x => x.GetType().GetProperty("Id").GetValue(x).ToString() == id);
        }

        public async Task AddAsync(T item)
        {
            var items = await GetAllAsync();
            items.Add(item);
            await SaveAllAsync(items);
        }

        public async Task UpdateAsync(T item)
        {
            var items = await GetAllAsync();
            var index = items.FindIndex(x => x.GetType().GetProperty("Id").GetValue(x).ToString() == item.GetType().GetProperty("Id").GetValue(item).ToString());
            if (index >= 0)
            {
                items[index] = item;
                await SaveAllAsync(items);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var items = await GetAllAsync();
            var item = items.FirstOrDefault(x => x.GetType().GetProperty("Id").GetValue(x).ToString() == id);
            if (item != null)
            {
                items.Remove(item);
                await SaveAllAsync(items);
            }
        }

        private async Task SaveAllAsync(List<T> items)
        {
            var jsonData = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }
}