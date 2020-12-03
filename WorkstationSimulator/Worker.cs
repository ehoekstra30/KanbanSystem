using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanbanDal;


/*  FILE        : Worker.cs
 *  PROJECT     : AdvSql Project
 *  DEVELOPERS  : Ethan Hoekstra & Mohamed Benzreba
 *  PURPOSE     : Models a worker at a Workstation, putting together foglamps. Also
 *      defines the ExperienceLevel_t type for enumerating experience level of
 *      Workers.
 */


namespace WorkstationSimulator
{
    // For tracking experience level of Worker
    public enum ExperienceLevel_t
    {
        Rookie,
        Experienced,
        Senior
    }


    public class Worker : IDisposable
    {

        private ExperienceLevel_t experienceLevel;
        private int minutesPerSecond;
        private double fogLampsToMake;
        private int fogLampsMade;
        private int fogLampsOnTestTray;
        private int currentTestTrayId;
        private KanbanDbModel kdb;
        private Workstation wstation;

        private Random rand;
        private double assemblyTimeVariance;

        private static int harnessBinMax;
        private static int reflectorBinMax;
        private static int housingBinMax;
        private static int lensBinMax;
        private static int bulbBinMax;
        private static int bezelBinMax;

        private static int harnessBinMin;
        private static int reflectorBinMin;
        private static int housingBinMin;
        private static int lensBinMin;
        private static int bulbBinMin;
        private static int bezelBinMin;

        



        /* -------------------------------------------------------------------------------- */
        /* ----------------------------- CONSTRUCTORS & SET-UP ---------------------------- */
        /* -------------------------------------------------------------------------------- */



