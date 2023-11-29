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
    /// Логика взаимодействия для UserProjectsPage.xaml
    /// </summary>
    public partial class UserProjectsPage : Page
    {

        private SqlDataAdapter _dataAdapter;
        private SqlCommandBuilder _commandBuilder;
        private DataTable _dataTable;

        public UserProjectsPage()
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
    }
}
