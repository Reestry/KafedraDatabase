﻿using System.Windows;
using System.Windows.Controls;

namespace Kafedra.UserPages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage(string name)
        {
            InitializeComponent();
            _label.Content = name;

            _label.Content = MainWindow.WriteName;
        }


        private void EDMat_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new EducationMaterialsPage();
            MyFrame.Content = new UserEducationMaterialsPage();
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new EventsControllerPage();
            MyFrame.Content = new UserEventsControllerPage();
        }

        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new Projects();
            MyFrame.Content = new UserProjectsPage();
        }

        private void Teachers_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new TeacherPage();
            MyFrame.Content = new _UserTeachersPage();
        }

        private void Disciplines_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new DisciplinesPage();
        }

        private void Grop_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new GroupPage();
        }

        private void Other_Click(object sender, RoutedEventArgs e)
        {
            //MyFrame.Content = new OtherPage();

        }

    }
}
