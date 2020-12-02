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

        public Worker(ExperienceLevel_t experienceLevel)
        {
            this.experienceLevel = experienceLevel;
        }
    }
}
