﻿using System;
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
    /// <summary>
    /// Логика взаимодействия для EditGroupWindow.xaml
    /// </summary>
    public partial class EditGroupWindow : Window
    {
        private int _groupId;

        public EditGroupWindow(int groupId)
        {
            InitializeComponent();
            _groupId = groupId;
            FillTeacherComboBox();
            FillSpecializationComboBox();
            LoadGroupData();
        }


        private void LoadGroupData()
        {
            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT FKTeacherID, FKSpecializationID, GroupName, StudentsCount FROM SupervisedGroup WHERE SupervisedGroupID = @GroupId", connection);
                command.Parameters.AddWithValue("@GroupId", _groupId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TeacherComboBox.SelectedValue = reader["FKTeacherID"];
                    SpecializationComboBox.SelectedValue = reader["FKSpecializationID"];
                    GroupNameTextBox.Text = reader["GroupName"].ToString();
                    StudentsCountTextBox.Text = reader["StudentsCount"].ToString();
                }
            }
        }

        private void FillTeacherComboBox()
        {
            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT PersonID, FirstName, LastName FROM Person INNER JOIN Teacher ON Person.PersonID = Teacher.FKPersonID", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                TeacherComboBox.ItemsSource = dt.DefaultView;
                TeacherComboBox.DisplayMemberPath = "FirstName";
                TeacherComboBox.SelectedValuePath = "PersonID";
            }
        }

        private void FillSpecializationComboBox()
        {
            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT SpecializationID, SpecializationName FROM Specialization", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                SpecializationComboBox.ItemsSource = dt.DefaultView;
                SpecializationComboBox.DisplayMemberPath = "SpecializationName";
                SpecializationComboBox.SelectedValuePath = "SpecializationID";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TeacherComboBox.Text) || string.IsNullOrEmpty(SpecializationComboBox.Text) || string.IsNullOrEmpty(GroupNameTextBox.Text) || string.IsNullOrEmpty(StudentsCountTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string connectionString = SQLConnection.connectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EditGroup", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@GroupId", _groupId);
                command.Parameters.AddWithValue("@TeacherID", TeacherComboBox.SelectedValue);
                command.Parameters.AddWithValue("@SpecializationID", SpecializationComboBox.SelectedValue);
                command.Parameters.AddWithValue("@GroupName", GroupNameTextBox.Text);
                command.Parameters.AddWithValue("@StudentsCount", int.Parse(StudentsCountTextBox.Text));

                command.ExecuteNonQuery();
            }
            this.Close();

        }



    }
}