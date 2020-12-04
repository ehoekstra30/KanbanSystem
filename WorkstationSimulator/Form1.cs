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
        private int runnerCounter;
        private static int runnerWaitTime = 5;

        private Worker worker;
        private KanbanDbModel kdb;

        //Simple contructor to handle for setup
        public Form1()
        {
            //BASIC FORM AND CHART SETUP
            InitializeComponent();
            this.Text = "Kanban Worker Simulator";
            chart.Series.Clear();
            chart.Series.Add(seriesName);
            chart.Series[seriesName].SetDefault(true);
            chart.Series[seriesName].Enabled = true;
            runnerCounter = 0;
            worker = null;
            run = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.kdb = new KanbanDbModel(); // provides modelled connection to db defined in App.config
        }



        //Creates a worker object and assigns appropriate values to begin the simulation
        private void startBtn_Click(object sender, EventArgs e)
        {
            ExperienceLevel_t exp = ExperienceLevel_t.Experienced;

            if (workerExperienceBox.SelectedIndex == 0)
            {
                exp = ExperienceLevel_t.Rookie;
            }
            else if (workerExperienceBox.SelectedIndex == 1)
            {
                exp = ExperienceLevel_t.Experienced;
            }
            else
            {
                exp = ExperienceLevel_t.Senior;
            }
            worker = new Worker(this.kdb, exp, 2);

            thread = new Thread(RunSim);
            thread.Start();

            stopBtn.Enabled = true;
            startBtn.Enabled = false;
        }


        //Run on a separate thread in order to simulate time passing
        public void RunSim() {

            run = true;

            while (run == true)
            {
                //check if it is time for a runner to be sent
                if (runnerCounter == 5)
                {
                    worker.SendRunner();
                    runnerCounter = 0;
                }
                worker.SimulateWork();
                UpdateChart();

                runnerCounter++;
                if (run == false) { 
                    break;
                }
                Thread.Sleep(1000);
            }

        }





        //updates the chart with new bin values
        public void UpdateChart() {

            chart.Series.Clear();
            chart.Series.Add(seriesName);

            chart.Series[seriesName].Points.AddXY("Harnesses",worker.HarnessBin);
            chart.Series[seriesName].Points.AddXY("Reflectors",worker.ReflectorBin);
            chart.Series[seriesName].Points.AddXY("Housings",worker.HousingBin);
            chart.Series[seriesName].Points.AddXY("Lenses",worker.LensBin);
            chart.Series[seriesName].Points.AddXY("Bulbs",worker.BulbBin);
            chart.Series[seriesName].Points.AddXY("Bezels", worker.BezelBin);

            chart.Show();
            chart.ResetAutoValues();
        }

        private void workerExperienceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            startBtn.Enabled = true;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            run = false;
            stopBtn.Enabled = false;
            resumeBtn.Enabled = true;
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            thread = new Thread(RunSim);
            thread.Start();
            resumeBtn.Enabled = false;
            stopBtn.Enabled = true;
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (worker != null)
            {
                worker.EndWork();
            }
        }
    }
}
