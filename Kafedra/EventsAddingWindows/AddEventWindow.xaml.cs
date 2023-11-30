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

namespace Kafedra.EventsAddingWindows
{
    /// <summary>
    /// Логика взаимодействия для AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public AddEventWindow()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string eventType = EventTypeTextBox.Text;
            string eventName = EventNameTextBox.Text;
            DateTime? eventDate = EventDatePicker.SelectedDate;

            if (string.IsNullOrEmpty(eventType) || string.IsNullOrEmpty(eventName) || !eventDate.HasValue)
            {
                MessageBox.Show("Заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Events (EventType, EventName, EventDate) VALUES (@eventType, @eventName, @eventDate)", connection);
                command.Parameters.AddWithValue("@eventType", eventType);
                command.Parameters.AddWithValue("@eventName", eventName);
                command.Parameters.AddWithValue("@eventDate", eventDate.Value);
                command.ExecuteNonQuery();
            }

            this.Close();
        }
    }
}
