using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanbanCore;


/*  FILE        : Worker.cs
 *  PROJECT     : AdvSql Project
 *  DEVELOPERS  : Ethan Hoekstra & Mohamed Benzreba
 *  PURPOSE     : Models a worker at a Workstation, putting together foglamps.
 */


namespace WorkstationSimulator
{
    public enum ExperienceLevel_t
    {
        Rookie,
        Experienced,
        Senior
    }

    public class Worker
    {
        private ExperienceLevel_t experienceLevel;
        private int minutesPerSecond;
        private double fogLampsToMake;
        private int fogLampsMade;
        private int fogLampsOnTestTray;
        private int currentTestTrayId;

        private Random rand;
        private double assemblyTimeVariance;

        public Worker(KanbanDbModel kdb, ExperienceLevel_t experienceLevel, int minutesPerSecond)
        {
            this.experienceLevel = experienceLevel;
            this.minutesPerSecond = minutesPerSecond;
            this.fogLampsToMake = 0;
            this.fogLampsMade = 0;
        }


        public int HarnessBin { get; }
        private int harnessBin;

        public int ReflectorBin { get; }
        private int reflectorBin;

        public int HousingBin { get; }
        private int housingBin;

        public int LensBin { get; }
        private int lensBin;

        public int BulbBin { get; }
        private int bulbBin;

        public int BezelBin { get; }
        private int bezelBin;


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

        }


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

        private bool simulateWorkOnDb(KanbanDbModel kdb)
        {
            // For each fogLampMade...
            // ...

            // Get a hold of the current test tray we are working on
            TestTray tt = kdb.TestTrays.Find(this.currentTestTrayId);
            

            if (tt.IsCompleted)
            {
                // Tray is complete and we are still waiting on runner; return
                return true;
            }

            int nextFogLampId = tt.FogLamps.Count + 1;
            FogLamp fl = new FogLamp();
            fl.ExperienceLevelOfAssembler = (int)this.experienceLevel;
            fl.TestTrayId = this.currentTestTrayId;

            while ((nextFogLampId <= 60) && (this.fogLampsMade > 0))
            {
                fl.FogLampId = nextFogLampId;
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
    }
}
