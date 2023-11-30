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
    /// Логика взаимодействия для EditGuestWindow.xaml
    /// </summary>
    public partial class EditGuestWindow : Window
    {
        private int guestId;

        public EditGuestWindow(int guestId)
        {
            InitializeComponent();
            this.guestId = guestId;
            LoadGuestInfo();
        }

        private void LoadGuestInfo()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Guests WHERE GuestsID = @guestId", connection);
                command.Parameters.AddWithValue("@guestId", guestId);
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

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic))
            {
                MessageBox.Show("Заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Guests SET FirstName = @firstName, LastName = @lastName, Patronymic = @patronymic WHERE GuestsID = @guestId", connection);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@patronymic", patronymic);
                    command.Parameters.AddWithValue("@guestId", guestId);

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
