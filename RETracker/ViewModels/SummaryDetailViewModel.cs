using System;

using RETracker.Models;

namespace RETracker.ViewModels
{
    public class SummaryDetailViewModel : BaseViewModel
    {
        public Entity Item { get; set; }
        public SummaryDetailViewModel(Entity item = null)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
