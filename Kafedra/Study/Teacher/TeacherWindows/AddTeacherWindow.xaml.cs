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

namespace Kafedra.Study.Teacher.TeacherWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTeacherWindow.xaml
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        public AddTeacherWindow()
        {
            InitializeComponent();
            
            FillPostComboBox();
        }
        public void FillPostComboBox()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT PostID, PostName FROM Post", connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Posts");
                dataAdapter.Fill(dataTable);
                PostComboBox.ItemsSource = dataTable.DefaultView;
                PostComboBox.DisplayMemberPath = "PostName";
                PostComboBox.SelectedValuePath = "PostID";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(LastNameTextBox.Text) || string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Text) || PostComboBox.SelectedValue == null)
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

                    SqlCommand command = new SqlCommand("AddNewTeacher", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                    command.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                    command.Parameters.AddWithValue("@Patronymic", PatronymicTextBox.Text);
                    command.Parameters.AddWithValue("@Login", LoginTextBox.Text);
                    command.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                    command.Parameters.AddWithValue("@PostID", PostComboBox.SelectedValue);

                    command.ExecuteNonQuery();
                }

                this.Close();
                MessageBox.Show("Новый учитель добавлен успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении нового учителя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
