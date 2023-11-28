using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Kafedra.Study.Other.AddingWindows
{
    /// <summary>
    /// Логика взаимодействия для AddAdminWindow.xaml
    /// </summary>
    public partial class AddAdminWindow : Window
    {
        public AddAdminWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = SQLConnection.connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CreateAdmin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
                command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
                command.Parameters.AddWithValue("@Patronymic", patronymicTextBox.Text);
                command.Parameters.AddWithValue("@Login", loginTextBox.Text);
                command.Parameters.AddWithValue("@Password", passwordTextBox.Text);

                command.ExecuteNonQuery();
            }

            this.Close();
        }
    }
}
