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
        ViewModelBase CurrentDialogView { get; }
        ViewModelBase ItemPanelView { get; }

        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo<T>(object currentWindowObj, WindowTypeEnum changeWindowTo, bool isDialog = false) where T : ViewModelBase;
        void NavigateItemPanelTo<T>() where T : ViewModelBase;
        void Close(object currentWindowObj);
    }

    public class NavigationService : ViewModelBase, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private readonly Func<Type, Window> _viewFactory;
        private ViewModelBase _currentView;
        private ViewModelBase _currentDialogView;
        private ViewModelBase _itemPanelView;

        public ViewModelBase CurrentView 
        {
            get => _currentView;
            private set { _currentView = value; OnPropertyChanged(); }
        }
        public ViewModelBase CurrentDialogView
        {
            get => _currentDialogView;
            private set { _currentDialogView = value; OnPropertyChanged(); }
        }
        public ViewModelBase ItemPanelView
        {
            get => _itemPanelView;
            private set { _itemPanelView = value; OnPropertyChanged(); }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory, Func<Type, Window> viewFactory)
        {
            _viewModelFactory = viewModelFactory;
            _viewFactory = viewFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            CurrentView = _viewModelFactory.Invoke(typeof(TViewModel));
        }
        
        public void NavigateTo<TViewModel>(object currentWindowObj, WindowTypeEnum changeWindowTo, bool isDialog = false) where TViewModel : ViewModelBase
        {
            var windowType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name.Equals($"{changeWindowTo}WindowView"));

            var currentWindow = (currentWindowObj as Window);
            var window = _viewFactory.Invoke(windowType);
            if (isDialog)
            {
                window.Owner = currentWindow;
                CurrentDialogView = _viewModelFactory.Invoke(typeof(TViewModel));
            }
            else
            {
                currentWindow.Close();
                NavigateTo<TViewModel>();
            }
            window.Show();
        }

        public void NavigateItemPanelTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ItemPanelView = _viewModelFactory.Invoke(typeof(TViewModel));
        }

        public void Close(object currentWindowObj) => (currentWindowObj as Window).Close();
    }
}
