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

namespace WorkstationSimulator
{

    public partial class Form1 : Form
    {
        private static string seriesName = "Bin Contents";

        private Thread thread;
        private Boolean run;

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
            worker = new Worker(this.kdb, exp, 100);
            
            RunSim();

            stopBtn.Enabled = true;
            startBtn.Enabled = false;
        }


        //Run on a separate thread in order to simulate time passing
        public void RunSim() {

            worker.SimulateWork();
                
            UpdateChart();

        }

        //updates the chart with new bin values
        public void UpdateChart() {

            chart.Series[seriesName].Points.Clear();

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
            stopBtn.Enabled = false;
            resumeBtn.Enabled = true;
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            RunSim();
            UpdateChart();
            //resumeBtn.Enabled = false;
            stopBtn.Enabled = true;
        }
    }
}


/*********************************************************************
 * EXAMPLE FROM CLASS:
 * DELETE this if you don't care about it Ethan! It's just the example we ran through
 * in class showing us how to use models and stuff for insertion, updating, deleting 
 * records, etc. etc.
 * entityEntities ee = new entityEntities();
            Department dpt = new Department();
            int opt = 0;

            do
            {
                Console.WriteLine("1: New Record \n 2: Display \n 3: Update \n 4: Delete");
                Console.WriteLine("Enter your option: ");

                opt = Convert.ToInt32(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        Console.WriteLine("Enter ID, Name, Location, and PostalCode");
                        dpt.ID = Convert.ToInt32(Console.ReadLine());
                        dpt.Name = Console.ReadLine();
                        dpt.Location = Console.ReadLine();
                        dpt.PostalCode = Console.ReadLine();

                        ee.Departments.Add(dpt);
                        ee.SaveChanges();
                        break;
                    case 2:
                        foreach (Department d in ee.Departments)
                        {
                            Console.WriteLine("{0} \t {1} \t {2}",
                                d.ID, d.Name, d.Location);
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter ID to update:");
                        Department de = ee.Departments.Find(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("Enter new Name, Location, Postal Code");

                        de.Name = Console.ReadLine();
                        de.Location = Console.ReadLine();
                        de.PostalCode = Console.ReadLine();
                        ee.SaveChanges();
                        break;

                    case 4:
                        Console.WriteLine("Enter ID to delete:");
                        Department dep = ee.Departments.Find(Convert.ToInt32(Console.ReadLine()));
                        ee.Departments.Remove(dep);
                        ee.SaveChanges();
                        break;

                    default:
                        Console.WriteLine("BAD CHOICE");
                        break;
                } 
            } while (opt != 5);
****************************************************************/
