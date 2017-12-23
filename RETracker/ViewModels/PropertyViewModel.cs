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
    public class PropertyViewModel : BaseViewModel
    {
        public ObservableCollection<Property> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PropertyViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Property>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<PropertyDetailPage, Property>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Property;
                Items.Add(_item);
                SaveProperty(_item);
            });
        }

        public PropertyViewModel(PropertyViewModel model)
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
                var items = await GetProperties();
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

        private async Task<IList<Property>> GetProperties()
        {
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/property", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IList<Property>>(response.Content);
            }

            return new List<Property>();
        }

        private async void SaveProperty(Property entity)
        {
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/property", Method.POST);
            request.AddJsonBody(entity);
            var response = await client.ExecutePostTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(response));
            }
        }
    }
}