        // !!! CONSTRUCTOR !!!
        public Worker(KanbanDbModel kdb, ExperienceLevel_t experienceLevel, int minutesPerSecond)
        {
            this.experienceLevel = experienceLevel;
            this.minutesPerSecond = minutesPerSecond;
            this.fogLampsToMake = 0;
            this.fogLampsMade = 0;
            this.kdb = kdb;
            this.rand = new Random();

            // Set up the workstation
            // If it cannot be setup, set our boolean to false so that external program knows
            // that no Worker is present
            if (this.setupWorkstation())
            {
                this.isAtWorkstation = true;
            }
            else
            {
                this.isAtWorkstation = false;
            }
        }



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        private bool setupWorkstation()
        {
            // Set up static maxes for each bin (retrieved from Config)
            Worker.harnessBinMax = Int32.Parse(this.kdb.ConfigTables.Find("HarnessCapacity").SystemValue);
            Worker.reflectorBinMax = Int32.Parse(this.kdb.ConfigTables.Find("ReflectorCapacity").SystemValue);
            Worker.housingBinMax = Int32.Parse(this.kdb.ConfigTables.Find("HousingCapacity").SystemValue);
            Worker.lensBinMax = Int32.Parse(this.kdb.ConfigTables.Find("LensCapacity").SystemValue);
            Worker.bulbBinMax = Int32.Parse(this.kdb.ConfigTables.Find("BulbCapacity").SystemValue);
            Worker.bezelBinMax = Int32.Parse(this.kdb.ConfigTables.Find("BezelCapacity").SystemValue);

            // Set up static minimums for each bin (runner will refill bin if at this amount or lower)
            Worker.harnessBinMin = Int32.Parse(this.kdb.ConfigTables.Find("HarnessMinimum").SystemValue);
            Worker.reflectorBinMin = Int32.Parse(this.kdb.ConfigTables.Find("ReflectorMinimum").SystemValue);
            Worker.housingBinMin = Int32.Parse(this.kdb.ConfigTables.Find("HousingMinimum").SystemValue);
            Worker.lensBinMin = Int32.Parse(this.kdb.ConfigTables.Find("LensMinimum").SystemValue);
            Worker.bulbBinMin = Int32.Parse(this.kdb.ConfigTables.Find("BulbMinimum").SystemValue);
            Worker.bezelBinMin = Int32.Parse(this.kdb.ConfigTables.Find("BezelMinimum").SystemValue);


            // Query to find an available workstation
            IQueryable<Workstation> queryWorkstations =
                from station in this.kdb.Workstations
                where station.IsCurrentlyWorking == false
                select station;
            

            // TODO: check that queryWorkstations actually returns smthn
            // If not, the worker cannot work!
            if (queryWorkstations.Any())
            {
                this.wstation = queryWorkstations.First();
                wstation.IsCurrentlyWorking = true;

                // Set each bin to whatever they were left at the workstation last time
                // If this is the first time this workstation is being used, it will be set
                //  to the capacities of each bin.
                this.harnessBin = (int)wstation.HarnessAmount;
                this.reflectorBin = (int)wstation.ReflectorAmount;
                this.housingBin = (int)wstation.HousingAmount;
                this.lensBin = (int)wstation.LensAmount;
                this.bulbBin = (int)wstation.BulbAmount;
                this.bezelBin = (int)wstation.BezelAmount;


                // Set assembly time variance based on experience level
                switch (this.experienceLevel)
                {
                    case ExperienceLevel_t.Rookie:
                        this.assemblyTimeVariance = Double.Parse(
                            this.kdb.ConfigTables.Find("RookieAssemblyTimeVariance").SystemValue);
                        break;
                    case ExperienceLevel_t.Experienced:
                        this.assemblyTimeVariance = Double.Parse(
                            this.kdb.ConfigTables.Find("ExperiencedAssemblyTimeVariance").SystemValue);
                        break;
                    case ExperienceLevel_t.Senior:
                        this.assemblyTimeVariance = Double.Parse(
                            this.kdb.ConfigTables.Find("SeniorAssemblyTimeVariance").SystemValue);
                        break;
                    default:
                        this.assemblyTimeVariance = 0.0;
                        break;
                }

                // Now, grab a new test tray for this workstation
                // TODO: Check if any incomplete test trays have been left... grab that one instead
                IQueryable<TestTray> queryTestTrays =
                    from testt in this.kdb.TestTrays
                    where testt.IsCompleted == false 
                    where testt.IsCurrentlyInUse == false
                    select testt;

                if (queryTestTrays.Any())
                {
                    // There is a test tray from before with some parts on it...
                    // Let's use it! How many parts are on it tho?
                    var query =
                        from thingy in this.kdb.TestTrays
                        join otherthingy in this.kdb.FogLamps on thingy equals otherthingy.TestTray
                        select new { 
                            testTrayId = thingy.TestTrayId, 
                            fogLampsSoFar = thingy.FogLamps.Count };
                    var firstAvailableTestTray = query.FirstOrDefault();

                    this.currentTestTrayId = firstAvailableTestTray.testTrayId;
                    this.fogLampsOnTestTray = firstAvailableTestTray.fogLampsSoFar;

                    // Now find this particular test tray and make sure that it is in use
                    TestTray currtt = this.kdb.TestTrays.Find(this.currentTestTrayId);
                    currtt.IsCurrentlyInUse = true;
                }
                else
                {
                    this.kdb.TestTrays.Add(new TestTray());
                    this.currentTestTrayId = this.kdb.TestTrays.Count() + 1;
                    this.fogLampsOnTestTray = 0;
                }

                
                this.kdb.SaveChanges();

                return true;
            }

            else
            {
                // all workstations are taken up...
                // Don't let this Worker work!! Get outta here!!
                return false;
            }

        }



        /* -------------------------------------------------------------------------------- */
        /* ----------------------------- ACCESSORS & MUTATORS ----------------------------- */
        /* -------------------------------------------------------------------------------- */


        public bool IsAtWorkstation { get { return this.isAtWorkstation; } }
        private bool isAtWorkstation;

        public int HarnessBin { get { return this.harnessBin; } }
        private int harnessBin;

        public int ReflectorBin { get { return this.reflectorBin; } }
        private int reflectorBin;

        public int HousingBin { get { return this.housingBin; } }
        private int housingBin;

        public int LensBin { get { return this.lensBin; } }
        private int lensBin;

        public int BulbBin { get { return this.bulbBin; } }
        private int bulbBin;

        public int BezelBin { get { return this.bezelBin; } }
        private int bezelBin;
        private bool disposedValue;



        /* -------------------------------------------------------------------------------- */
        /* ------------------------------- PUBLIC INTERFACE ------------------------------- */
        /* -------------------------------------------------------------------------------- */



