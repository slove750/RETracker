using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RETracker.Models;
using RETracker.Views;
using RETracker.ViewModels;

namespace RETracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransactionsPage : ContentPage
	{
        TransactionViewModel viewModel;

        public TransactionsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TransactionViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TransEntry;
            if (item == null)
                return;

            await Navigation.PushAsync(new TransactionDetailPage(new TransactionDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TransactionDetailPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}