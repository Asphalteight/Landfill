using Landfill.Abstractions;
using Landfill.MVVM.Models;
using Landfill.MVVM.ViewModels;

namespace Landfill.Services
{
    public interface IItemsService
    {
        public ObservableCollectionWithItemNotify<BuildProjectModel> Items { get; set; }
        public int SelectedItemIndex { get; set; }
    }

    public class ItemsService : ViewModelBase, IItemsService
    {
        #region Поля и свойства

        private readonly INavigationService _navigation;
        private int _selectedItemIndex;
        private int _previousItemIndex;
        private ObservableCollectionWithItemNotify<BuildProjectModel> _items;

        public int SelectedItemIndex { get => _selectedItemIndex; set { _selectedItemIndex = value; SelectedItemChanged(); OnPropertyChanged(); } }
        public ObservableCollectionWithItemNotify<BuildProjectModel> Items { get => _items; set { _items = value; ItemsChanged(); OnPropertyChanged(); } }

        #endregion

        public ItemsService(INavigationService navigation)
        {
            _navigation = navigation;
        }

        public void ItemsChanged()
        {
            if (Items.Count > 0)
            {
                SelectedItemIndex = 0;
                Items[SelectedItemIndex].IsSelected = true;
                RunCommand(x => _navigation.NavigateItemPanelTo<BuildProjectViewModel>());
            }
        }

        private void SelectedItemChanged()
        {
            if (Items.Count == 0 || SelectedItemIndex < 0)
            {
                RunCommand(x => _navigation.NavigateItemPanelTo<EmptyViewModel>());
                return;
            }
            if (Items.Count > _previousItemIndex && _previousItemIndex >= 0)
            {
                Items[_previousItemIndex].IsSelected = false;
            }

            if (SelectedItemIndex >= 0)
            {
                Items[SelectedItemIndex].IsSelected = true;
                RunCommand(x => _navigation.NavigateItemPanelTo<BuildProjectViewModel>());
            }

            _previousItemIndex = SelectedItemIndex;
        }
    }
}
