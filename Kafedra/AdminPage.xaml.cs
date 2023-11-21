using System.Windows;
using System.Windows.Controls;

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(string name)
        {
            InitializeComponent();
            _label.Content = name;
        }

        private void EDMat_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new EducationMaterialsPage();
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new EventsControllerPage();
        }
    }
}
