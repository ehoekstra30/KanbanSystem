using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace ConfigTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private string dbName = "kanbandb";

        public MainWindow()
        {
            InitializeComponent();

            string constr = ConfigurationManager.ConnectionStrings["kanban"].ConnectionString;

            try {
                SqlConnection connection = new SqlConnection("data source= .;database=Index_demo;integrated security=SSPI");
            }
            catch (Exception ex) {
                MessageBox.Show("Cannot connect to " + dbName + "database\n" + ex.ToString(), "Error");
            }
        }
    }
}