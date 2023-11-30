using System;
using System.Data.SqlClient;
using System.Windows;

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для EditMaterialWindow.xaml
    /// </summary>
    public partial class EditMaterialWindow : Window
    {
        private SqlDataAdapter dataadapter;

        public int ID;

        public EditMaterialWindow(int id)
        {
            InitializeComponent();

            ID = id;
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaterialType.Text) || string.IsNullOrWhiteSpace(txtMaterialName.Text) || string.IsNullOrWhiteSpace(txtMaterialAutor.Text) || dpPublicationYear.SelectedDate == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE EducationalMaterials SET MaterialType = @MaterialType, MaterialName = @MaterialName, MaterialAutor = @MaterialAutor, PublicationYear = @PublicationYear WHERE EducationalMaterialsID = @EducationalMaterialsID", connection);

                    command.Parameters.AddWithValue("@MaterialType", txtMaterialType.Text);
                    command.Parameters.AddWithValue("@MaterialName", txtMaterialName.Text);
                    command.Parameters.AddWithValue("@MaterialAutor", txtMaterialAutor.Text);
                    command.Parameters.AddWithValue("@PublicationYear", dpPublicationYear.SelectedDate);
                    command.Parameters.AddWithValue("@EducationalMaterialsID", ID);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Информация успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении информации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
