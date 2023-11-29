﻿using System;
using System.Collections.Generic;
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

namespace Kafedra.ProjectsWindows
{
    /// <summary>
    /// Логика взаимодействия для ProjectsEditWindow.xaml
    /// </summary>
    public partial class ProjectsEditWindow : Window
    {
        private int projectID; 

        public ProjectsEditWindow(int projectId)
        {
            InitializeComponent();

            projectID = projectId;

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TypeOfProject, Status FROM Projects WHERE ProjectsID = @ProjectsID", connection))
                {
                    command.Parameters.AddWithValue("@ProjectsID", projectID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EventTypeTextBox.Text = reader["TypeOfProject"].ToString();
                            StatusComboBox.SelectedItem = reader["Status"].ToString();
                        }
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string eventType = EventTypeTextBox.Text;
            string status = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Projects SET TypeOfProject = @TypeOfProject, Status = @Status WHERE ProjectsID = @ProjectsID", connection))
                {
                    command.Parameters.AddWithValue("@TypeOfProject", eventType);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ProjectsID", projectID);
                    command.ExecuteNonQuery();
                }
            }

            this.Close();
        }
    }
}
