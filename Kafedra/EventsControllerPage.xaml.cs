using Kafedra.EventsAddingWindows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для EventsControllerPage.xaml
    /// </summary>
    public partial class EventsControllerPage : Page
    {

        public EventsControllerPage()
        {
            InitializeComponent();
            LoadParticipants();
            LoadEvents();
            LoadDataGrid();
            LoadGuests();
            LoadEvents1();

            LoadGuestDataGrid();


            dataGrid.LayoutUpdated += DataGrid_LayoutUpdated;
            dataGrid1.LayoutUpdated += DataGrid_LayoutUpdatedGuests;
        }

        private void EditEventWindow_Closed(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            LoadParticipants();
            LoadEvents();
            LoadDataGrid();
            LoadGuests();
            LoadEvents1();

            LoadGuestDataGrid();
        }

        #region Participant

        private void LoadParticipants()
        {
            participantsComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Participants", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    item.Tag = reader["ParticipantsID"];
                    participantsComboBox.Items.Add(item);
                }
            }
        }

        private void LoadEvents()
        {
            eventsComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Events", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = reader["EventName"].ToString();
                    item.Tag = reader["EventsID"];
                    eventsComboBox.Items.Add(item);

                }
            }
        }

        private void LoadDataGrid()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM GetParticipantEvents()", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
            }


        }

        private void AssignEventButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedParticipant = (ComboBoxItem)participantsComboBox.SelectedItem;
            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox.SelectedItem;
            int participantId = (int)selectedParticipant.Tag;
            int eventId = (int)selectedEvent.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Events_Participants (FKParticipantsID, FKEventsID) VALUES (@participantId, @eventId)", connection);
                command.Parameters.AddWithValue("@participantId", participantId);
                command.Parameters.AddWithValue("@eventId", eventId);
                command.ExecuteNonQuery();
            }

            LoadDataGrid(); // Обновите DataGrid после назначения мероприятия
        }

        private void RemoveAssignmentButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dataGrid.SelectedItem;
            int assignmentId = (int)row["Events_ParticipantsID"];

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC DeleteEventParticipant @Events_ParticipantsID", connection);
                command.Parameters.AddWithValue("@Events_ParticipantsID", assignmentId);
                command.ExecuteNonQuery();
            }

            LoadDataGrid();
        }

        private void AddParticipants_Click(object sender, RoutedEventArgs e)
        {
            AddParticipantWindow addParticipantWindow = new AddParticipantWindow();
            addParticipantWindow.ShowDialog();
            LoadParticipants();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow();
            addEventWindow.ShowDialog();
            LoadEvents();
        }

        private void EventEdt_Click(object sender, RoutedEventArgs e)
        {

            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox.SelectedItem;
            int eventId = (int)selectedEvent.Tag;


            EditEventWindow editEventWindow = new EditEventWindow(eventId);
            editEventWindow.Closed += EditEventWindow_Closed;
            editEventWindow.Show();
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox.SelectedItem;
            int eventId = (int)selectedEvent.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                // Сначала удалите связанные записи из таблицы Events_Participants
                SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKEventsID = @eventId", connection);
                command1.Parameters.AddWithValue("@eventId", eventId);
                command1.ExecuteNonQuery();

                // Затем удалите само мероприятие
                SqlCommand command2 = new SqlCommand("DELETE FROM Events WHERE EventsID = @eventId", connection);
                command2.Parameters.AddWithValue("@eventId", eventId);
                command2.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void DeletePartButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedParticipant = (ComboBoxItem)participantsComboBox.SelectedItem;
            int participantId = (int)selectedParticipant.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKParticipantsID = @participantId", connection);
                command1.Parameters.AddWithValue("@participantId", participantId);
                command1.ExecuteNonQuery();

                SqlCommand command2 = new SqlCommand("DELETE FROM Participants WHERE ParticipantsID = @participantId", connection);
                command2.Parameters.AddWithValue("@participantId", participantId);
                command2.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void EditPartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            // Скройте столбец Events_ParticipantsID
            if (dataGrid.Columns.Count > 0)
            {
                dataGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }

        #endregion



        #region Guests
        private void DataGrid_LayoutUpdatedGuests(object sender, EventArgs e)
        {
            // Скройте столбец Events_ParticipantsID
            if (dataGrid1.Columns.Count > 0)
            {
                dataGrid1.Columns[0].Visibility = Visibility.Hidden;
            }
        }

        private void LoadEvents1()
        {
            eventsComboBox1.Items.Clear();

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Events", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = reader["EventName"].ToString();
                    item.Tag = reader["EventsID"];
                    eventsComboBox1.Items.Add(item);

                }
            }
        }

        private void LoadGuestDataGrid()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from GetGuestsEvents()", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            AddGuestWindow addGuestWindow = new AddGuestWindow();
            addGuestWindow.ShowDialog();
            LoadParticipants();
        }

        private void LoadGuests()
        {
            guestsComboBox.Items.Clear();

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Guests", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    item.Tag = reader["GuestsID"];
                    guestsComboBox.Items.Add(item);
                }
            }
        }

        private void AddEvent_Click1(object sender, RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow();
            addEventWindow.ShowDialog();
            UpdateInfo();
        }

        private void DeleteEventButton_Click1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox1.SelectedItem;
            int eventId = (int)selectedEvent.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                // Сначала удалите связанные записи из таблицы Events_Participants
                SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKEventsID = @eventId", connection);
                command1.Parameters.AddWithValue("@eventId", eventId);
                command1.ExecuteNonQuery();

                // Затем удалите само мероприятие
                SqlCommand command2 = new SqlCommand("DELETE FROM Events WHERE EventsID = @eventId", connection);
                command2.Parameters.AddWithValue("@eventId", eventId);
                command2.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGuestButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedGuest = (ComboBoxItem)guestsComboBox.SelectedItem;
            int guestId = (int)selectedGuest.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand("DELETE FROM Events_Guests WHERE FKGuestsID = @guestId", connection);
                command1.Parameters.AddWithValue("@guestId", guestId);
                command1.ExecuteNonQuery();

                SqlCommand command2 = new SqlCommand("DELETE FROM Guests WHERE GuestsID = @guestsId", connection);
                command2.Parameters.AddWithValue("@guestsId", guestId);
                command2.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void EventEdt_Click1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox1.SelectedItem;
            int eventId = (int)selectedEvent.Tag;


            EditEventWindow editEventWindow = new EditEventWindow(eventId);
            editEventWindow.Closed += EditEventWindow_Closed;
            editEventWindow.Show();

        
        }

        private void AssignEventButton_Click1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedParticipant = (ComboBoxItem)guestsComboBox.SelectedItem;
            ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox1.SelectedItem;
            int guestId = (int)selectedParticipant.Tag;
            int eventId = (int)selectedEvent.Tag;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Events_Guests (FKGuestsID, FKEventsID) VALUES (@guestId, @eventId)", connection);
                command.Parameters.AddWithValue("@guestId", guestId);
                command.Parameters.AddWithValue("@eventId", eventId);
                command.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void RemoveAssignmentButton_Click1(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dataGrid1.SelectedItem;
            int assignmentId = (int)row["Events_GuestsID"];

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC DeleteEventGuest @Events_GuestsID", connection);
                command.Parameters.AddWithValue("@Events_GuestsID", assignmentId);
                command.ExecuteNonQuery();
            }

            UpdateInfo();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion


    }
}
