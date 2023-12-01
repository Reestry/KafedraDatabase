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
using Kafedra.EventsAddingWindows;

namespace Kafedra
{
    /// <summary>
    /// Логика взаимодействия для EducationMaterialsPage.xaml
    /// </summary>

    public partial class EducationMaterialsPage : Page
    {
        private SqlDataAdapter dataadapter;
        private SqlCommandBuilder commandBuilder;
        private DataSet ds;

        private int MaterialID;

        public EducationMaterialsPage()
        {
            InitializeComponent();
            LoadData();

            dataGrid.LayoutUpdated += DataGrid_LayoutUpdated;
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow();
            addMaterialWindow.ShowDialog();
            LoadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = dataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                if (MaterialID == null)
                    return;

                EditMaterialWindow editMaterialWindow = new EditMaterialWindow(MaterialID);

                editMaterialWindow.txtMaterialType.Text = rowview["MaterialType"].ToString();
                editMaterialWindow.txtMaterialName.Text = rowview["MaterialName"].ToString();
                editMaterialWindow.txtMaterialAutor.Text = rowview["MaterialAutor"].ToString();
                editMaterialWindow.dpPublicationYear.SelectedDate = Convert.ToDateTime(rowview["PublicationYear"]);

                editMaterialWindow.Closed += Update;

                if (editMaterialWindow.ShowDialog() == true)
                {
                    rowview["MaterialType"] = editMaterialWindow.txtMaterialType.Text;
                    rowview["MaterialName"] = editMaterialWindow.txtMaterialName.Text;
                    rowview["MaterialAutor"] = editMaterialWindow.txtMaterialAutor.Text;
                    rowview["PublicationYear"] = editMaterialWindow.dpPublicationYear.SelectedDate;

                    dataadapter.Update(ds, "EducationalMaterials");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для редактирования.");
            }

        }

        private void Update(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            DataRowView rowview = dataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                if (MaterialID == null)
                    return;

                rowview.Row.Delete();

                dataadapter.Update(ds, "EducationalMaterials");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.");
            }
        }


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowview = dataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {

                MaterialID = Convert.ToInt32(rowview["EducationalMaterialsID"]);
            }
        }
    }
}
