using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RETracker.Models;

[assembly: Xamarin.Forms.Dependency(typeof(RETracker.Services.MockDataStore))]
namespace RETracker.Services
{
    public class MockDataStore : IDataStore<Entity>
    {
        List<Entity> items;

        public MockDataStore()
        {
            items = new List<Entity>();
            /*
            var mockItems = new List<Entity>
            {
                new Entity { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Entity { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Entity { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Entity { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Entity { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Entity { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            };
            
            foreach (var item in mockItems)
            {
                items.Add(item);
            }
            */
        }

        public async Task<bool> AddItemAsync(Entity item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Entity item)
        {
            var _item = items.Where((Entity arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Entity item)
        {
            var _item = items.Where((Entity arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Entity> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => /*s.Id == id*/ s.Id == 1));
        }

        public async Task<IEnumerable<Entity>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}