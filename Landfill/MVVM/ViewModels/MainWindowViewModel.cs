﻿using Landfill.Abstractions;
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

            RunCommand(x => Navigation.NavigateTo<EmployeeViewModel>());
        }
    }
}
