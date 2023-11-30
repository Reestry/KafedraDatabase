using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Kafedra.Study.Teacher.TeacherWindows
{
    public partial class TeacherAssignWindow : Window
    {
        private int _teacherId;
        public TeacherAssignWindow(int _teacherID)
        {
            InitializeComponent();

            UpdateInfo();
            _teacherId = _teacherID;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_teacherId <= 0 || TypeWork_Specialization_Discipline.SelectedValue == null || string.IsNullOrWhiteSpace(AverageTime.Text) || GroupName.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(AverageTime.Text, out int averageTime))
            {
                MessageBox.Show("Введите корректное значение для среднего времени.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AssignTeacherToDiscipline", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@TeacherID", _teacherId));
                        command.Parameters.Add(new SqlParameter("@TypeWork_Specialization_DisciplineID", (int)TypeWork_Specialization_Discipline.SelectedValue));
                        command.Parameters.Add(new SqlParameter("@AwarageTime", averageTime));
                        command.Parameters.Add(new SqlParameter("@GroupID", (int)GroupName.SelectedValue));

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Преподаватель успешно назначен на дисциплину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при назначении преподавателя на дисциплину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateInfo()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                // Заполнение ComboBox 'TypeWork_Specialization_Discipline'
                using (SqlCommand command = new SqlCommand("GetFullInfoForTypeWork_Specialization_Discipline", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        TypeWork_Specialization_Discipline.ItemsSource = reader.Cast<DbDataRecord>().ToList();
                        TypeWork_Specialization_Discipline.DisplayMemberPath = "FullInfo"; // отображаем полную информацию
                        TypeWork_Specialization_Discipline.SelectedValuePath = "TypeWork_Specialization_DisciplineID";
                    }
                }

                // Заполнение ComboBox 'GroupName'
                using (SqlCommand command = new SqlCommand("SELECT SupervisedGroupID, GroupName FROM SupervisedGroup", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        GroupName.ItemsSource = reader.Cast<DbDataRecord>().ToList();
                        GroupName.DisplayMemberPath = "GroupName"; // или другое поле
                        GroupName.SelectedValuePath = "SupervisedGroupID";
                    }
                }
            }
        }

        private void TypeWork_Specialization_Discipline_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
