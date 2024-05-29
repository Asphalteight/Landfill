using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        #region Проперти

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private INavigationService _navigation;
        private string _searchText;
        private DateTime _lastSearchTextPressedAt;

        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); OnSearchTextChanged(); } }

        public IItemsService ItemsService { get; set; }
        public IUserContextService UserContextService { get; set; }
        public ICommand LogoutCommand { get; }
        public ICommand NewBuildProjectCommand { get; }
        public ICommand NavigateToEmployeeProfileCommand { get; }
        public ICommand NavigateToEmployeesManagingCommand { get; }

        #endregion

        public EmployeeViewModel(INavigationService navigation, IDbContext dbContext, IMapper mapper, IItemsService itemsService, IUserContextService userContextService)
        {
            _navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            ItemsService = itemsService;
            UserContextService = userContextService;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            NewBuildProjectCommand = new ViewModelCommand(ExecuteNewBuildProjectCommand);
            NavigateToEmployeeProfileCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeProfileViewModel>());
            NavigateToEmployeesManagingCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeesManagingViewModel>());

            UpdateItems();
        }

        private void UpdateItems()
        {
            var projects = _dbContext.QuerySet<BuildProject>().ToList();
            ItemsService.Items = _mapper.Map<ObservableCollectionWithItemNotify<BuildProjectModel>>(projects);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            UserContextService.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }

        private void OnSearchTextChanged()
        {
            var delay = 500;
            _lastSearchTextPressedAt = DateTime.Now;
            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (s, e) => Thread.Sleep(delay);
            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                if (DateTime.Now > _lastSearchTextPressedAt.AddMilliseconds(delay))
                {
                    ApplySearch();
                }
            };
            backgroundWorker.RunWorkerAsync();
        }

        private void ApplySearch()
        {
            var searchResult = _dbContext.QuerySet<BuildProject>().Where(x =>
                x.Name.Contains(SearchText) ||
                x.Description.Contains(SearchText) ||
                x.Address.Contains(SearchText) ||
                x.Customer.Contains(SearchText)).ToList();

            ItemsService.Items = _mapper.Map<ObservableCollectionWithItemNotify<BuildProjectModel>>(searchResult);
        }

        private void ExecuteNewBuildProjectCommand(object obj)
        {
            ItemsService.SelectedItemIndex = -1;
            RunCommand(x => Navigation.NavigateItemPanelTo<BuildProjectAddNewViewModel>());
        }
    }
}
