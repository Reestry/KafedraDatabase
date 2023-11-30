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
            string status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(eventType) || string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении проекта. Подробности: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
