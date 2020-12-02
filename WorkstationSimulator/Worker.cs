using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        private Random rand;
        private double assemblyTimeVariance;

        public Worker(ExperienceLevel_t experienceLevel, int minutesPerSecond)
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

            }
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
            if (() &&
                () &&
                () &&
                () &&
                () &&
                ())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
