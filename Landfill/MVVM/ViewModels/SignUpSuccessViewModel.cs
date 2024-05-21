using Landfill.Abstractions;
using Landfill.Services;
using System.Threading;
using System.Windows.Input;

namespace Landfill.MVVM.ViewModels
{
    public class SignUpSuccessViewModel : ViewModelBase
    {
        #region Проперти

        private string _backToSignRemaningSeconds = "3";
        private INavigationService _navigation;
        private static Timer _timer;
        private int _lifeTime = 4000;
        private readonly int _period = 1000;

        public string BackToSignRemaningSeconds { get => _backToSignRemaningSeconds; set { _backToSignRemaningSeconds = value; OnPropertyChanged(); } }
        public INavigationService Navigation { get => _navigation; private set { _navigation = value; OnPropertyChanged(); } }
        public ICommand BackToSignInCommand { get; }

        #endregion

        public SignUpSuccessViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            BackToSignInCommand = new ViewModelCommand(x => { _timer.Dispose(); Navigation.NavigateTo<SignInViewModel>(); });
            StartTimer();
        }

        private void StartTimer()
        {
            _timer = new Timer(x => ProcessTimer(), null, _period, _period);
        }

        private void ProcessTimer()
        {
            _lifeTime -= _period;
            if (_lifeTime <= 0)
            {
                RunCommand(BackToSignInCommand);
                return;
            }

            BackToSignRemaningSeconds = (int.Parse(BackToSignRemaningSeconds) - 1).ToString();
        }
    }
}
