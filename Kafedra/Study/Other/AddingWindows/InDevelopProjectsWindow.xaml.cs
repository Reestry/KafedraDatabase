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
using System.Windows.Shapes;

namespace Kafedra.Study.Other.AddingWindows
{
    /// <summary>
    /// Логика взаимодействия для InDevelopProjectsWindow.xaml
    /// </summary>
    public partial class InDevelopProjectsWindow : Window
    {
        public InDevelopProjectsWindow()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Projects WHERE Status = 'В разработке'";

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Projects");
                dataAdapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.DefaultView;
            }

            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[1].Visibility = Visibility.Hidden;
        }

    }
}
