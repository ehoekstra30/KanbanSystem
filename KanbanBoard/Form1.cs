using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KanbanDal;

namespace KanbanBoard
{
    public partial class Form1 : Form
    {
        private bool run;
        private KanbanDbModel kdb;
        private Thread thread;

        private static int numberOfWorkStations;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kanban Production Board";
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Completed Lamps";
            dataGridView1.Columns[1].Name = "Passed";
            dataGridView1.Columns[2].Name = "Failed";
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Red;
            run = true;

            int i = 1;
            while (i >= numberOfWorkStations) {
                dataGridView1.Rows.Add(i);
                i++;
            }

        }

        //delegate for updating the datagrid
        public delegate void UpdateGridDelegate();

        private void Begin() {
            
            thread = new Thread(UpdateForm);
            thread.Start();

        }

        private void UpdateForm() {

            while (run == true) {




                Thread.Sleep(1000);
            }

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thread.Abort();
        }
    }
}
