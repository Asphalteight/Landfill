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

        private INavigationService _navigation;
        private int _selectedItemIndex;
        private int _previousItemIndex;
        private ObservableCollectionWithItemNotify<BuildProjectModel> _items;

        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public int SelectedItemIndex { get => _selectedItemIndex; set { _selectedItemIndex = value; SelectedItemChanged(); OnPropertyChanged(); } }
        public ObservableCollectionWithItemNotify<BuildProjectModel> Items { get => _items; set { _items = value; ItemsChanged(); OnPropertyChanged(); } }

        #endregion

        public ItemsService(INavigationService navigation)
        {
            Navigation = navigation;
        }

        public void ItemsChanged()
        {
            if (Items.Count > 0)
            {
                SelectedItemIndex = 0;
                Items[SelectedItemIndex].IsSelected = true;
                RunCommand(x => Navigation.NavigateItemPanelTo<BuildProjectViewModel>());
            }
        }

        private void SelectedItemChanged()
        {
            if (Items.Count == 0)
            {
                RunCommand(x => Navigation.NavigateItemPanelTo<EmptyViewModel>());
                return;
            }
            else if (SelectedItemIndex < 0)
            {
                SelectedItemIndex = 0;
            }
            if (Items.Count > _previousItemIndex && _previousItemIndex >= 0)
            {
                Items[_previousItemIndex].IsSelected = false;
            }

            if (SelectedItemIndex >= 0)
            {
                Items[SelectedItemIndex].IsSelected = true;
                RunCommand(x => Navigation.NavigateItemPanelTo<BuildProjectViewModel>());
            }

            _previousItemIndex = SelectedItemIndex;
        }
    }
}
