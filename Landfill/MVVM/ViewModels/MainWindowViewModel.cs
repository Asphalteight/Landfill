using Landfill.Abstractions;
using Landfill.Common.Helpers;
using Landfill.Services;

namespace Landfill.MVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private INavigationService _navigation;

        public INavigationService Navigation { get => _navigation; set { _navigation = value; OnPropertyChanged(); } }

        public MainWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            NavigateToView();
        }

        private void NavigateToView()
        {
            var currentUserName = StorageHelper.GetStoredUser();
            if (currentUserName != null)
            {
                RunCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
            }
            else
            {
                RunCommand(x => Navigation.NavigateTo<SignInViewModel>());
            }
        }
    }
}
