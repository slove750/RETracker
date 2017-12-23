using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RETracker.Models;

namespace RETracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Entity Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            /*
            Item = new Entity
            {
                Text = "Item name",
                Description = "This is an item description."
            };
            */
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}