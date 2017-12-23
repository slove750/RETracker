using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RETracker.Models;
using RETracker.ViewModels;

namespace RETracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyDetailPage : ContentPage
    {
        public Property Item { get; set; }

        public PropertyDetailPage()
        {
            InitializeComponent();
            
            Item = new Property
            {
                Id = -1
            };
            
            BindingContext = Item;
        }

        public PropertyDetailPage(Property model)
        {
            Item = model;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}