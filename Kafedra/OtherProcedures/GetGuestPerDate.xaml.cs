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

namespace Kafedra.OtherProcedures
{
    /// <summary>
    /// Логика взаимодействия для GetGuestPerDate.xaml
    /// </summary>
    public partial class GetGuestPerDate : Window
    {
        public GetGuestPerDate()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string sql = "GetGuestsAfterDate";

            using (SqlConnection connection = new SqlConnection(SQLConnection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter dateParam = new SqlParameter
                {
                    ParameterName = "@date",
                    Value = datePicker.SelectedDate
                };
                command.Parameters.Add(dateParam);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable("Guests");
                dataAdapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
