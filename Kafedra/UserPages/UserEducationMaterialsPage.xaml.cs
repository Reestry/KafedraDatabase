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
    /// Логика взаимодействия для UserEducationMaterialsPage.xaml
    /// </summary>
    public partial class UserEducationMaterialsPage : Page
    {
        private SqlDataAdapter dataadapter;
        private SqlCommandBuilder commandBuilder;
        private DataSet ds;
        public UserEducationMaterialsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void DataGrid_LayoutUpdated(object sender, EventArgs e)
        {
            if (dataGrid.Columns.Count > 0)
            {
                dataGrid.Columns[0].Visibility = Visibility.Hidden;
                dataGrid.Columns[1].Visibility = Visibility.Hidden;
            }
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT * FROM EducationalMaterials";
                SqlConnection connection = new SqlConnection(SQLConnection.connectionString);
                dataadapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(dataadapter);
                ds = new DataSet();
                connection.Open();
                dataadapter.Fill(ds, "EducationalMaterials");
                connection.Close();
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


    }
}
