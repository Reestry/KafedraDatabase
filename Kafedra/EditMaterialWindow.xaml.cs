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
        string connectionString = "Data Source=WIN-TSLNADACF9B\\SQLEXPRESS;Initial Catalog=Kafedra;Integrated Security=True";
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
