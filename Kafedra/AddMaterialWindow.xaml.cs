using System;
using System.Data.SqlClient;
using System.Windows;

namespace Kafedra
{
    public partial class AddMaterialWindow : Window
    {
        public AddMaterialWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaterialType.Text) || string.IsNullOrWhiteSpace(txtMaterialName.Text) || string.IsNullOrWhiteSpace(txtMaterialAutor.Text) || dpPublicationYear.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO EducationalMaterials (FKAdminID, MaterialType, MaterialName, MaterialAutor, PublicationYear) VALUES (@FKAdminID, @MaterialType, @MaterialName, @MaterialAutor, @PublicationYear)";

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FKAdminID", 1);
                        command.Parameters.AddWithValue("@MaterialType", txtMaterialType.Text);
                        command.Parameters.AddWithValue("@MaterialName", txtMaterialName.Text);
                        command.Parameters.AddWithValue("@MaterialAutor", txtMaterialAutor.Text);
                        command.Parameters.AddWithValue("@PublicationYear", dpPublicationYear.SelectedDate);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Материал успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении материала: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
