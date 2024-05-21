using Landfill.Common.Helpers;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Landfill.MVVM.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = 
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordChanged, null, false, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        public static readonly DependencyProperty IsPasswordVisibleProperty =
            DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(CustomPasswordBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ShowPasswordChanged, null, false, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public bool IsPasswordVisible
        {
            get { return (bool)GetValue(IsPasswordVisibleProperty); }
            set { SetValue(IsPasswordVisibleProperty, value); }
        }

        public CustomPasswordBox()
        {
            InitializeComponent();
            textPasswordHidden.PasswordChanged += (o, s) => OnPasswordChanged();
            textPassword.TextChanged += (o, s) => OnPasswordChanged();
        }

        private void OnPasswordChanged()
        {
            Password = IsPasswordVisible ? textPassword.Text : textPasswordHidden.Password;
        }

        private static void ShowPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomPasswordBox passwordBox)
            {
                passwordBox.ChangePasswordVisibility((bool)e.NewValue);
            }
        }

        private static void PasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomPasswordBox passwordBox && !(e.NewValue as string).IsNullOrWhiteSpace())
            {
                passwordBox.SetPasswordBoxes(e.NewValue as string);
            }
        }

        private void ChangePasswordVisibility(bool value)
        {
            IsPasswordVisible = value;
            if (value)
            {
                textPasswordHidden.Visibility = Visibility.Hidden;
                textPassword.Visibility = Visibility.Visible;
            }
            else
            {
                textPasswordHidden.Visibility = Visibility.Visible;
                textPassword.Visibility = Visibility.Hidden;
            }
            SetPasswordBoxes(Password);
        }

        private void SetPasswordBoxes(string password)
        {
            textPassword.Text = password;
            textPasswordHidden.Password = password;
            textPasswordHidden.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(textPasswordHidden, new object[] { Password.Length, 0 });
        }
    }
}
