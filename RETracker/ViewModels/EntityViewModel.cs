using Newtonsoft.Json;
using RestSharp;
using RETracker.Models;
using RETracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RETracker.ViewModels
{
    public class EntityViewModel : BaseViewModel
    {
        public ObservableCollection<Entity> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public EntityViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Entity>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<EntityDetailPage, Entity>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Entity;
                Items.Add(_item);
                SaveEntity(_item);
            });
        }

        public EntityViewModel(EntityViewModel model)
        {
            if (model != null)
            {
                Items = model.Items;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await GetEntities();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IList<Entity>> GetEntities()
        {
            var client = new RestClient("http://localhost:9000");
            var request = new RestRequest("/api/entity", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IList<Entity>>(response.Content);
            }

            return new List<Entity>();
        }

        private async void SaveEntity(Entity entity)
        {
            var client = new RestClient("http://localhost:9000");
            var request = new RestRequest("/api/entity", Method.POST);
            request.AddJsonBody(entity);
            var response = await client.ExecutePostTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(response));
            }
        }
    }
}
