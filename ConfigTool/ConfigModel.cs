using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool
{
    public class ConfigModel
    {
		private int harnessBinCapacity;
		private int reflectorBinCapacity;
		private int housingBinCapacity;
		private int lensBinCapacity;
		private int bulbBinCapacity;
		private int bezelBinCapacity;

		private int harnessBinMinimum;
		private int reflectorBinMinimum;
		private int housingBinMinimum;
		private int lensBinMinimum;
		private int bulbBinMinimum;
		private int bezelBinMinimum;

		private int runnerFrequency;
		private int secondsPerTick;

		private int numAssemblyStations;

		private float rookieDefectRate;
		private float experiencedDefectRate;
		private float seniorDefectRate;

		private int assemblyBaseTime;
		private float rookieAssemblyTimeVariance;
		private float experiencedAssemblyTimeVariance;
		private float seniorAssemblyTimeVariance;

		private int rookieAssemblySpeed;
		private int experiencedAssemblySpeed;
		private int seniorAssemblySpeed;

		private int testTrayCapacity;


		// ACCESSORS & MUTATORS
		public int HarnessBinCapacity
        {
			get => this.harnessBinCapacity;
			set
            {
				this.harnessBinCapacity = value;
            }
        }

		public int ReflectorBinCapacity
        {
			get => this.reflectorBinCapacity;
			set
            {
				this.reflectorBinCapacity = value;
            }
        }

		public int HousingBinCapacity
        {
			get => this.housingBinCapacity;
			set
            {
				this.housingBinCapacity = value;
            }
        }

		public int LensBinCapacity
        {
			get => this.lensBinCapacity;
			set
            {
				this.lensBinCapacity = value;
            }
        }

		public int BulbBinCapacity
        {
			get => this.bulbBinCapacity;
			set
            {
				this.bulbBinCapacity = value;
            }
        }

		public int BezelBinCapacity
        {
			get => this.bezelBinCapacity;
			set
            {
				this.bezelBinCapacity = value;
            }
        }

		public int HarnessBinMinimum
        {
			get => this.harnessBinMinimum;
			set
            {
				this.harnessBinMinimum = value;
            }
        }

		
		public int ReflectorBinMinimum
        {
			get => this.reflectorBinMinimum;
			set
            {
				this.reflectorBinMinimum = value;
            }
        }

		public int HousingBinMinimum
        {
			get => this.housingBinMinimum;
			set
            {
				this.housingBinMinimum = value;
            }
        }


		public int LensBinMinimum
        {
			get => this.lensBinMinimum;
			set
            {
				this.lensBinMinimum = value;
            }
        }
		
		public int BulbBinMinimum
        {
			get => this.bulbBinMinimum;
			set
            {
				this.bulbBinMinimum = value;
            }
        }
		

		public int BezelBinMinimum
        {
			get => this.bezelBinMinimum;
			set
            {
				this.bezelBinMinimum = value;
            }
        }


		public int RunnerFrequency
        {
			get => this.runnerFrequency;
			set
            {
				this.runnerFrequency = value;
            }
        }

        public int SecondsPerTick { get => secondsPerTick; set => secondsPerTick = value; }
        public int NumAssemblyStations { get => numAssemblyStations; set => numAssemblyStations = value; }
        public float RookieDefectRate { get => rookieDefectRate; set => rookieDefectRate = value; }
        public float ExperiencedDefectRate { get => experiencedDefectRate; set => experiencedDefectRate = value; }
        public float SeniorDefectRate { get => seniorDefectRate; set => seniorDefectRate = value; }
        public int AssemblyBaseTime { get => assemblyBaseTime; set => assemblyBaseTime = value; }
        public float RookieAssemblyTimeVariance { get => rookieAssemblyTimeVariance; set => rookieAssemblyTimeVariance = value; }
        public float ExperiencedAssemblyTimeVariance { get => experiencedAssemblyTimeVariance; set => experiencedAssemblyTimeVariance = value; }
        public float SeniorAssemblyTimeVariance { get => seniorAssemblyTimeVariance; set => seniorAssemblyTimeVariance = value; }
        public int RookieAssemblySpeed { get => rookieAssemblySpeed; set => rookieAssemblySpeed = value; }
        public int ExperiencedAssemblySpeed { get => experiencedAssemblySpeed; set => experiencedAssemblySpeed = value; }
        public int SeniorAssemblySpeed { get => seniorAssemblySpeed; set => seniorAssemblySpeed = value; }
        public int TestTrayCapacity { get => testTrayCapacity; set => testTrayCapacity = value; }
    }
}
