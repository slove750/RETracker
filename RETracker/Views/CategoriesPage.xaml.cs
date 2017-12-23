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
	public partial class CategoriesPage : ContentPage
	{
        CategoriesPageViewModel catModel = new CategoriesPageViewModel();
        ItemsViewModel subCatModel = new ItemsViewModel();

        public CategoriesPage()
        {
            InitializeComponent();

            lvCat.BindingContext = catModel;
            lvSubCat.BindingContext = catModel;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Category;
            if (item == null)
                return;

            //            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            catModel.LoadSubItemsCommand.Execute(item.Id);

            // Manually deselect item.
//            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (catModel.Items.Count == 0)
                catModel.LoadItemsCommand.Execute(null);
        }
    }
}