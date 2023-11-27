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
    /// Логика взаимодействия для DisciplineWindow.xaml
    /// </summary>
    public partial class DisciplineWindow : Window
    {

        public string DisciplineName { get; set; }
        public DisciplineWindow(string disciplineName = "")
        {
            InitializeComponent();
            DisciplineNameTextBox.Text = disciplineName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DisciplineName = DisciplineNameTextBox.Text;
            this.DialogResult = true;
        }
    }
}
