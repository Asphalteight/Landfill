using AutoMapper;
using Landfill.Abstractions;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class BuildProjectViewModel : ViewModelBase
    {
        #region Свойства и поля

        private IItemsService _itemsService;
        private readonly IMapper _mapper;
        private INavigationService _navigation;
        private BuildProjectModel _currentItem;

        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public IItemsService ItemsService { get => _itemsService; set { _itemsService = value; OnPropertyChanged(); } }
        public IUserContextService UserContext { get; set; }
        public BuildProjectModel CurrentItem { get => _currentItem; set { _currentItem = value; OnPropertyChanged(); } }

        public ICommand NavigateToEditProjectCommand { get; }

        #endregion

        public BuildProjectViewModel(IItemsService itemsService, INavigationService navigation, IMapper mapper, IUserContextService userContext)
        {
            _itemsService = itemsService;
            _navigation = navigation;
            _mapper = mapper;
            UserContext = userContext;

            NavigateToEditProjectCommand = new ViewModelCommand(x => Navigation.NavigateItemPanelTo<BuildProjectEditableViewModel>());

            CurrentItem = ItemsService.Items[ItemsService.SelectedItemIndex];
        }
    }
}
