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
    public class TransactionViewModel : BaseViewModel
    {
        public ObservableCollection<TransEntry> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public TransactionViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<TransEntry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            MessagingCenter.Subscribe<TransactionDetailPage, TransEntry>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as TransEntry;
                Items.Add(_item);
            });
            
        }

        public TransactionViewModel(TransactionViewModel model)
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
                var items = await GetTransactions();
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

        private async Task<IList<TransEntry>> GetTransactions()
        {
            var client = new RestClient("http://localhost:9000");
            var request = new RestRequest("/api/transentry", Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IList<TransEntry>>(response.Content);
            }

            return new List<TransEntry>();
        }
    }
}
