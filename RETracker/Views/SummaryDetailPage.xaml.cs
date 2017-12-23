using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RETracker.Models;
using RETracker.ViewModels;

namespace RETracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SummaryDetailPage : ContentPage
	{
        SummaryDetailViewModel viewModel;

        public SummaryDetailPage(SummaryDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public SummaryDetailPage()
        {
            InitializeComponent();

            var item = new Entity
            {
                Name = "Item 1",
            };

            viewModel = new SummaryDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}