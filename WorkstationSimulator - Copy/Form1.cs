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


namespace WorkstationSimulator
{
    public partial class Form1 : Form
    {
        private static string seriesName = "Bin Contents";

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
        }



        //Creates a worker object and assigns appropriate values to begin the simulation
        private void startBtn_Click(object sender, EventArgs e)
        {

            thread = new Thread(RunSim);
            thread.Start();
        }

        //Run on a separate thread in order to simulate time passing
        public void RunSim() {

            run = true;

            while (run == true)
            {

                //chart.BeginInvoke(new UpdateChartDelegate(UpdateChart));

                Thread.Sleep(1000);
            }

        }





        //updates the chart with new bin values
        public void UpdateGrid() {

            
        }
    }
}
