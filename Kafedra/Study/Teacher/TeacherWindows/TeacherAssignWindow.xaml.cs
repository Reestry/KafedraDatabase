using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// Логика взаимодействия для TeacherAssignWindow.xaml
    /// </summary>
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
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AssignTeacherToDiscipline", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@TeacherID", _teacherId));
                    command.Parameters.Add(new SqlParameter("@TypeWork_Specialization_DisciplineID", (int)TypeWork_Specialization_Discipline.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@AwarageTime", int.Parse(AverageTime.Text)));
                    command.Parameters.Add(new SqlParameter("@GroupID", (int)GroupName.SelectedValue));

                    command.ExecuteNonQuery();
                }
            }

            this.Close();
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


    }
}
