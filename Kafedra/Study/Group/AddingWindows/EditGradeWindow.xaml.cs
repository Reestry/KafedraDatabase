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
    public partial class EditGradeWindow : Window
    {
        private int _gradeID;

        public EditGradeWindow(int gradeID)
        {
            InitializeComponent();

            _gradeID = gradeID;

            FillSupervisedGroupComboBox();
            FillDisciplineComboBox();
            FillTypeWorkComboBox();

            LoadGradeData();
        }

        private void LoadGradeData()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetGradesByGroupID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GroupID", _gradeID); // Используйте @GroupID вместо @GradeID
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cbGroup.SelectedValue = reader["FKSupervisedGroupID"];
                    cbDiscipline.SelectedValue = reader["FKDisciplineID"];
                    cbTypeWork.SelectedValue = reader["FKTypeWorkID"];
                    tbRating.Text = reader["AverageRating"].ToString();
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbGroup.SelectedValue == null || cbDiscipline.SelectedValue == null || cbTypeWork.SelectedValue == null || string.IsNullOrWhiteSpace(tbRating.Text))
            {
                MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(tbRating.Text, out float rating))
            {
                MessageBox.Show("Введите корректное число в поле Рейтинг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int groupID = (int)cbGroup.SelectedValue;
            int disciplineID = (int)cbDiscipline.SelectedValue;
            int typeWorkID = (int)cbTypeWork.SelectedValue;

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateGradeByGroupID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GroupID", groupID);
                    command.Parameters.AddWithValue("@GradeID", _gradeID);
                    command.Parameters.AddWithValue("@NewDisciplineID", disciplineID);
                    command.Parameters.AddWithValue("@NewTypeWorkID", typeWorkID);
                    command.Parameters.AddWithValue("@NewRating", rating);
                    command.ExecuteNonQuery();
                }

                this.Close();
                MessageBox.Show("Оценка обновлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении оценки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

