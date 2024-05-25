using Landfill.MVVM.Models;
using System.Collections.ObjectModel;

namespace Landfill.Services
{
    public interface IItemsService
    {
        public ObservableCollection<BuildingProjectModel> Items { get; set; }
    }

    public class ItemsService : IItemsService
    {
        public ObservableCollection<BuildingProjectModel> Items { get ; set; }
    }
}
