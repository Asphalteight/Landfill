using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.Common.Helpers;
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
    /// <summary>
    /// Главное окно
    /// </summary>
    public class EmployeeViewModel : ViewModelBase
    {
        #region Поля и свойства

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private INavigationService _navigation;
        private DateTime _lastSearchTextPressedAt;

        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public FilterModel Filter { get; set; } = new();

        public IItemsService ItemsService { get; set; }
        public IUserContextService UserContextService { get; set; }
        public ICommand LogoutCommand { get; }
        public ICommand NewBuildProjectCommand { get; }
        public ICommand NavigateToEmployeeProfileCommand { get; }
        public ICommand NavigateToEmployeesManagingCommand { get; }

        #endregion

        public EmployeeViewModel(INavigationService navigation, IDbContext dbContext, IMapper mapper, IItemsService itemsService, IUserContextService userContextService)
        {
            Navigation = navigation;
            _dbContext = dbContext;
            _mapper = mapper;
            ItemsService = itemsService;
            UserContextService = userContextService;

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
            NewBuildProjectCommand = new ViewModelCommand(ExecuteNewBuildProjectCommand);
            NavigateToEmployeeProfileCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeeProfileViewModel>());
            NavigateToEmployeesManagingCommand = new ViewModelCommand(x => Navigation.NavigateTo<EmployeesManagingViewModel>());

            Filter.OnSearchTextChanged = OnSearchTextChanged;
            Filter.OnMyProjectsOnlyCheck = ApplySearch;
            Filter.OnProjectStateChange = ApplySearch;
            Filter.OnMinPriceChange = ApplySearch;
            Filter.OnMaxPriceChange = ApplySearch;
            Filter.OnSortChange = ApplySearch;

            if (ItemsService.RunFiltersUpdate == null)
            {
                ItemsService.RunFiltersUpdate = ApplySearch;
            }
            ItemsService.RunFiltersUpdate();
        }

        private void ExecuteLogoutCommand(object obj)
        {
            UserContextService.ResetStoredUser();

            RunCommand(x => Navigation.NavigateTo<SignInViewModel>(obj, WindowTypeEnum.Login));
        }

        private void ExecuteNewBuildProjectCommand(object obj)
        {
            ItemsService.SelectedItemIndex = -1;
            RunCommand(x => Navigation.NavigateItemPanelTo<BuildProjectAddNewViewModel>());
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
            var query = _dbContext.QuerySet<BuildProject>();

            if (!Filter.SearchText.IsNullOrWhiteSpace())
            {
                var searchText = Filter.SearchText;
                query = query.Where(x =>
                    x.Name.Contains(searchText) ||
                    x.Description.Contains(searchText) ||
                    x.Address.Contains(searchText) ||
                    x.Customer.Contains(searchText));
            }
            if (Filter.MyProjectsOnly)
            {
                query = query.Where(x => x.EmployeeId == UserContextService.CurrentUser.Id);
            }
            if (Filter.ProjectState.HasValue)
            {
                query = query.Where(x => x.State == Filter.ProjectState.Value);
            }
            if (Filter.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= Filter.MinPrice.Value);
            }
            if (Filter.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= Filter.MaxPrice.Value);
            }
            switch (Filter.Sort)
            {
                case SortEnum.CreatedDesc:
                    query = query.OrderByDescending(x => x.CreatedOn);
                    break;
                case SortEnum.CreatedAsc:
                    query = query.OrderBy(x => x.CreatedOn);
                    break;
                case SortEnum.PriceDesc:
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case SortEnum.PriceAsc:
                    query = query.OrderBy(x => x.Price);
                    break;
            }

            ItemsService.Items = _mapper.Map<ObservableCollectionWithItemNotify<BuildProjectModel>>(query.ToArray());
        }
    }
}
