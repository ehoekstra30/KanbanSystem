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
        private OrderPicker op;

        private static int numberOfWorkStations;

        private int processedAmount;
        private int producedAmount;
        private int yieldAmount;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kanban Production Board";
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Backlog";
            dataGridView1.Columns[1].Name = "Passed";
            dataGridView1.Columns[2].Name = "Failed";
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.Green;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Red;
            run = true;

            processedAmount = 0;
            producedAmount = 0;
            yieldAmount = 0;

            kdb = new KanbanDbModel();
            op = new OrderPicker(kdb);

            op.StartNewOrder(100);

            int i = 1;
            while (i <= numberOfWorkStations)
            {
                dataGridView1.Rows.Add(i);
                i++;
            }
            Begin();
        }

        //delegate for updating the datagrid
        public delegate void UpdateGridDelegate();

        
        private void Begin() {
            
            thread = new Thread(UpdateForm);
            thread.Start();

        }



        private void UpdateForm() {

            while (run == true) {

                dataGridView1.BeginInvoke(new UpdateGridDelegate(UpdateGrid));
                Thread.Sleep(1000);
            }

        }



        public void UpdateGrid() {

            dataGridView1.Rows.Clear();

            if (op.IsOrderFinished)
            {
                op.StartNewOrder(100);
            }
            op.Test(1);

            int numberOfRows = op.Assembled.Count;
            if (numberOfRows < op.FailedTesting.Count) {
                numberOfRows = op.FailedTesting.Count;
            }
            if (numberOfRows < op.PassedTesting.Count) {
                numberOfRows = op.PassedTesting.Count;
            }

            yieldAmount = 0;
            processedAmount = 0;
            int i = 0;

            while (i < numberOfRows) {
                dataGridView1.Rows.Add();
                i++;
            }

            i = 0;
            foreach (FogLamp f in op.Assembled)
            {
                dataGridView1.Rows[i].Cells[0].Value = f.ToString();
                i++;
                producedAmount++;
            }
            i = 0;
            foreach (FogLamp f in op.PassedTesting) {
                dataGridView1.Rows[i].Cells[1].Value = f.ToString();
                i++;
                yieldAmount++;
            }
            i = 0;
            foreach (FogLamp f in op.FailedTesting)
            {
                dataGridView1.Rows[i].Cells[2].Value = f.ToString();
                i++;
            }

            inProcess.Text = "In Process: " + "3";
            produced.Text = "Produced: " + producedAmount;
            yield.Text = "Yield: " + yieldAmount;
        }



        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thread.Abort();
        }
    }
}
