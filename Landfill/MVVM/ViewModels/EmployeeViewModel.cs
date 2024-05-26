using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System;
using System.Collections.ObjectModel;
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
        private EmployeeInfoModel _currentUser;
        private INavigationService _navigation;
        private string _searchText;
        private DateTime _lastSearchTextPressedAt;

        public EmployeeInfoModel CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); OnSearchTextChanged(); } }
        
        public IItemsService ItemsService { get; set; }
        public ICommand LogoutCommand { get; }
        public ICommand NewBuildProjectCommand { get; }

        #endregion

        public EmployeeViewModel(INavigationService navigation, IDbContext dbContext, IMapper mapper, IItemsService itemsService)
        {
            _navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            ItemsService = itemsService;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            NewBuildProjectCommand = new ViewModelCommand(ExecuteNewBuildProjectCommand);

            UpdateCurrentUser();
            UpdateItems();
        }

        private void UpdateCurrentUser()
        {
            var currentUserLogin = Thread.CurrentPrincipal?.Identity?.Name;

            var employee = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == currentUserLogin)?.Employee;
            if (employee != null)
            {
                CurrentUser = _mapper.Map<EmployeeInfoModel>(employee);
            }
        }

        private void UpdateItems()
        {
            var projects = _dbContext.QuerySet<BuildProject>().ToList();
            ItemsService.Items = _mapper.Map<ObservableCollection<BuildProjectModel>>(projects);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            StorageHelper.ResetStoredUser();

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

            ItemsService.Items = _mapper.Map<ObservableCollection<BuildProjectModel>>(searchResult);
        }

        private void ExecuteNewBuildProjectCommand(object obj)
        {
            
        }
    }
}
