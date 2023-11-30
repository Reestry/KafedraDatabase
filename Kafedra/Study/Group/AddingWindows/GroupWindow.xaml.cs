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

namespace Kafedra.Study.Group.AddingWindows
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            InitializeComponent();
            FillTeacherComboBox();
            FillSpecializationComboBox();
        }

        private void FillTeacherComboBox()
        {
            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT PersonID, FirstName, LastName FROM Person INNER JOIN Teacher ON Person.PersonID = Teacher.FKPersonID", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                TeacherComboBox.ItemsSource = dt.DefaultView;
                TeacherComboBox.DisplayMemberPath = "FirstName";
                TeacherComboBox.SelectedValuePath = "PersonID";
            }
        }

        private void FillSpecializationComboBox()
        {
            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT SpecializationID, SpecializationName FROM Specialization", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                SpecializationComboBox.ItemsSource = dt.DefaultView;
                SpecializationComboBox.DisplayMemberPath = "SpecializationName";
                SpecializationComboBox.SelectedValuePath = "SpecializationID";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TeacherComboBox.Text) || string.IsNullOrEmpty(SpecializationComboBox.Text) || string.IsNullOrEmpty(GroupNameTextBox.Text) || string.IsNullOrEmpty(StudentsCountTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!int.TryParse(StudentsCountTextBox.Text, out int studentsCount))
            {
                MessageBox.Show("Введите корректное число в поле Количество студентов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = SQLConnection.connectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("CreateNewGroup", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TeacherID", TeacherComboBox.SelectedValue);
                    command.Parameters.AddWithValue("@SpecializationID", SpecializationComboBox.SelectedValue);
                    command.Parameters.AddWithValue("@GroupName", GroupNameTextBox.Text);
                    command.Parameters.AddWithValue("@StudentsCount", studentsCount);

                    command.ExecuteNonQuery();
                }

                this.Close();
                MessageBox.Show("Группа успешно создана!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании группы: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
