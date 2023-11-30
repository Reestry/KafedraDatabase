using System;
using System.Data.SqlClient;
using System.Windows;

namespace Kafedra.EventsAddingWindows
{

    public partial class EditEventWindow : Window
    {
        private int eventId;

        public EditEventWindow(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
            LoadEventInfo();
        }

        private void LoadEventInfo()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Events WHERE EventsID = @eventId", connection);
                command.Parameters.AddWithValue("@eventId", eventId);
                SqlDataReader reader = command.ExecuteReader(); 
                if (reader.Read())
                {
                    EventTypeTextBox.Text = reader["EventType"].ToString();
                    EventNameTextBox.Text = reader["EventName"].ToString();
                    EventDatePicker.SelectedDate = (DateTime)reader["EventDate"];
                }
            }
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

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Events SET EventType = @eventType, EventName = @eventName, EventDate = @eventDate WHERE EventsID = @eventId", connection);
                    command.Parameters.AddWithValue("@eventType", eventType);
                    command.Parameters.AddWithValue("@eventName", eventName);
                    command.Parameters.AddWithValue("@eventDate", eventDate.Value);
                    command.Parameters.AddWithValue("@eventId", eventId);

                    command.ExecuteNonQuery();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обновлении данных. Подробности: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
