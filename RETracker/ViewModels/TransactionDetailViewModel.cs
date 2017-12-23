using Newtonsoft.Json;
using RestSharp;
using RETracker.Models;
using RETracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RETracker.ViewModels
{
    public class TransactionDetailViewModel : BaseViewModel
    {
        public TransEntry Item { get; set; }

        public ObservableCollection<Property> Properties { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<SubCategory> SubCategories { get; set; }
        public Command LoadItemsCommand { get; set; }

        public TransactionDetailViewModel()
        {
            Title = "Transaction Detail";
            Properties = new ObservableCollection<Property>();
            Categories = new ObservableCollection<Category>();
            SubCategories = new ObservableCollection<SubCategory>();
            Item = new TransEntry { Id = -1, EntryDate = DateTime.Now };

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<TransactionDetailPage, TransEntry>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as TransEntry;
                SaveTransaction(_item);
            });
        }

        public TransactionDetailViewModel(TransEntry item)
        {
            if (item != null)
            {
                Item = item;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Properties.Clear();
                Categories.Clear();
                SubCategories.Clear();

                var props = await GetProperties();
                foreach (var prop in props)
                {
                    Properties.Add(prop);
                }
                var cats = await GetCategories();
                foreach (var cat in cats)
                {
                    Categories.Add(cat);
                }
                var subs = await GetSubCategories();
                foreach (var sub in subs)
                {
                    SubCategories.Add(sub);
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

        private async Task<IList<Category>> GetCategories()
        {
            Debug.WriteLine("GetCats");
            var results = new List<Category>();

            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/category", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                results = JsonConvert.DeserializeObject<List<Category>>(response.Content);
            }

            Debug.WriteLine("end GetCats");
            return results;
        }

        private async Task<IList<SubCategory>> GetSubCategories()
        {
            Debug.WriteLine("GetSubCats");
            var results = new List<SubCategory>();

            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/subcategory", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                results = JsonConvert.DeserializeObject<List<SubCategory>>(response.Content);
                results.Insert(0, new SubCategory
                {
                    Id = -1,
                    Name = "--- None ---"
                });
            }

            Debug.WriteLine("end GetSubCats");
            return results;
        }

        private async Task<IList<Property>> GetProperties()
        {
            Debug.WriteLine("GetProps");
            var results = new List<Property>();

            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/property", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                results = JsonConvert.DeserializeObject<List<Property>>(response.Content);
            }

            Debug.WriteLine("end GetProps");
            return results;
        }

        private async Task<TransEntry> GetTransaction(TransEntry item)
        {
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest($"/api/transentry/{item.Id}", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(response));
            }

            return JsonConvert.DeserializeObject<TransEntry>(response.Content);
        }

        private async void SaveTransaction(TransEntry entity)
        {
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/transentry", Method.POST);
            request.AddJsonBody(entity);
            var response = await client.ExecutePostTaskAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(response));
            }
        }
    }
}
