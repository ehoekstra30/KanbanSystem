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
        private FogLampOrder currOrder;

        private List<FogLamp> failedTesting;
        private List<FogLamp> passedTesting;



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
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



        /*  !!! PROPERTY !!!
         *  DESCRIPTION : Returns a list of all created, yet untested, foglamps residing
         *      in the database.
         */
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



        /*  !!! PROPERTY !!!
         *  DESCRIPTION : Returns list of fog lamps that have failed testing.
         */
        public List<FogLamp> FailedTesting
        {
            get
            {
                return this.failedTesting;
            }
        }



        /*  !!! PROPERTY !!!
         *  DESCRIPTION : Returns list of fog lamps that have passed testing.
         */
        public List<FogLamp> PassedTesting
        {
            get
            {
                return this.passedTesting;
            }
        }



        /*  !!! PROPERTY !!!
         *  DESCRIPTION : Returns internal tracker on if order is finished or not.
         */
        public bool IsOrderFinished
        {
            get
            {
                return this.isOrderFinished;
            }
        }



        /*  DESCRIPTION : Start a new order to test foglamps for.
         *  PARAMETERS  :
         *      int orderAmount : number of foglamps to create for this order.
         *  ALTERS      :
         *      Adds an order to the db.
         *      Clears passedTesting & failedTesting.
         *  RETURNS     :
         *      FogLampOrder    : the order newly started.
         */
        public FogLampOrder StartNewOrder(int orderAmount)
        {
            // Create the nex order
            FogLampOrder order = new FogLampOrder();
            order.AmountOrdered = orderAmount;
            order.IsFulfilled = false;

            // Set internal tracker on if order is finished
            this.isOrderFinished = false;

            
            // Add the order to database
            this.kdb.FogLampOrders.Add(order);
            this.kdb.SaveChanges();

            // Set the internal tracker to the order we just created
            this.currOrder = order;

            // Clear these lists so that we can start fresh
            this.failedTesting.Clear();
            this.passedTesting.Clear();

            return this.currOrder;
        }



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
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



        /*  DESCRIPTION : Tests numToTest number of fog lamps. If they are effective,
         *      add foglamp to the order (through a connecting OrderLine) and to the
         *      internal passedTesting list. If defective, simply add to failedTesting
         *      list.
         *  PARAMETERS  :
         *      int numToTest   : number of foglamps to test.
         *  ALTERS      :
         *      Adds OrderLine to db for every effective foglamp.
         *      Changes FogLamp's IsEffective field.
         *      Adds to either passedTesting or failedTesting.
         *  RETURNS     : void
         */
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

                            OrderLine ol = new OrderLine();
                            ol.OrderId = this.currOrder.OrderId;
                            ol.TestTrayId = fl.TestTrayId;
                            ol.FogLampId = fl.FogLampId;
                            this.kdb.OrderLines.Add(ol);
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

                            OrderLine ol = new OrderLine();
                            ol.OrderId = this.currOrder.OrderId;
                            ol.TestTrayId = fl.TestTrayId;
                            ol.FogLampId = fl.FogLampId;
                            this.kdb.OrderLines.Add(ol);
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

                            OrderLine ol = new OrderLine();
                            ol.OrderId = this.currOrder.OrderId;
                            ol.TestTrayId = fl.TestTrayId;
                            ol.FogLampId = fl.FogLampId;
                            this.kdb.OrderLines.Add(ol);
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
                this.checkOrderStatus();
            }

            
        }



        /*  DESCRIPTION : Checks if the order is completed, based on how many OrderLines
         *      are connected to this order we're working on right now.
         *  PARAMETERS  : void
         *  ALTERS      :
         *      If OrderAmount achieved, sets IsFulfilled field to true.
         *  RETURNS     : void
         */
        private void checkOrderStatus()
        {
            // Query order lines
            //  i.e. count of foglamps in the entire order

            /*
            IQueryable<OrderLine> queryOrderLines =
                from ol in this.kdb.OrderLines
                join lamp in this.kdb.FogLamps on ol.FogLampId equals lamp.FogLampId
                join tray in this.kdb.TestTrays on ol.TestTrayId equals tray.TestTrayId
                join order in this.kdb.FogLampOrders on ol.OrderId equals order.OrderId
                where lamp.IsEffective == true
                where tray.IsCompleted == true
                where tray.IsCurrentlyInUse == false
                select ol; */

            if (this.currOrder.OrderLines.Count() == this.currOrder.AmountOrdered)
            {
                this.currOrder.IsFulfilled = true;
                this.kdb.SaveChanges();
                this.isOrderFinished = true;
            }

        }


    }
}
