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
using Kafedra.Study.Group.AddingWindows;
using Kafedra.Study.Other.AddingWindows;
using Kafedra.OtherProcedures;

namespace Kafedra.Study.Other
{
    /// <summary>
    /// Логика взаимодействия для OtherPage.xaml
    /// </summary>
    public partial class OtherPage : Page
    {
        public OtherPage()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            string connectionString = SQLConnection.connectionString;
            string sql = "EXEC GetAdmins";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                AdminDataGrid.ItemsSource = dt.DefaultView;
            }
        }


        private void AddAdminButton_Click(object sender, RoutedEventArgs e)
        {
            AddAdminWindow addAdminWindow = new AddAdminWindow();
            addAdminWindow.ShowDialog();

            LoadDataGrid();
        }


        private void EditAdminButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)AdminDataGrid.SelectedItem;
            if (row != null)
            {
                EditAdminWindow editAdminWindow = new EditAdminWindow(row);
                editAdminWindow.ShowDialog();

                LoadDataGrid();
            }
        }
        private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)AdminDataGrid.SelectedItem;
            if (row != null)
            {
                string connectionString = SQLConnection.connectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("DeleteAdmin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AdminID", row["AdminID"]);

                    command.ExecuteNonQuery();
                }

                LoadDataGrid();
            }
        }

        private void _groupDataGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GetMaterialsAfterYear_Click(object sender, RoutedEventArgs e)
        {
            GetMaterialsAfterYear GetMaterialsAfterYear = new GetMaterialsAfterYear();
            GetMaterialsAfterYear.ShowDialog();
        }

        private void GetGuestPerDate(object sender, RoutedEventArgs e)
        {
            GetGuestPerDate GetGuestPerDate = new GetGuestPerDate();
            GetGuestPerDate.ShowDialog();
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT SUM(StudentsCount) as TotalStudents FROM SupervisedGroup";

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    MessageBox.Show("Всего студентов: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
        }
    }
}
