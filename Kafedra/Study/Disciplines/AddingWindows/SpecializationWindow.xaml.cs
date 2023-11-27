using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kafedra.Study.Disciplines.AddingWindows
{
    /// <summary>
    /// Логика взаимодействия для SpecializationWindow.xaml
    /// </summary>
    public partial class SpecializationWindow : Window
    {
        public string SpecializationName { get; set; }
        public SpecializationWindow(string specializationName = "")
        {
            InitializeComponent();
            SpecializationNameTextBox.Text = specializationName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SpecializationName = SpecializationNameTextBox.Text;
            this.DialogResult = true;
        }
    }
}
