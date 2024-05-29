using System.Windows.Controls;

namespace Landfill.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.ScrollIntoView(listView.SelectedItem);
        }
    }
}
