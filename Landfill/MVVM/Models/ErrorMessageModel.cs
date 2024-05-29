namespace Landfill.MVVM.Models
{
    /// <summary>
    /// Модель сообщений с ошибками
    /// </summary>
    public class ErrorMessageModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordDublicate { get; set; }
    }
}