        /*  DESCRIPTION : Call to simulate the worker at their workstation, making
         *      foglamps.
         *  PARAMETERS  : void
         *  ALTERS      :
         *      Will decrement part counts, if parts are available.
         *      Will decrement fogLampsToMake, if a fog lamp is successfully made.
         *      Will increment fogLampsMade, if a fog lamp is successfully made.
         *  RETURNS     : void
         */
        public void SimulateWork()
        {
            double variance = this.getAssemblyTimeVarianceForThisRound();
            this.fogLampsToMake += this.minutesPerSecond * variance;

            while (this.fogLampsToMake >= 1)
            {
                // if there are parts available, make a foglamp
                if (this.arePartsAvailable())
                {
                    // Update our counts
                    this.fogLampsMade++;
                    this.fogLampsToMake--;

                    // Update the bins
                    this.harnessBin--;
                    this.reflectorBin--;
                    this.housingBin--;
                    this.lensBin--;
                    this.bulbBin--;
                    this.bezelBin--;
                }
                else
                {
                    // No parts available; break out of simulate loop
                    break;
                }
            }


            // Update these newly made fog lamps to Db Model
            this.simulateWorkOnDb();
        }



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        public void SendRunner()
        {
            // Runner needs to replenish part bins AND replace test tray if full

            // We actually need to check if the parts have reached their minimum yet...
            if (this.harnessBin <= Worker.harnessBinMin)
            {
                this.harnessBin = Worker.harnessBinMax;
            }

            if (this.reflectorBin <= Worker.reflectorBinMin)
            {
                this.reflectorBin = Worker.reflectorBinMax;
            }

            if (this.housingBin <= Worker.housingBinMin)
            {
                this.housingBin = Worker.housingBinMax;
            }

            if (this.lensBin <= Worker.lensBinMin)
            {
                this.lensBin = Worker.lensBinMax;
            }

            if (this.bulbBin <= Worker.bulbBinMin)
            {
                this.bulbBin = Worker.bulbBinMax;
            }

            if (this.bezelBin <= Worker.bezelBinMin)
            {
                this.bezelBin = Worker.bezelBinMax;
            }
            

            // Now replace the test tray, if needed
            TestTray tt = this.kdb.TestTrays.Find(this.currentTestTrayId);
            if (tt.IsCompleted)
            {
                // Grab a new test tray
                TestTray newtt = new TestTray();
                newtt.IsCompleted = false;
                newtt.IsCurrentlyInUse = true;
                this.kdb.TestTrays.Add(newtt);
                
                this.kdb.SaveChanges();
                this.currentTestTrayId = this.kdb.TestTrays.Count();
            }
        }


        public void EndWork()
        {
            // Release the workstation in use
            this.wstation.IsCurrentlyWorking = false;

            // Put the test tray away
            TestTray tt = this.kdb.TestTrays.Find(this.currentTestTrayId);
            tt.IsCurrentlyInUse = false;
            kdb.SaveChanges();
        }





        /* -------------------------------------------------------------------------------- */
        /* ---------------------------- PRIVATE IMPLEMENTATION ---------------------------- */
        /* -------------------------------------------------------------------------------- */



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        private double getAssemblyTimeVarianceForThisRound()
        {
            double variance;
            switch (this.experienceLevel)
            {
                case ExperienceLevel_t.Rookie:
                    variance = rand.NextDouble() * (-(this.assemblyTimeVariance));
                    break;
                case ExperienceLevel_t.Experienced:
                    variance = rand.NextDouble() * (2 * this.assemblyTimeVariance) - this.assemblyTimeVariance;
                    break;
                case ExperienceLevel_t.Senior:
                    variance = rand.NextDouble() * (this.assemblyTimeVariance);
                    break;
                default:
                    variance = 0.0;
                    break;
            }

            return variance+1;
        }



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        private bool arePartsAvailable()
        {
            if ((this.harnessBin > 0) &&
                (this.reflectorBin > 0) &&
                (this.housingBin > 0) &&
                (this.lensBin > 0) &&
                (this.bulbBin > 0) &&
                (this.bezelBin > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /*  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        private bool simulateWorkOnDb()
        {
            // Get a hold of the current test tray we are working on
            TestTray tt = kdb.TestTrays.Find(this.currentTestTrayId);
            

            if (tt.IsCompleted)
            {
                // Tray is complete and we are still waiting on runner; return
                return false;
            }



            int nextFogLampId = tt.FogLamps.Count + 1;
            
            

            while ((nextFogLampId <= 60) && (this.fogLampsMade > 0))
            {
                FogLamp fl = new FogLamp();
                fl.FogLampId = nextFogLampId;
                fl.ExperienceLevelOfAssembler = (int)this.experienceLevel;
                fl.TestTrayId = this.currentTestTrayId;

                kdb.FogLamps.Add(fl);

                nextFogLampId++;
                if (nextFogLampId == 61)
                {
                    // Mark tray as complete
                    tt.IsCompleted = true;
                }
            }


            kdb.SaveChanges();
            return true;
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.rand = null;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Worker()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
