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

        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            // Скройте столбец Events_ParticipantsID
            if (dataGrid.Columns.Count > 0)
            {
                dataGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }

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
            try
            {
                if (participantsComboBox.SelectedItem == null || eventsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите участника и событие перед назначением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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

                MessageBox.Show("Участник успешно назначен на событие!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при назначении участника на событие: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void RemoveAssignmentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Выберите назначение для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataRowView row = (DataRowView)dataGrid.SelectedItem;
                int assignmentId = (int)row["Events_ParticipantsID"];

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("EXEC DeleteEventParticipant @Events_ParticipantsID", connection);

                    command.Parameters.AddWithValue("@Events_ParticipantsID", assignmentId);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Назначение успешно удалено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении назначения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            try
            {
                if (eventsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите событие для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox.SelectedItem;
                int eventId = (int)selectedEvent.Tag;

                EditEventWindow editEventWindow = new EditEventWindow(eventId);
                editEventWindow.Closed += EditEventWindow_Closed;

                if (editEventWindow != null)
                {
                    editEventWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии окна редактирования события: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditPartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (participantsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите участника!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int participantId = (int)((ComboBoxItem)participantsComboBox.SelectedItem).Tag;

                EditParticipantWindow editParticipantWindow = new EditParticipantWindow(participantId);
                editParticipantWindow.Closed += EditEventWindow_Closed;

                if (editParticipantWindow != null)
                {
                    editParticipantWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии окна редактирования участника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (eventsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите событие для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox.SelectedItem;
                int eventId = (int)selectedEvent.Tag;

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKEventsID = @eventId", connection))
                    {
                        command1.Parameters.AddWithValue("@eventId", eventId);
                        command1.ExecuteNonQuery();
                    }

                    using (SqlCommand command2 = new SqlCommand("DELETE FROM Events WHERE EventsID = @eventId", connection))
                    {
                        command2.Parameters.AddWithValue("@eventId", eventId);
                        command2.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении события: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeletePartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (participantsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите участника для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ComboBoxItem selectedParticipant = (ComboBoxItem)participantsComboBox.SelectedItem;
                int participantId = (int)selectedParticipant.Tag;

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKParticipantsID = @participantId", connection))
                    {
                        command1.Parameters.AddWithValue("@participantId", participantId);
                        command1.ExecuteNonQuery();
                    }

                    using (SqlCommand command2 = new SqlCommand("DELETE FROM Participants WHERE ParticipantsID = @participantId", connection))
                    {
                        command2.Parameters.AddWithValue("@participantId", participantId);
                        command2.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении участника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            try
            {
                if (eventsComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Выберите событие для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ComboBoxItem selectedEvent = (ComboBoxItem)eventsComboBox1.SelectedItem;
                int eventId = (int)selectedEvent.Tag;

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Events_Participants WHERE FKEventsID = @eventId", connection))
                    {
                        command1.Parameters.AddWithValue("@eventId", eventId);
                        command1.ExecuteNonQuery();
                    }

                    using (SqlCommand command2 = new SqlCommand("DELETE FROM Events WHERE EventsID = @eventId", connection))
                    {
                        command2.Parameters.AddWithValue("@eventId", eventId);
                        command2.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении события: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (guestsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите гостя для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int guestId = (int)((ComboBoxItem)guestsComboBox.SelectedItem).Tag;

                EditGuestWindow editGuestWindow = new EditGuestWindow(guestId);
                editGuestWindow.Closed += EditEventWindow_Closed;

                if (editGuestWindow != null)
                {
                    editGuestWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии окна редактирования гостя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (guestsComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите гостя для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ComboBoxItem selectedGuest = (ComboBoxItem)guestsComboBox.SelectedItem;
                int guestId = (int)selectedGuest.Tag;

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand("DELETE FROM Events_Guests WHERE FKGuestsID = @guestId", connection))
                    {
                        command1.Parameters.AddWithValue("@guestId", guestId);
                        command1.ExecuteNonQuery();
                    }

                    using (SqlCommand command2 = new SqlCommand("DELETE FROM Guests WHERE GuestsID = @guestId", connection))
                    {
                        command2.Parameters.AddWithValue("@guestId", guestId);
                        command2.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении гостя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EventEdt_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (eventsComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Выберите событие для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int eventId = (int)((ComboBoxItem)eventsComboBox1.SelectedItem).Tag;

                EditEventWindow editEventWindow = new EditEventWindow(eventId);
                editEventWindow.Closed += EditEventWindow_Closed;

                if (editEventWindow != null)
                {
                    editEventWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при открытии окна редактирования события: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AssignEventButton_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (guestsComboBox.SelectedItem == null || eventsComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Выберите гостя и событие для привязки.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int guestId = (int)((ComboBoxItem)guestsComboBox.SelectedItem).Tag;
                int eventId = (int)((ComboBoxItem)eventsComboBox1.SelectedItem).Tag;

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO Events_Guests (FKGuestsID, FKEventsID) VALUES (@guestId, @eventId)", connection))
                    {
                        command.Parameters.AddWithValue("@guestId", guestId);
                        command.Parameters.AddWithValue("@eventId", eventId);
                        command.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при привязке гостя к событию: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void RemoveAssignmentButton_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid1.SelectedItem == null)
                {
                    MessageBox.Show("Выберите привязку гостя к событию для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataRowView row = (DataRowView)dataGrid1.SelectedItem;
                int assignmentId = (int)row["Events_GuestsID"];

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EXEC DeleteEventGuest @Events_GuestsID", connection))
                    {
                        command.Parameters.AddWithValue("@Events_GuestsID", assignmentId);
                        command.ExecuteNonQuery();
                    }
                }

                UpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении привязки гостя к событию: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        #endregion
    }
}
