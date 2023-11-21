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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Name;
        public MainWindow()
        {
            InitializeComponent();
            LoginTextBox.Text = "admin1";
            PasswordBox.Password = "password1";
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "Data Source=WIN-TSLNADACF9B\\SQLEXPRESS;Initial Catalog=Kafedra;Integrated Security=True";
                string queryAdmin = $"SELECT * FROM Admin WHERE Login='{LoginTextBox.Text}' AND Password='{PasswordBox.Password}'";
                string queryTeacher = $"SELECT * FROM Teacher WHERE Login='{LoginTextBox.Text}' AND Password='{PasswordBox.Password}'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryAdmin, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Close();
                        command = new SqlCommand($"SELECT FirstName, LastName, Patronymic FROM Person WHERE PersonID IN (SELECT FKPersonID FROM Admin WHERE Login='{LoginTextBox.Text}' AND Password='{PasswordBox.Password}')", connection);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show($"Добро пожаловать, админ {reader["FirstName"]} {reader["LastName"]} {reader["Patronymic"]}!");

                            var name = $"Добро пожаловать, {reader["FirstName"]} {reader["LastName"]} {reader["Patronymic"]}!";
                            Frame frame = new Frame();
                            frame.Navigate(new AdminPage(name));
                            Content = frame;
                        }
                    }
                    else
                    {
                        reader.Close();
                        command = new SqlCommand(queryTeacher, connection);
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Close();
                            command = new SqlCommand($"SELECT FirstName, LastName, Patronymic FROM Person WHERE PersonID IN (SELECT FKPersonID FROM Teacher WHERE Login='{LoginTextBox.Text}' AND Password='{PasswordBox.Password}')", connection);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                MessageBox.Show($"Добро пожаловать, учитель {reader["FirstName"]} {reader["LastName"]} {reader["Patronymic"]}!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин и пароль");
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

        }
    }
}
