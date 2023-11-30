using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Kafedra.Study.Group.AddingWindows;

namespace Kafedra.Study.Group
{
    public partial class GroupPage : Page
    {
        public GroupPage()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            FillSupervisedGroupComboBox();
            FillSupervisedGroupComboBox1();
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
                    string groupInfo = row["GroupName"].ToString() + " (" + row["StudentsCount"].ToString() + " студентов)";
                    row["GroupName"] = groupInfo;
                }
                GroupsComboBox.ItemsSource = dataTable.DefaultView;
                GroupsComboBox.DisplayMemberPath = "GroupName";
                GroupsComboBox.SelectedValuePath = "SupervisedGroupID";

                _groupDataGrade.ItemsSource = dataTable.DefaultView;
            }
        }



        private void GroupsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupsComboBox.SelectedValue != null)
            {
                int groupId = (int)GroupsComboBox.SelectedValue;
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetGroupInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GroupID", groupId);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable("GroupInfo");
                    dataAdapter.Fill(dataTable);
                    _groupDataGrade.ItemsSource = dataTable.DefaultView;
                    _groupDataGrade.Columns[0].Visibility = Visibility.Collapsed; 
                }
            }
        }

        private void UpdateOnClose(object sender, EventArgs e)
        {
            Update();
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupWindow GroupWindow = new GroupWindow();
            GroupWindow.ShowDialog();
            Update();

            GroupWindow.Closed += UpdateOnClose;
        }
        private void EditGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (_groupDataGrade.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)_groupDataGrade.SelectedItem;
                int groupId = (int)rowView["SupervisedGroupID"];

                EditGroupWindow editGroupWindow = new EditGroupWindow(groupId);
                editGroupWindow.ShowDialog();
                Update();

                editGroupWindow.Closed += UpdateOnClose;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу для редактирования.");
            }
        }
        private void DeleteGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void _groupDataGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AddGrade_Click(object sender, RoutedEventArgs e)
        {
            AddGradeWindow GroupWindow = new AddGradeWindow();
            GroupWindow.ShowDialog();
            Update();

            GroupWindow.Closed += UpdateOnClose;
        }

        private void GroupsComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupsComboBox1.SelectedValue != null)
            {
                int groupId = (int)GroupsComboBox1.SelectedValue;
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetGradesByGroupID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GroupID", groupId);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable("GroupInfo");
                    dataAdapter.Fill(dataTable);
                    _groupData_GetGrade.ItemsSource = dataTable.DefaultView;

                    _groupData_GetGrade.Columns[0].Visibility = Visibility.Collapsed;
                    _groupData_GetGrade.Columns[1].Visibility = Visibility.Collapsed;
                    _groupData_GetGrade.Columns[2].Visibility = Visibility.Collapsed;
                    _groupData_GetGrade.Columns[4].Visibility = Visibility.Collapsed;


                }
            }
        }

        public void FillSupervisedGroupComboBox1()
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
                    string groupInfo = row["GroupName"].ToString() + " (" + row["StudentsCount"].ToString() + " студентов)";
                    row["GroupName"] = groupInfo;
                }
                GroupsComboBox1.ItemsSource = dataTable.DefaultView;
                GroupsComboBox1.DisplayMemberPath = "GroupName";
                GroupsComboBox1.SelectedValuePath = "SupervisedGroupID";

                _groupData_GetGrade.ItemsSource = dataTable.DefaultView;
            }
        }
        private void EditGrade_Click(object sender, RoutedEventArgs e)
        {
            if (_groupData_GetGrade.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)_groupData_GetGrade.SelectedItem;
                int gradeid = (int)rowView["GradeID"];

                EditGradeWindow EditGradeWindow = new EditGradeWindow(gradeid);
                EditGradeWindow.ShowDialog();
                Update();

                EditGradeWindow.Closed += UpdateOnClose;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_groupData_GetGrade.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)_groupData_GetGrade.SelectedItem;
                int gradeID = (int)rowView["GradeID"];

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteGradeByID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GradeID", gradeID);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Оценка удалена!");
                Update();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите оценку для удаления.");
            }
        }

    }
}
