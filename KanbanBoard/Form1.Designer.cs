
namespace KanbanBoard
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.produced = new System.Windows.Forms.Label();
            this.yield = new System.Windows.Forms.Label();
            this.inProcess = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(399, 575);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 624);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Order Amount: 100";
            // 
            // produced
            // 
            this.produced.AutoSize = true;
            this.produced.Location = new System.Drawing.Point(221, 624);
            this.produced.Name = "produced";
            this.produced.Size = new System.Drawing.Size(65, 13);
            this.produced.TabIndex = 2;
            this.produced.Text = "Produced: 0";
            // 
            // yield
            // 
            this.yield.AutoSize = true;
            this.yield.Location = new System.Drawing.Point(316, 624);
            this.yield.Name = "yield";
            this.yield.Size = new System.Drawing.Size(42, 13);
            this.yield.TabIndex = 3;
            this.yield.Text = "Yield: 0";
            // 
            // inProcess
            // 
            this.inProcess.AutoSize = true;
            this.inProcess.Location = new System.Drawing.Point(127, 624);
            this.inProcess.Name = "inProcess";
            this.inProcess.Size = new System.Drawing.Size(69, 13);
            this.inProcess.TabIndex = 4;
            this.inProcess.Text = "In Process: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 649);
            this.Controls.Add(this.inProcess);
            this.Controls.Add(this.yield);
            this.Controls.Add(this.produced);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label produced;
        private System.Windows.Forms.Label yield;
        private System.Windows.Forms.Label inProcess;
    }
}

