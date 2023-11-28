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
using Kafedra.Study.Group.AddingWindows;

namespace Kafedra.Study.Group
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
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
    }
}
