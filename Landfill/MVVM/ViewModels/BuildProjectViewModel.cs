using AutoMapper;
using Landfill.Abstractions;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class BuildProjectViewModel : ViewModelBase
    {
        #region Свойства и поля

        private IItemsService _itemsService;
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private INavigationService _navigation;
        private BuildProjectModel _currentItem;

        public ICommand SaveItemCommand { get; }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public IItemsService ItemsService { get => _itemsService; set { _itemsService = value; OnPropertyChanged(); } }
        public BuildProjectModel CurrentItem { get => _currentItem; set { _currentItem = value; OnPropertyChanged(); } }

        #endregion

        public BuildProjectViewModel(IItemsService itemsService, IDbContext dbContext, INavigationService navigation, IMapper mapper)
        {
            _itemsService = itemsService;
            _dbContext = dbContext;
            _navigation = navigation;
            _mapper = mapper;

            CurrentItem = ItemsService.Items[ItemsService.SelectedItemIndex];
            SaveItemCommand = new ViewModelCommand(ExecuteSaveItemCommand);
        }

        private void ExecuteSaveItemCommand(object obj)
        {
            var item = _dbContext.QuerySet<BuildProject>().FirstOrDefault(x => x.Id == CurrentItem.Id);
            _mapper.Map(CurrentItem, item);

            _dbContext.SaveChanges();

            var items = _dbContext.QuerySet<BuildProject>();
            ItemsService.Items = _mapper.Map<ObservableCollection<BuildProjectModel>>(items);
        }
    }
}
