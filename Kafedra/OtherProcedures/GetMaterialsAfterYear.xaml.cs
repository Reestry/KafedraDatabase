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
    /// Логика взаимодействия для GetMaterialsAfterYear.xaml
    /// </summary>
    public partial class GetMaterialsAfterYear : Window
    {
        public GetMaterialsAfterYear()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int year;
            if (int.TryParse(YearTextBox.Text, out year))
            {
                using (SqlConnection conn = new SqlConnection(SQLConnection.connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetMaterialsAfterYear", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int)).Value = year;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите корректное число в поле года");
            }
        }
    }
}
