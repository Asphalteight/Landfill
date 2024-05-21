using Landfill.Abstractions;

namespace Landfill.MVVM.Models
{
    public class CredentialsModel : ObservableObject
    {
        private string _login;
        private string _passwordDublicate;
        private string _password;
        private bool _showPassword;
        private bool _passwordMaskedVisibility = true;
        private bool _passwordUnmaskedVisibility = true;

        public string Login { get => _login; set { _login = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value;
                OnPropertyChanged(); } }
        public string PasswordDublicate { get => _passwordDublicate; set { _passwordDublicate = value; OnPropertyChanged(); } }
        public bool ShowPassword { get => _showPassword; set { _showPassword = value; OnPropertyChanged(); PasswordMaskedVisibility = value == false; PasswordUnmaskedVisibility = value == true; } }
        public bool PasswordMaskedVisibility { get => _passwordMaskedVisibility; set { _passwordMaskedVisibility = value; OnPropertyChanged(); } }
        public bool PasswordUnmaskedVisibility { get => _passwordUnmaskedVisibility; set { _passwordUnmaskedVisibility = value; OnPropertyChanged();} }
    }
}
