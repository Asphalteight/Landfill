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
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null, null, false, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

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
            textPasswordHidden.PasswordChanged += OnPasswordChanged;
            textPassword.TextChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
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

        private void ChangePasswordVisibility(bool value)
        {
            IsPasswordVisible = value;
            if (value)
            {
                textPasswordHidden.Visibility = Visibility.Hidden;
                textPassword.Visibility = Visibility.Visible;
                textPassword.Text = Password;
            }
            else
            {
                textPasswordHidden.Visibility = Visibility.Visible;
                textPassword.Visibility = Visibility.Hidden;
                textPasswordHidden.Password = Password;
            }
        }
    }
}
