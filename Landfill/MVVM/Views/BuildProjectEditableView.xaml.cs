using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Landfill.MVVM.Views
{
    /// <summary>
    /// Interaction logic for BuildProjectEditableView.xaml
    /// </summary>
    public partial class BuildProjectEditableView : UserControl
    {
        public BuildProjectEditableView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
