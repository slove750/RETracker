using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RETracker.Models;
using RETracker.ViewModels;

namespace RETracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntityDetailPage : ContentPage
    {
        public Entity Item { get; set; }

        public EntityDetailPage()
        {
            InitializeComponent();
            
            Item = new Entity
            {
                Id = -1,
                Name = ""
            };
            
            BindingContext = Item;
        }

        public EntityDetailPage(Entity model)
        {
            Item = model;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}