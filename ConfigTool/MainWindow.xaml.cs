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
    public partial class MainWindow : Window
    {

        static private string dbName = "kanbandb";

        //CONFIG DEFAULTS

        //WORKER SETTINGS
        private string dNewSpeed = "60";
        private string dExpSpeed = "60";
        private string dSuperSpeed = "60";
        private string dNewDefect = "0.8";
        private string dExpDefect = "0.8";
        private string dSuperDefect = "0.8";
        private string dNewVariance = "0.5";
        private string dExpVariance = "0.1";
        private string dSuperVariance  = "0.15";

        //INDIVIDUAL PART CAPACITY SETTINGS
        private string dHarnessCapacity = "55";
        private string dHarnessMinimum = "5";
        private string dReflectorCapacity = "35";
        private string dReflectorMinimum = "5";
        private string dHousingCapacity = " 24";
        private string dHousingMinimum = "5";
        private string dLensCapacity = "40";
        private string dLensMinimum = "5";
        private string dBulbCapacity = "60";
        private string dBulbMinimum = "5";
        private string dBezelCapacity = "75";
        private string dBezelMinimum = "5";

        //GENERAL SETTINGS
        private static string dTestTrayCap = "60";
        private static string dAssemblyBaseTime = "60";
        private static string dRunnerFrequency = "300";
        private static string dTickTime = "1";
        private static string dNumberOfStations = "3";



        public MainWindow()
        {
            InitializeComponent();
            UpdateBoxes();

            string constr = ConfigurationManager.ConnectionStrings["kanban"].ConnectionString;

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection("data source= .;database=Index_demo;integrated security=SSPI");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to " + dbName + "database\n" + ex.ToString(), "Error");
                return;
            }

            connection.Close();


            
        }


        //FUNCTION      : UpdateBtn_Click
        //DESCRIPTION   : This button click function attempts to update the db with the given
        //                parameters stored in text boxes.
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlServerDal dal = new SqlServerDal(dbName);


            ConfigModel config = new ConfigModel();

            try
            {
                dal.UpdateConfiguration(config);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Nope");
                return;
            }


            MessageBox.Show("Database successfully updated","Yep");
            return;
        }



        //FUNCTION      : UpdateBoxes
        //DESCRIPTION   : Places default values in the text boxes for the user.
        public int UpdateBoxes()
        {

            newSpeed.Text = dNewSpeed;
            expSpeed.Text = dExpSpeed;
            superSpeed.Text = dSuperSpeed;

            newFailure.Text = dNewDefect;
            expFailure.Text = dExpDefect;
            superFailure.Text = dSuperDefect;

            newVariance.Text = dNewVariance;
            expVariance.Text = dExpVariance;
            superVariance.Text = dSuperVariance;

            harnessCapacity.Text = dHarnessCapacity;
            harnessMinimum.Text = dHarnessMinimum;
            reflectorCapacity.Text = dReflectorCapacity;
            reflectorMinimum.Text = dReflectorMinimum;
            housingCapacity.Text = dHousingCapacity;
            housingMinimum.Text = dHousingMinimum;
            lensCapacity.Text = dLensCapacity;
            lensMinimum.Text = dLensMinimum;
            bulbCapacity.Text = dBulbCapacity;
            bulbMinimum.Text = dBulbMinimum;
            bezelCapacity.Text = dBezelCapacity;
            bezelMinimum.Text = dBezelMinimum;

            tickTimeBox.Text = dTickTime;
            runnerFrequency.Text = dRunnerFrequency;
            assemblyTime.Text = dAssemblyBaseTime;
            numberOfStations.Text = dNumberOfStations;
            testTrayCapacity.Text = dTestTrayCap;


            return 0;
        }
    }
}