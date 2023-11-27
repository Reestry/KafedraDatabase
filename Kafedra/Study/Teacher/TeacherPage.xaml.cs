using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
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
            FillSupervisedGroupComboBox();
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


        private void AssignTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherAssignWindow TeacherAssignWindow = new TeacherAssignWindow(_teacherId);
            TeacherAssignWindow.ShowDialog();
            Update();

            TeacherAssignWindow.Closed += UpdateOnClose;
        }

        private void DeleteAssignTeacher_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = _teachers_disciplinesGrid.SelectedItem as DataRowView;
            if (rowView != null)
            {
                int teacherId = _teacherId;
                int typeWork_Specialization_DisciplineID = Convert.ToInt32(rowView["TypeWork_Specialization_DisciplineID"]);
                int groupId = Convert.ToInt32(rowView["GroupID"]);

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("RemoveTeacherAssignment", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeacherID", teacherId);
                    command.Parameters.AddWithValue("@TypeWork_Specialization_DisciplineID", typeWork_Specialization_DisciplineID);
                    command.Parameters.AddWithValue("@GroupID", groupId);
                    command.ExecuteNonQuery();
                }

                FillDataGrid(teacherId);
            }
        }

        #endregion

        #region SupGroup

        private int GroupID;
        private void AssignSupGroup_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного преподавателя и выбранную группу
            var selectedTeacherID = SupTeachersComboBox.SelectedValue;
            var selectedGroupID = GroupID;

            // Проверяем, выбрал ли пользователь элементы
            if (selectedTeacherID == null || selectedGroupID == null)
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя и группу.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AssignGroupToTeacher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TeacherID", (int)selectedTeacherID));
                    command.Parameters.Add(new SqlParameter("@SupervisedGroupID", (int)selectedGroupID));

                    command.ExecuteNonQuery();
                }
            }
            TeachersComboBox_SelectionChanged1(null, null);
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



        private void DeleteAssignSupGroup_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную группу из DataGrid
            var selectedGroup = _teachers_Group_ass.SelectedItem as DataRowView;
            if (selectedGroup == null)
            {
                MessageBox.Show("Пожалуйста, выберите группу.");
                return;
            }

            var selectedGroupID = (int)selectedGroup["SupervisedGroupID"];

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("RemoveGroupFromTeacher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SupervisedGroupID", selectedGroupID));

                    command.ExecuteNonQuery();
                }
            }

            TeachersComboBox_SelectionChanged1(null, null);
        }

        #endregion
    }
}
