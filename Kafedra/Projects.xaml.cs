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

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Page
    {
        private SqlDataAdapter _dataAdapter;
        private SqlCommandBuilder _commandBuilder;
        private DataTable _dataTable;

        public Projects()
        {
            InitializeComponent();
            LoadData();
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

            ProjectsDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            ProjectsDataGrid.Columns[1].Visibility = Visibility.Collapsed;
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _dataAdapter.Update(_dataTable);
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
