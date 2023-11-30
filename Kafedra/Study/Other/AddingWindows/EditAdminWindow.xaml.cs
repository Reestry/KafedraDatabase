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
    /// Логика взаимодействия для EditAdminWindow.xaml
    /// </summary>
    public partial class EditAdminWindow : Window
    {
        private DataRowView adminRow;

        public EditAdminWindow(DataRowView adminRow)
        {
            InitializeComponent();

            this.adminRow = adminRow;

            firstNameTextBox.Text = adminRow["FirstName"].ToString();
            lastNameTextBox.Text = adminRow["LastName"].ToString();
            patronymicTextBox.Text = adminRow["Patronymic"].ToString();
            loginTextBox.Text = adminRow["Login"].ToString();
            passwordTextBox.Text = adminRow["Password"].ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) || string.IsNullOrWhiteSpace(lastNameTextBox.Text) || string.IsNullOrWhiteSpace(loginTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string connectionString = SQLConnection.connectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("EditAdmin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AdminID", adminRow["AdminID"]);
                    command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
                    command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
                    command.Parameters.AddWithValue("@Patronymic", patronymicTextBox.Text);
                    command.Parameters.AddWithValue("@Login", loginTextBox.Text);
                    command.Parameters.AddWithValue("@Password", passwordTextBox.Text);

                    command.ExecuteNonQuery();
                }

                this.Close();
                MessageBox.Show("Информация об администраторе обновлена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при редактировании администратора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
