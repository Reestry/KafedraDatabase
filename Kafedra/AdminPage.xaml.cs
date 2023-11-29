using Kafedra.Study.Disciplines;
using Kafedra.Study.Group;
using Kafedra.Study.Other;
using Kafedra.Study.Teacher;
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

            _label.Content = MainWindow.WriteName;
        }

        private void EDMat_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new EducationMaterialsPage();
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new EventsControllerPage();
        }

        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Projects();
        }

        private void Teachers_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new TeacherPage();
        }

        private void Disciplines_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new DisciplinesPage();
        }

        private void Grop_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new GroupPage();
        }

        private void Other_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new OtherPage();

        }
    }
}
