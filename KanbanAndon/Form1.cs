using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using KanbanDal;
using System.Reflection;

/*  FILE        : Form1.cs
 *  PROJECT     : AdvSql Project
 *  DEVELOPERS  : Ethan Hoekstra & Mohamed Benzreba
 *  PURPOSE     : Runs the UI logic relating to button presses, worker manipulation, and more.
 */


namespace KanbanAndon
{
    public partial class Form1 : Form
    {
        private static string seriesName = "Bin Contents";

        private WorkstationReader workstationReader;
        private Thread thread;
        private Boolean run;

        private KanbanDbModel kdb;

        //delegate for updating the datagrid
        public delegate void UpdateGridDelegate();

        //Simple contructor to handle for setup
        public Form1()
        {
            //BASIC FORM AND CHART SETUP
            InitializeComponent();
            this.Text = "Kanban Andon Display";
            run = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.kdb = new KanbanDbModel(); // provides modelled connection to db defined in App.config
            workstationReader = new WorkstationReader(kdb);

            //DEFINE GRID
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Bin Number";
            dataGridView1.Columns[1].Name = "Harness";
            dataGridView1.Columns[2].Name = "Reflector";
            dataGridView1.Columns[3].Name = "Housing";
            dataGridView1.Columns[4].Name = "Lens";
            dataGridView1.Columns[5].Name = "Bulb";
            dataGridView1.Columns[6].Name = "Bezel";

            thread = new Thread(RunSim);
            thread.Start();
        }


        //Run on a separate thread in order to simulate time passing
        public void RunSim() {

            run = true;

            while (run == true)
            {
                dataGridView1.BeginInvoke(new UpdateGridDelegate(UpdateGrid));
                Thread.Sleep(1000);
            }

        }


        //updates the chart with new bin values
        public void UpdateGrid() {

            dataGridView1.Rows.Clear();

            int i = 0;
            List<Workstation> l = workstationReader.GetWorkstations();
            foreach (Workstation workstation in l)
            {
                dataGridView1.Rows.Add((i + 1).ToString(), workstation.HarnessAmount, workstation.ReflectorAmount, 
                    workstation.HousingAmount, workstation.LensAmount, workstation.BulbAmount, workstation.BezelAmount) ;
            }
            dataGridView1.Show();
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thread.Abort();
        }
    }
}
