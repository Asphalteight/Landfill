using Landfill.Abstractions;
using Landfill.Common.Enums;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Landfill.Services
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo<T>(object currentWindow, WindowTypeEnum changeWindowTo) where T : ViewModelBase;
    }

    public class NavigationService : ViewModelBase, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private readonly Func<Type, Window> _viewFactory;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView 
        {
            get => _currentView;
            private set { _currentView = value; OnPropertyChanged(); }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory, Func<Type, Window> viewFactory)
        {
            _viewModelFactory = viewModelFactory;
            _viewFactory = viewFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

        public void NavigateTo<TViewModel>(object currentWindow, WindowTypeEnum changeWindowTo) where TViewModel : ViewModelBase
        {
            var windowType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name.Equals($"{changeWindowTo}WindowView"));

            _viewFactory.Invoke(windowType).Show();
            (currentWindow as Window).Close();

            NavigateTo<TViewModel>();
        }
    }
}
