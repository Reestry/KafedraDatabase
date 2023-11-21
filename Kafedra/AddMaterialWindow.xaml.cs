using System;
using System.Data.SqlClient;
using System.Windows;

namespace Kafedra
{
    public partial class AddMaterialWindow : Window
    {
        string connectionString = "Data Source=WIN-TSLNADACF9B\\SQLEXPRESS;Initial Catalog=Kafedra;Integrated Security=True";

        public AddMaterialWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO EducationalMaterials (FKAdminID, MaterialType, MaterialName, MaterialAutor, PublicationYear) VALUES (@FKAdminID, @MaterialType, @MaterialName, @MaterialAutor, @PublicationYear)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FKAdminID", 1); // TODO: Replace with actual admin ID.
                    command.Parameters.AddWithValue("@MaterialType", txtMaterialType.Text);
                    command.Parameters.AddWithValue("@MaterialName", txtMaterialName.Text);
                    command.Parameters.AddWithValue("@MaterialAutor", txtMaterialAutor.Text);
                    command.Parameters.AddWithValue("@PublicationYear", dpPublicationYear.SelectedDate);
                    command.ExecuteNonQuery();
                }
            }
            this.Close();
        }
    }
}
