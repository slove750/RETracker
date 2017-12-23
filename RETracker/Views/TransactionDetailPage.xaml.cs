using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RETracker.ViewModels;
using RETracker.Models;

namespace RETracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailPage : ContentPage
    {
        private TransactionDetailViewModel model = new TransactionDetailViewModel();

        public TransactionDetailPage()
        {
            InitializeComponent();

            BindingContext = model;
        }

        public TransactionDetailPage(TransactionDetailViewModel m)
        {
            model = m;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            // get the picker selected items to add as FKeys
            model.Item.CategoryId = ((Category)pCats.SelectedItem).Id;
            model.Item.SubCategoryId = ((SubCategory)pSubCats.SelectedItem)?.Id;
            model.Item.PropertyId = ((Property)pProps.SelectedItem).Id;

            MessagingCenter.Send(this, "AddItem", model.Item);
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (model.Properties.Count == 0)
            {
                model.LoadItemsCommand.Execute(null);
            }
        }
    }
}