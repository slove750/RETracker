using Newtonsoft.Json;
using RestSharp;
using RETracker.Models;
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
    public class CategoriesPageViewModel :BaseViewModel
    {
        public ObservableCollection<Category> Items { get; set; }
        public ObservableCollection<SubCategory> SubItems { get; set; } = new ObservableCollection<SubCategory>();
        public Command LoadItemsCommand { get; set; }
        public Command LoadSubItemsCommand { get; set; }

        public CategoriesPageViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadSubItemsCommand = new Command<int>(async (c) => await ExecuteLoadSubItemsCommand(c));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await GetCategories();
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

        async Task ExecuteLoadSubItemsCommand(int catId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SubItems.Clear();
                var items = await GetSubCategories(catId);
                foreach (var item in items)
                {
                    SubItems.Add(item);
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
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest("/api/category", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IList<Category>>(response.Content);
            }

            return new List<Category>();
        }

        private async Task<IList<SubCategory>> GetSubCategories(int catId)
        {
            var client = new RestClient($"http://{Constants.URL}");
            var request = new RestRequest($"/api/subcategory/category/{catId}", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IList<SubCategory>>(response.Content);
            }

            return new List<SubCategory>();
        }
    }
}
