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
	public partial class EntitiesPage : ContentPage
	{
        EntityViewModel viewModel;

        public EntitiesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EntityViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Entity;
            if (item == null)
                return;

            await Navigation.PushAsync(new EntityDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EntityDetailPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}