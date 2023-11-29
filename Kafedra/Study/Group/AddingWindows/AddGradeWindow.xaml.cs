using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Kafedra.Study.Group.AddingWindows
{
    /// <summary>
    /// Логика взаимодействия для AddGradeWindow.xaml
    /// </summary>
    public partial class AddGradeWindow : Window
    {
        public AddGradeWindow()
        {
            InitializeComponent();

            FillSupervisedGroupComboBox();
            FillDisciplineComboBox();
            FillTypeWorkComboBox();
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
                cbGroup.ItemsSource = dataTable.DefaultView;
                cbGroup.DisplayMemberPath = "GroupName";
                cbGroup.SelectedValuePath = "SupervisedGroupID";

            }
        }


        public void FillDisciplineComboBox()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT DisciplineID, DisciplineName FROM Discipline", connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Disciplines");
                dataAdapter.Fill(dataTable);
                cbDiscipline.ItemsSource = dataTable.DefaultView;
                cbDiscipline.DisplayMemberPath = "DisciplineName";
                cbDiscipline.SelectedValuePath = "DisciplineID";
            }
        }

        public void FillTypeWorkComboBox()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TypeWorkID, TypeWorkName FROM TypeWork", connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("TypeWorks");
                dataAdapter.Fill(dataTable);
                cbTypeWork.ItemsSource = dataTable.DefaultView;
                cbTypeWork.DisplayMemberPath = "TypeWorkName";
                cbTypeWork.SelectedValuePath = "TypeWorkID";
            }
        }


        private void AddGrade_Click(object sender, RoutedEventArgs e)
        {
            int groupID = (int)cbGroup.SelectedValue;
            int disciplineID = (int)cbDiscipline.SelectedValue;
            int typeWorkID = (int)cbTypeWork.SelectedValue;
            float rating = float.Parse(tbRating.Text);

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddGrade", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupervisedGroupID", groupID);
                command.Parameters.AddWithValue("@DisciplineID", disciplineID);
                command.Parameters.AddWithValue("@TypeWorkID", typeWorkID);
                command.Parameters.AddWithValue("@AverageRating", rating);
                command.ExecuteNonQuery();
            }

            this.Close();

            MessageBox.Show("Оценка добавлена!");

        }

        private void cbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbTypeWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
