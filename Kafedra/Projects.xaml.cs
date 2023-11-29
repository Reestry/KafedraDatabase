using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System;
using Kafedra.EventsAddingWindows;
using Kafedra.ProjectsWindows;
using System.Collections.Generic;

namespace Kafedra
{
    public partial class Projects : Page
    {
        private SqlDataAdapter _dataAdapter;
        private SqlCommandBuilder _commandBuilder;
        private DataTable _dataTable;

        public Projects()
        {
            InitializeComponent();
            LoadData();

            ProjectsDataGrid.LayoutUpdated += DataGrid_LayoutUpdated;
        }

        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            if (ProjectsDataGrid.Columns.Count > 0)
            {
                ProjectsDataGrid.Columns[0].Visibility = Visibility.Hidden;
                ProjectsDataGrid.Columns[1].Visibility = Visibility.Hidden;

            }
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM Projects";
            SqlConnection connection = new SqlConnection(SQLConnection.connectionString);
            _dataAdapter = new SqlDataAdapter(sql, connection);
            _commandBuilder = new SqlCommandBuilder(_dataAdapter);
            _dataTable = new DataTable();
            _dataAdapter.Fill(_dataTable);
            ProjectsDataGrid.ItemsSource = _dataTable.DefaultView;

        }
        private void Update(object sender, EventArgs e)
        {
            LoadData();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectAddWindow ProjectAddWindow = new ProjectAddWindow();
            ProjectAddWindow.ShowDialog();
            LoadData();

            ProjectAddWindow.Closed += Update;

        }

        private void FinishDevelopment_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ProjectsDataGrid.SelectedItem;
                int projectId = (int)row["ProjectsID"];
                UpdateProjectStatus(projectId, "Завершен");

                LoadData();
            }
        }

        private void UpdateProjectStatus(int projectId, string status)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Projects SET Status = @Status WHERE ProjectsID = @ProjectID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ProjectID", projectId);
                    command.ExecuteNonQuery();
                }
                _dataAdapter.Update(_dataTable);
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ProjectsDataGrid.SelectedItem;
                int projectId = (int)row["ProjectsID"];

                ProjectsEditWindow projectEditWindow = new ProjectsEditWindow(projectId);
                projectEditWindow.ShowDialog();

                projectEditWindow.Closed += Update;
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)ProjectsDataGrid.SelectedItem;
                row.Row.Delete();
                _dataAdapter.Update(_dataTable);
            }
        }
    }
}
