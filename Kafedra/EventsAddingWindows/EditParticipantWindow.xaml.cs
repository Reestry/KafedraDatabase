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
    /// Логика взаимодействия для EditParticipantWindow.xaml
    /// </summary>
    public partial class EditParticipantWindow : Window
    {
        private int participantId;

        public EditParticipantWindow(int participantId)
        {
            InitializeComponent();
            this.participantId = participantId;
            LoadParticipantInfo();
        }

        private void LoadParticipantInfo()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Participants WHERE ParticipantsID = @participantId", connection);
                command.Parameters.AddWithValue("@participantId", participantId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FirstNameTextBox.Text = reader["FirstName"].ToString();
                    LastNameTextBox.Text = reader["LastName"].ToString();
                    PatronymicTextBox.Text = reader["Patronymic"].ToString();
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string patronymic = PatronymicTextBox.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Participants SET FirstName = @firstName, LastName = @lastName, Patronymic = @patronymic WHERE ParticipantsID = @participantId", connection);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@patronymic", patronymic);
                    command.Parameters.AddWithValue("@participantId", participantId);

                    command.ExecuteNonQuery();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
