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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kafedra.Study.Teacher.TeacherWindows;

namespace Kafedra.Study.Teacher
{
    /// <summary>
    /// Логика взаимодействия для TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {

        private int _teacherId;

        public TeacherPage()
        {
            InitializeComponent();
            Update();

            _teachersGrid.LayoutUpdated += DataGrid_LayoutUpdated;
        }
        private void Update()
        {
            FillDataGrid();
            FillTeachersComboBox();
        }

        #region AddTeacher
        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {

            foreach (var column in _teachersGrid.Columns)
            {
                if (column.Header.ToString() == "PersonID" || column.Header.ToString() == "TeacherID" || column.Header.ToString() == "PostID")
                {
                    column.Visibility = Visibility.Hidden;
                }
            }

        }



        public void FillDataGrid()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetTeacherInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("TeacherInfo");
                dataAdapter.Fill(dataTable);
                _teachersGrid.ItemsSource = dataTable.DefaultView;
                dataAdapter.Update(dataTable);
            }
        }
        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeacherWindow AddTeacherWindow = new AddTeacherWindow();
            AddTeacherWindow.ShowDialog();
            Update();

            AddTeacherWindow.Closed += UpdateOnClose;
        }

        private void UpdateOnClose(object sender, EventArgs e)
        {
            Update();
        }

        private void DeleteTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (_teachersGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)_teachersGrid.SelectedItem;
                int teacherId = Convert.ToInt32(row["TeacherID"]);

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteTeacher", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeacherID", teacherId);
                    command.ExecuteNonQuery();
                }

                FillDataGrid();
                Update();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion


        #region Disciplines


        private void TeachersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeachersComboBox.SelectedValue != null)
            {
                int teacherId = (int)TeachersComboBox.SelectedValue;
                _teacherId = teacherId;
                FillDataGrid(teacherId);
            }
        }

        public void FillTeachersComboBox()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TeacherID, FirstName, LastName, Patronymic FROM Teacher INNER JOIN Person ON Teacher.FKPersonID = Person.PersonID", connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Teachers");
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    string fullName = row["FirstName"].ToString() + " " + row["LastName"].ToString();
                    if (row["Patronymic"] != DBNull.Value)
                    {
                        fullName += " " + row["Patronymic"].ToString();
                    }
                    row["FirstName"] = fullName;
                }
                TeachersComboBox.ItemsSource = dataTable.DefaultView;
                TeachersComboBox.DisplayMemberPath = "FirstName";
                TeachersComboBox.SelectedValuePath = "TeacherID";
            }
        }


        public void FillDataGrid(int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetTeacherDisciplines", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TeacherID", teacherId);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("TeacherDisciplines");
                dataAdapter.Fill(dataTable);
                _teachers_disciplinesGrid.ItemsSource = dataTable.DefaultView;
                dataAdapter.Update(dataTable);
            }
        }

        private void AssignTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherAssignWindow TeacherAssignWindow = new TeacherAssignWindow(_teacherId);
            TeacherAssignWindow.ShowDialog();
            Update();

            TeacherAssignWindow.Closed += UpdateOnClose;
        }
        #endregion

    }
}
