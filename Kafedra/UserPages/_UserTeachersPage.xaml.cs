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
using System.Text.RegularExpressions;

namespace Kafedra.UserPages
{
    /// <summary>
    /// Логика взаимодействия для _UserTeachersPage.xaml
    /// </summary>
    public partial class _UserTeachersPage : Page
    {
        private int _teacherId;
        private int GroupID;
        public _UserTeachersPage()
        {
            InitializeComponent();
            Update();

            _teachersGrid.LayoutUpdated += DataGrid_LayoutUpdated;
        }
        private void Update()
        {
            FillDataGrid();
            FillTeachersComboBox();
            FillSupervisedGroupComboBox();
        }
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
        private void UpdateOnClose(object sender, EventArgs e)
        {
            Update();
        }
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

                SupTeachersComboBox.ItemsSource = dataTable.DefaultView;
                SupTeachersComboBox.DisplayMemberPath = "FirstName";
                SupTeachersComboBox.SelectedValuePath = "TeacherID";


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

                if (!dataTable.Columns.Contains("TypeWork_Specialization_DisciplineID"))
                {
                    DataColumn column = new DataColumn("TypeWork_Specialization_DisciplineID", typeof(int));
                    dataTable.Columns.Add(column);
                }

                if (!dataTable.Columns.Contains("GroupID"))
                {
                    DataColumn column = new DataColumn("GroupID", typeof(int));
                    dataTable.Columns.Add(column);
                }

                _teachers_disciplinesGrid.AutoGeneratingColumn += (sender, e) =>
                {
                    if (e.PropertyName == "TypeWork_Specialization_DisciplineID" || e.PropertyName == "GroupID")
                    {
                        e.Cancel = true;
                    }
                };

                _teachers_disciplinesGrid.ItemsSource = dataTable.DefaultView;
                dataAdapter.Update(dataTable);
            }
        }
        private void TeachersComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранного преподавателя
            var selectedTeacherID = (int)SupTeachersComboBox.SelectedValue;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetGroupsByTeacher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TeacherID", selectedTeacherID));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Скрываем столбец SupervisedGroupID
                        _teachers_Group_ass.AutoGeneratingColumn += (s, args) =>
                        {
                            if (args.PropertyName == "SupervisedGroupID")
                            {
                                args.Cancel = true;
                            }
                        };

                        _teachers_Group_ass.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }
        public void FillSupervisedGroupComboBox()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT SupervisedGroupID, GroupName, StudentsCount FROM SupervisedGroup INNER JOIN Teacher ON SupervisedGroup.FKTeacherID = Teacher.TeacherID", connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("SupervisedGroups");
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    string groupInfo = row["GroupName"].ToString() + " (" + row["StudentsCount"].ToString() + " students)";
                    row["GroupName"] = groupInfo;
                }
                SupGroupComboBox.ItemsSource = dataTable.DefaultView;
                SupGroupComboBox.DisplayMemberPath = "GroupName";
                SupGroupComboBox.SelectedValuePath = "SupervisedGroupID";
            }
        }
        private void GroupsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGroupID = (int)SupGroupComboBox.SelectedValue;
            GroupID = selectedGroupID;
        }
    }
}
