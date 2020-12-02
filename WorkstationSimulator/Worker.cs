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
        private float fogLampsToMake;

        private Random rand;
        private double assemblyTimeVariance;

        public Worker(ExperienceLevel_t experienceLevel, int minutesPerSecond)
        {
            this.experienceLevel = experienceLevel;
            this.minutesPerSecond = minutesPerSecond;
            this.fogLampsToMake = 0;
        }


        public void SimulateWork()
        {
            this.getAssemblyTimeVarianceForThisRound();
            this.fogLampsToMake += this.minutesPerSecond * this.
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

            return variance;
        }
    }
}
