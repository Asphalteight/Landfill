using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using Landfill.Services;
using System;
using System.Linq;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class BuildProjectAddNewViewModel : ViewModelBase
    {
        #region Свойства и поля

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private INavigationService _navigation;
        private BuildProjectModel _currentItem;
        private int _projectMemberSelectedIndex = -1;
        private int _previousProjectMemberSelectedIndex;

        public int ProjectMemberSelectedIndex { get => _projectMemberSelectedIndex; set { _projectMemberSelectedIndex = value; OnSelectionChange(); OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public IItemsService ItemsService { get; set; }
        public IUserContextService UserContextService { get; set; }
        public BuildProjectModel CurrentItem { get => _currentItem; set { _currentItem = value; OnPropertyChanged(); } }

        public ICommand SaveItemCommand { get; }
        public ICommand AddProjectMemberCommand { get; }
        public ICommand RemoveProjectMemberCommand { get; }
        public ICommand ClearStartDateCommand { get; }
        public ICommand ClearPlanningCompletionDateCommand { get; }
        public ICommand ClearCompletionDateCommand { get; }

        #endregion

        public BuildProjectAddNewViewModel(IItemsService itemsService, IDbContext dbContext, INavigationService navigation, IMapper mapper, IUserContextService userContextService)
        {
            ItemsService = itemsService;
            UserContextService = userContextService;
            _dbContext = dbContext;
            _navigation = navigation;
            _mapper = mapper;

            LoadBuildProject();

            SaveItemCommand = new ViewModelCommand(ExecuteSaveItemCommand, CanExecuteSaveItemCommand);
            AddProjectMemberCommand = new ViewModelCommand(ExecuteAddProjectMemberCommand);
            RemoveProjectMemberCommand = new ViewModelCommand(ExecuteRemoveProjectMemberCommand, CanExecuteRemoveProjectMemberCommand);
            ClearStartDateCommand = new ViewModelCommand(ExecuteClearStartDateCommand);
            ClearPlanningCompletionDateCommand = new ViewModelCommand(ExecuteClearPlanningCompletionDateCommand);
            ClearCompletionDateCommand = new ViewModelCommand(ExecuteClearCompletionDateCommand);
        }

        private void LoadBuildProject()
        {
            CurrentItem = new BuildProjectModel();
        }

        private void ExecuteClearCompletionDateCommand(object obj)
        {
            CurrentItem.CompletionDate = DateTime.Now.Date;
            CurrentItem.CompletionDate = null;
        }

        private void ExecuteClearPlanningCompletionDateCommand(object obj) => CurrentItem.PlanningCompletionDate = DateTime.Now.Date;
        private void ExecuteClearStartDateCommand(object obj)
        {
            CurrentItem.StartDate = DateTime.Now.Date;
            CurrentItem.StartDate = null;
        }

        private bool CanExecuteRemoveProjectMemberCommand(object obj) => CurrentItem.Members.Any(x => x.IsSelected);

        private void ExecuteRemoveProjectMemberCommand(object obj)
        {
            var selectedIndex = ProjectMemberSelectedIndex;
            if (selectedIndex != 0)
            {
                ProjectMemberSelectedIndex--;
            }
            CurrentItem.Members.RemoveAt(selectedIndex);
        }

        private void ExecuteAddProjectMemberCommand(object obj)
        {
            CurrentItem.Members.Add(new ProjectMemberModel { IsNew = true });
            ProjectMemberSelectedIndex = CurrentItem.Members.IndexOf(CurrentItem.Members.Last());
        }

        private void OnSelectionChange()
        {
            if (CurrentItem.Members.Count > _previousProjectMemberSelectedIndex && _previousProjectMemberSelectedIndex >= 0)
            {
                CurrentItem.Members[_previousProjectMemberSelectedIndex].IsSelected = false;
            }
            if (ProjectMemberSelectedIndex >= 0)
            {
                CurrentItem.Members[ProjectMemberSelectedIndex].IsSelected = true;
            }

            _previousProjectMemberSelectedIndex = ProjectMemberSelectedIndex;
        }

        private bool CanExecuteSaveItemCommand(object obj)
        {
            var isMembersValid = CurrentItem.Members.Select(x => new { x.FirstName, x.LastName }).All(x => !x.FirstName.IsNullOrWhiteSpace() && !x.LastName.IsNullOrWhiteSpace());
            var isProjectValid = new string[] { CurrentItem.Name, CurrentItem.Address, CurrentItem.Customer, CurrentItem.Price.ToString() }.All(x => !x.IsNullOrWhiteSpace());

            return isMembersValid && isProjectValid;
        }

        private void ExecuteSaveItemCommand(object obj)
        {
            var item = new BuildProject();

            CurrentItem.EmployeeId = UserContextService.CurrentUser.Id;
            _mapper.Map(CurrentItem, item);

            _dbContext.Add(item);
            _dbContext.SaveChanges();

            CurrentItem = _mapper.Map<BuildProjectModel>(item);
            ItemsService.Items.Add(CurrentItem);

            ItemsService.SelectedItemIndex = ItemsService.Items.IndexOf(CurrentItem);
        }
    }
}
