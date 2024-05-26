using Landfill.Abstractions;
using Landfill.MVVM.Models;
using Landfill.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace Landfill.Services
{
    public interface IItemsService
    {
        public ObservableCollection<BuildProjectModel> Items { get; set; }
        public int SelectedItemIndex { get; set; }
    }

    public class ItemsService : ViewModelBase, IItemsService
    {
        #region Поля и свойства

        private readonly INavigationService _navigation;
        private int _selectedItemIndex;
        private int _previousItemIndex;
        private ObservableCollection<BuildProjectModel> _items;

        public int SelectedItemIndex { get { return _selectedItemIndex >= 0 ? _selectedItemIndex : 0; } set { _selectedItemIndex = value; SelectedItemChanged(); OnPropertyChanged(); } }
        public int PreviousItemIndex { get { return _previousItemIndex >= 0 ? _previousItemIndex : 0; } set => _previousItemIndex = value; }
        public ObservableCollection<BuildProjectModel> Items { get => _items; set { _items = value; ItemsChanged(); OnPropertyChanged(); } }

        #endregion

        public ItemsService(INavigationService navigation)
        {
            _navigation = navigation;
        }

        private void ItemsChanged()
        {
            foreach (var item in Items)
            {
                item.EditCommand = new ViewModelCommand(ExecuteEditCommand);
                item.RemoveCommand = new ViewModelCommand(ExecuteRemoveCommand);
            }

            SelectedItemIndex = 0;
            Items[SelectedItemIndex].IsSelected = true;

            RunCommand(x => _navigation.NavigateItemPanelTo<BuildProjectViewModel>());
        }

        private void SelectedItemChanged()
        {
            if (Items.Count < PreviousItemIndex)
            {
                PreviousItemIndex = 0;
            }
            Items[PreviousItemIndex].IsSelected = false;
            Items[SelectedItemIndex].IsSelected = true;

            PreviousItemIndex = SelectedItemIndex;

            RunCommand(x => _navigation.NavigateItemPanelTo<BuildProjectViewModel>());
        }

        private void ExecuteEditCommand(object obj)
        {
            
        }

        private void ExecuteRemoveCommand(object obj)
        {
            
        }

    }
}
