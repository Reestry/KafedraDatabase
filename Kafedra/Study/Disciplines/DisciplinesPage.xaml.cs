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
        private int currentSelection;

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataSet dataSet;

        SqlDataAdapter adapter1;
        DataSet dataSet1;


        SqlConnection sqlConnection;
        SqlDataAdapter sqlDataAdapter;
        SqlCommand sqlCommand;
        DataSet sqlDataSet;

        public DisciplinesPage()
        {
            InitializeComponent();

            connection = new SqlConnection(SQLConnection.connectionString);
            adapter = new SqlDataAdapter();
            dataSet = new DataSet();

            adapter1 = new SqlDataAdapter();
            dataSet1 = new DataSet();


            string connectionString = SQLConnection.connectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlDataAdapter = new SqlDataAdapter();
            sqlDataSet = new DataSet();


            Update();
            SpecializationComboBox.SelectionChanged += SpecializationComboBox_SelectionChanged;
        }

        private void Update()
        {
            LoadSpecialisationData();
            LoadDisciplineData();
            LoadComboBoxData();
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

        private void LoadComboBoxData()
        {
            LoadSpecializations();
            LoadDisciplines();
            LoadTypeWorks();
        }

        private void LoadSpecializations()
        {
            sqlCommand = new SqlCommand("SELECT * FROM Specialization", sqlConnection);
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet specializationDataSet = new DataSet();
            sqlDataAdapter.Fill(specializationDataSet);
            SpecializationComboBox.ItemsSource = specializationDataSet.Tables[0].DefaultView;
        }

        private void LoadDisciplines()
        {
            sqlCommand = new SqlCommand("SELECT * FROM Discipline", sqlConnection);
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet disciplineDataSet = new DataSet();
            sqlDataAdapter.Fill(disciplineDataSet);
            DisciplineComboBox.ItemsSource = disciplineDataSet.Tables[0].DefaultView;
        }

        private void LoadTypeWorks()
        {
            sqlCommand = new SqlCommand("SELECT * FROM TypeWork", sqlConnection);
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet typeWorkDataSet = new DataSet();
            sqlDataAdapter.Fill(typeWorkDataSet);
            TypeWorkComboBox.ItemsSource = typeWorkDataSet.Tables[0].DefaultView;
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecializationID = (int)SpecializationComboBox.SelectedValue;
            var selectedDisciplineID = (int)DisciplineComboBox.SelectedValue;
            var selectedTypeWorkID = (int)TypeWorkComboBox.SelectedValue;

            sqlCommand = new SqlCommand("EXEC AssignWorkToSpecializationDiscipline @FKSpecializationID, @FKDisciplineID, @FKTypeWorkID", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FKSpecializationID", selectedSpecializationID);
            sqlCommand.Parameters.AddWithValue("@FKDisciplineID", selectedDisciplineID);
            sqlCommand.Parameters.AddWithValue("@FKTypeWorkID", selectedTypeWorkID);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            Update();
            sqlCommand = new SqlCommand("EXEC GetDisciplinesAndTypeWorksForSpecialization @FKSpecializationID", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FKSpecializationID", currentSelection);
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataSet.Clear();
            sqlDataAdapter.Fill(sqlDataSet);
            Spec_disc_TypeGrid.ItemsSource = sqlDataSet.Tables[0].DefaultView;
        }


        private void SpecializationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecializationComboBox.SelectedValue != null)
            {
                var selectedSpecializationID = (int)SpecializationComboBox.SelectedValue;
                currentSelection = selectedSpecializationID;
                sqlCommand = new SqlCommand("EXEC GetDisciplinesAndTypeWorksForSpecialization @FKSpecializationID", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@FKSpecializationID", selectedSpecializationID);
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataSet.Clear();
                sqlDataAdapter.Fill(sqlDataSet);
                Spec_disc_TypeGrid.ItemsSource = sqlDataSet.Tables[0].DefaultView;
            }
        }

        private void DeleteAssignButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (DataRowView)Spec_disc_TypeGrid.SelectedItem;
            if (selectedRow == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.");
                return;
            }
            var selectedSpecializationID = (int)selectedRow["FKSpecializationID"];
            var selectedDisciplineID = (int)selectedRow["FKDisciplineID"];
            var selectedTypeWorkID = (int)selectedRow["FKTypeWorkID"];

            sqlCommand = new SqlCommand("EXEC UnassignWorkFromSpecializationDiscipline @FKSpecializationID, @FKDisciplineID, @FKTypeWorkID", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FKSpecializationID", selectedSpecializationID);
            sqlCommand.Parameters.AddWithValue("@FKDisciplineID", selectedDisciplineID);
            sqlCommand.Parameters.AddWithValue("@FKTypeWorkID", selectedTypeWorkID);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            Update();
            sqlCommand = new SqlCommand("EXEC GetDisciplinesAndTypeWorksForSpecialization @FKSpecializationID", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FKSpecializationID", currentSelection);
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataSet.Clear();
            sqlDataAdapter.Fill(sqlDataSet);
            Spec_disc_TypeGrid.ItemsSource = sqlDataSet.Tables[0].DefaultView;
        }
    }
}
