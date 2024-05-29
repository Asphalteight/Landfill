using Landfill.Abstractions;

namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель кредов с формы входа
    /// </summary>
    public class CredentialsModel : ObservableObject
    {
        public string Login { get => _login; set { _login = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string PasswordDublicate { get => _passwordDublicate; set { _passwordDublicate = value; OnPropertyChanged(); } }
        public bool ShowPassword { get => _showPassword; set { _showPassword = value; OnPropertyChanged(); } }


        private string _login;
        private string _passwordDublicate;
        private string _password;
        private bool _showPassword;
    }
}
