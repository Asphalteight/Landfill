using Landfill.MVVM.Models;
using System.Collections.ObjectModel;

namespace Landfill.Services
{
    public interface IItemsService
    {
        public ObservableCollection<BuildProjectModel> Items { get; set; }
    }

    public class ItemsService : IItemsService
    {
        public ObservableCollection<BuildProjectModel> Items { get ; set; }
    }
}
