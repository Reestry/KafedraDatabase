using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Kafedra.ProjectsWindows
{
    /// <summary>
    /// Логика взаимодействия для ProjectAddWindow.xaml
    /// </summary>
    public partial class ProjectAddWindow : Window
    {
        public ProjectAddWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string eventType = EventTypeTextBox.Text;
            string status = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            // Выполните ваш запрос к базе данных с использованием eventType и status

            // Пример обновленного триггера
            // Обратите внимание, что вам, возможно, придется внести изменения в ваш запрос в зависимости от структуры вашей базы данных
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Projects (TypeOfProject, Status) VALUES (@TypeOfProject, @Status)", connection))
                {
                    command.Parameters.AddWithValue("@TypeOfProject", eventType);
                    command.Parameters.AddWithValue("@Status", status);
                    command.ExecuteNonQuery();
                }
            }
            
            this.Close();
        }
    }
}
