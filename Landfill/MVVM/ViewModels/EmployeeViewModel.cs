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
        private ObservableCollection<BuildProjectModel> _items;
        private int _selectedItemIndex = -1;
        private string _searchText;
        private DateTime _lastSearchTextPressedAt;

        public EmployeeInfoModel CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ObservableCollection<BuildProjectModel> Items { get => _items; set { _items = value; OnPropertyChanged(); } }
        public int SelectedItemIndex { get => _selectedItemIndex; set { _selectedItemIndex = value; OnPropertyChanged(); RunCommand(ItemSelectedCommand); } }
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); OnSearchTextChanged(); } }

        public ICommand LogoutCommand { get; }
        public ICommand ItemSelectedCommand { get; }

        #endregion

        public EmployeeViewModel(INavigationService navigation, IDbContext dbContext, IMapper mapper)
        {
            _navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            ItemSelectedCommand = new ViewModelCommand(ExecuteItemSelectedCommand);

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
            Items = _mapper.Map<ObservableCollection<BuildProjectModel>>(projects);
        }

        private void ExecuteLogoutCommand(object obj)
        {
            StorageHelper.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }

        private void ExecuteItemSelectedCommand(object obj)
        {

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

            Items = _mapper.Map<ObservableCollection<BuildProjectModel>>(searchResult);
        }
    }
}
