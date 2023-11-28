using System;
using System.Collections.Generic;
using System.Data;
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

namespace Kafedra.Study.Teacher.TeacherWindows
{
    /// <summary>
    /// Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public int TeacherID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public int PostID { get; private set; }

        public EditTeacherWindow(DataRowView row)
        {
            InitializeComponent();

            TeacherID = (int)row["TeacherID"];
            FirstNameTextBox.Text = (string)row["FirstName"];
            LastNameTextBox.Text = (string)row["LastName"];
            PatronymicTextBox.Text = (string)row["Patronymic"];
            LoginTextBox.Text = (string)row["Login"];
            PasswordTextBox.Text = (string)row["Password"];
            PostID = (int)row["PostID"];

            FillPostComboBox();
            PostComboBox.SelectedValue = PostID;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохраните изменения в полях преподавателя
            FirstName = FirstNameTextBox.Text;
            LastName = LastNameTextBox.Text;
            Patronymic = PatronymicTextBox.Text;
            Login = LoginTextBox.Text;
            Password = PasswordTextBox.Text;
            PostID = (int)PostComboBox.SelectedValue;

            this.DialogResult = true;
        }
    }

}
