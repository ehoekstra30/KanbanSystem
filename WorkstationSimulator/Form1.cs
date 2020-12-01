using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KanbanCore;

namespace WorkstationSimulator
{
    public partial class Form1 : Form
    {
        private KanbanDbModel kdb;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.kdb = new KanbanDbModel(); // provides modelled connection to db defined in App.config
            
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
