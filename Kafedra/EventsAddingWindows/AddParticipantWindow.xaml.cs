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
    /// Логика взаимодействия для AddParticipantWindow.xaml
    /// </summary>
    public partial class AddParticipantWindow : Window
    {
        public AddParticipantWindow()
        {
            InitializeComponent();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string patronomyc = PatronomycTextBox.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronomyc))
            {
                MessageBox.Show("Заполните все поля перед сохранением.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Participants (FirstName, LastName, Patronymic) VALUES (@firstName, @lastName, @patronymic)", connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@patronymic", patronomyc);
                command.ExecuteNonQuery();
            }

            this.Close();
        }

    }
}
