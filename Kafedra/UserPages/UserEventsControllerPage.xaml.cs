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

namespace Kafedra.UserPages
{
    /// <summary>
    /// Логика взаимодействия для UserEventsControllerPage.xaml
    /// </summary>
    public partial class UserEventsControllerPage : Page
    {
        public UserEventsControllerPage()
        {
            InitializeComponent();

            LoadGuestDataGrid();
            LoadDataGrid();

            dataGrid.LayoutUpdated += DataGrid_LayoutUpdated;
            dataGrid1.LayoutUpdated += DataGrid_LayoutUpdatedGuests;
        }

        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            if (dataGrid.Columns.Count > 0)
                dataGrid.Columns[0].Visibility = Visibility.Hidden;
        }
        private void DataGrid_LayoutUpdatedGuests(object sender, EventArgs e)
        {
            if (dataGrid1.Columns.Count > 0)
                dataGrid1.Columns[0].Visibility = Visibility.Hidden;
        }
        private void LoadDataGrid()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM GetParticipantEvents()", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
            }
        }
        private void LoadGuestDataGrid()
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from GetGuestsEvents()", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
        }
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
