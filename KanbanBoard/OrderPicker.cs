using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanDal;

namespace KanbanBoard
{
    public class OrderPicker
    {
        private KanbanDbModel kdb;
        private double rookieDefectRate;
        private double experiencedDefectRate;
        private double seniorDefectRate;
        private Random random;

        private bool isOrderFinished;

        private List<FogLamp> failedTesting;
        private List<FogLamp> passedTesting;



        public OrderPicker(KanbanDbModel kdb)
        {
            this.kdb = kdb;

            // Get defect rates for this run
            this.rookieDefectRate = Convert.ToDouble(this.kdb.ConfigTables.Find("RookieDefectRate").SystemValue);
            this.experiencedDefectRate = Convert.ToDouble(this.kdb.ConfigTables.Find("ExperiencedDefectRate").SystemValue);
            this.seniorDefectRate = Convert.ToDouble(this.kdb.ConfigTables.Find("SeniorDefectRate").SystemValue);

            this.random = new Random();
            this.failedTesting = new List<FogLamp>();
            this.passedTesting = new List<FogLamp>();
        }


        public List<FogLamp> Assembled
        {
            get
            {
                IQueryable<FogLamp> queryAssembled =
                    from lamp in this.kdb.FogLamps
                    join tray in this.kdb.TestTrays on lamp.TestTrayId equals tray.TestTrayId
                    where lamp.IsEffective == null
                    where tray.IsCompleted == true
                    where tray.IsCurrentlyInUse == false
                    select lamp;

                return queryAssembled.ToList();
            }
        }

        public List<FogLamp> FailedTesting
        {
            get
            {
                return this.failedTesting;
            }
        }


        public List<FogLamp> PassedTesting
        {
            get
            {
                return this.passedTesting;
            }
        }


        public FogLampOrder StartNewOrder(int orderAmount)
        {
            FogLampOrder order = new FogLampOrder();

            order.AmountOrdered = orderAmount;

            return order;
        }


        public List<FogLamp> CheckForAssembled()
        {
            // Query foglamps that are...
            //  ...part of test trays that are completed but not in use
            //  ...have not been tested yet
            IQueryable<FogLamp> queryAssembled =
                from lamp in this.kdb.FogLamps
                join tray in this.kdb.TestTrays on lamp.TestTrayId equals tray.TestTrayId
                where lamp.IsEffective == null
                where tray.IsCompleted == true
                where tray.IsCurrentlyInUse == false
                select lamp;

            return queryAssembled.ToList();
        }


        public void Test(int numToTest)
        {
            IQueryable<FogLamp> queryAssembled =
                from lamp in this.kdb.FogLamps
                join tray in this.kdb.TestTrays on lamp.TestTrayId equals tray.TestTrayId
                where lamp.IsEffective == null
                where tray.IsCompleted == true
                where tray.IsCurrentlyInUse == false
                select lamp;

            for (int lamp_i=0; lamp_i < numToTest; ++lamp_i)
            {
                if (lamp_i >= queryAssembled.Count())
                {
                    // Break if there's no more lamps to test
                    break;
                }

                FogLamp fl = queryAssembled.First();
                switch (fl.ExperienceLevelOfAssembler)
                {
                    case 1:
                        if (this.random.NextDouble() > this.rookieDefectRate)
                        {
                            fl.IsEffective = true;
                            this.passedTesting.Add(fl);
                        }
                        else
                        {
                            fl.IsEffective = false;
                            this.failedTesting.Add(fl);
                        }
                        break;
                    case 2:
                        if (this.random.NextDouble() > this.experiencedDefectRate)
                        {
                            fl.IsEffective = true;
                            this.passedTesting.Add(fl);
                        }
                        else
                        {
                            fl.IsEffective = false;
                            this.failedTesting.Add(fl);
                        }
                        break;
                    case 3:
                        if (this.random.NextDouble() > this.seniorDefectRate)
                        {
                            fl.IsEffective = true;
                            this.passedTesting.Add(fl);
                        }
                        else
                        {
                            fl.IsEffective = false;
                            this.failedTesting.Add(fl);
                        }
                        break;
                }

                // Save changes, so that next time we get the query,
                // .First() will be different
                this.kdb.SaveChanges();

                // TODO: Check if order has been fulfilled

            }

            
        }

        private bool checkOrderStatus()
        {
            // Query order lines
            //  i.e. count of foglamps in the entire order
            IQueryable<OrderLine> queryOrderLines =
                from ol in this.kdb.OrderLines
                join lamp in this.kdb.FogLamps on ol.FogLampId equals lamp.FogLampId
                join tray in this.kdb.TestTrays on ol.TestTrayId equals tray.TestTrayId
                join order in this.kdb.FogLampOrders on ol.OrderId equals order.OrderId
                where lamp.IsEffective == true
                where tray.IsCompleted == true
                where tray.IsCurrentlyInUse == false
                select ol;

            return true;
        }


    }
}
