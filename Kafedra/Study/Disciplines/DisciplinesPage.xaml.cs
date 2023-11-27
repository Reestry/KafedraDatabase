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
using Kafedra.Study.Disciplines.AddingWindows;

namespace Kafedra.Study.Disciplines
{
    /// <summary>
    /// Логика взаимодействия для DisciplinesPage.xaml
    /// </summary>
    public partial class DisciplinesPage : Page
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataSet dataSet;

        SqlDataAdapter adapter1;
        DataSet dataSet1;

        public DisciplinesPage()
        {
            InitializeComponent();

            connection = new SqlConnection(SQLConnection.connectionString);
            adapter = new SqlDataAdapter();
            dataSet = new DataSet();

            adapter1 = new SqlDataAdapter();
            dataSet1 = new DataSet();

            Update();
        }

        private void Update()
        {
            LoadSpecialisationData();
            LoadDisciplineData();
        }
        private void LoadSpecialisationData()
        {
            command = new SqlCommand("SELECT * FROM Specialization", connection);
            adapter.SelectCommand = command;
            dataSet.Clear();
            adapter.Fill(dataSet);
            SpecializationDataGrid.ItemsSource = dataSet.Tables[0].DefaultView;
        }
        private void LoadDisciplineData()
        {
            command = new SqlCommand("SELECT * FROM Discipline", connection);
            adapter1.SelectCommand = command;
            dataSet1.Clear();
            adapter1.Fill(dataSet1);
            if (dataSet1.Tables.Count > 0)
            {
                DisciplineDataGrid.ItemsSource = dataSet1.Tables[0].DefaultView;
            }
            else
            {
                DisciplineDataGrid.ItemsSource = null;
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddSpecButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new SpecializationWindow();
            if (window.ShowDialog() == true)
            {
                command = new SqlCommand("EXEC CreateSpecialization @SpecializationName", connection);
                command.Parameters.AddWithValue("@SpecializationName", window.SpecializationName);
                adapter.InsertCommand = command;
                connection.Open();
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
                Update();
            }
        }

        private void EditSpecButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecialization = (DataRowView)SpecializationDataGrid.SelectedItem;
            var window = new SpecializationWindow(selectedSpecialization["SpecializationName"].ToString());
            if (window.ShowDialog() == true)
            {
                command = new SqlCommand("EXEC UpdateSpecialization @SpecializationID, @SpecializationName", connection);
                command.Parameters.AddWithValue("@SpecializationID", selectedSpecialization["SpecializationID"]);
                command.Parameters.AddWithValue("@SpecializationName", window.SpecializationName);
                adapter.UpdateCommand = command;
                connection.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
                connection.Close();
                Update();
            }
        }

        private void DeleteSpecButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecialization = (DataRowView)SpecializationDataGrid.SelectedItem;
            if (selectedSpecialization == null)
            {
                MessageBox.Show("Пожалуйста, выберите специализацию для удаления.");
                return;
            }
            command = new SqlCommand("EXEC DeleteSpecialization @SpecializationID", connection);
            command.Parameters.AddWithValue("@SpecializationID", selectedSpecialization["SpecializationID"]);
            adapter.DeleteCommand = command;
            connection.Open();
            adapter.DeleteCommand.ExecuteNonQuery();
            connection.Close();
            Update();
        }









        private void AddDisciplineButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new DisciplineWindow();
            if (window.ShowDialog() == true)
            {
                command = new SqlCommand("EXEC CreateDiscipline @DisciplineName", connection);
                command.Parameters.AddWithValue("@DisciplineName", window.DisciplineName);
                adapter.InsertCommand = command;
                connection.Open();
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
                Update();
            }
        }

        private void EditDisciplineButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDiscipline = (DataRowView)DisciplineDataGrid.SelectedItem;
            if (selectedDiscipline == null)
            {
                MessageBox.Show("Пожалуйста, выберите дисциплину для редактирования.");
                return;
            }
            var window = new DisciplineWindow(selectedDiscipline["DisciplineName"].ToString());
            if (window.ShowDialog() == true)
            {
                command = new SqlCommand("EXEC UpdateDiscipline @DisciplineID, @DisciplineName", connection);
                command.Parameters.AddWithValue("@DisciplineID", selectedDiscipline["DisciplineID"]);
                command.Parameters.AddWithValue("@DisciplineName", window.DisciplineName);
                adapter.UpdateCommand = command;
                connection.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
                connection.Close();
                Update();
            }
        }

        private void DeleteDisciplineButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDiscipline = (DataRowView)DisciplineDataGrid.SelectedItem;
            if (selectedDiscipline == null)
            {
                MessageBox.Show("Пожалуйста, выберите дисциплину для удаления.");
                return;
            }
            command = new SqlCommand("EXEC DeleteDiscipline @DisciplineID", connection);
            command.Parameters.AddWithValue("@DisciplineID", selectedDiscipline["DisciplineID"]);
            adapter.DeleteCommand = command;
            connection.Open();
            adapter.DeleteCommand.ExecuteNonQuery();
            connection.Close();
            Update();
        }
    }
}
