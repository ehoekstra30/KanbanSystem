using System;
using System.Collections.Generic;
using System.Data;
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


		public ConfigModel() { }

		public ConfigModel(DataTable dt)
        {
			this.harnessBinCapacity = Int32.Parse(dt.Rows.Find("HarnessCapacity").Field<string>("SystemValue"));
			this.reflectorBinCapacity = Int32.Parse(dt.Rows.Find("ReflectorCapacity").Field<string>("SystemValue"));
			this.housingBinCapacity = Int32.Parse(dt.Rows.Find("HousingCapacity").Field<string>("SystemValue"));
			this.lensBinCapacity = Int32.Parse(dt.Rows.Find("LensCapacity").Field<string>("SystemValue"));
			this.bulbBinCapacity = Int32.Parse(dt.Rows.Find("BulbCapacity").Field<string>("SystemValue"));
			this.bezelBinCapacity = Int32.Parse(dt.Rows.Find("BezelCapacity").Field<string>("SystemValue"));

			this.harnessBinMinimum = Int32.Parse(dt.Rows.Find("HarnessMinimum").Field<string>("SystemValue"));
			this.reflectorBinMinimum = Int32.Parse(dt.Rows.Find("ReflectorMinimum").Field<string>("SystemValue"));
			this.housingBinMinimum = Int32.Parse(dt.Rows.Find("HousingMinimum").Field<string>("SystemValue"));
			this.lensBinMinimum = Int32.Parse(dt.Rows.Find("LensMinimum").Field<string>("SystemValue"));
			this.bulbBinMinimum = Int32.Parse(dt.Rows.Find("BulbMinimum").Field<string>("SystemValue"));
			this.bezelBinMinimum = Int32.Parse(dt.Rows.Find("BezelMinimum").Field<string>("SystemValue"));

			this.runnerFrequency = Int32.Parse(dt.Rows.Find("RunnerFrequency").Field<string>("SystemValue"));
			this.secondsPerTick = Int32.Parse(dt.Rows.Find("TickTime").Field<string>("SystemValue"));

			this.numAssemblyStations = Int32.Parse(dt.Rows.Find("NumberOfAssemblyStations").Field<string>("SystemValue"));

			this.rookieDefectRate = float.Parse(dt.Rows.Find("RookieDefectRate").Field<string>("SystemValue"));
			this.experiencedDefectRate = float.Parse(dt.Rows.Find("ExperiencedDefectRate").Field<string>("SystemValue"));
			this.seniorDefectRate = float.Parse(dt.Rows.Find("SeniorDefectRate").Field<string>("SystemValue"));

			this.assemblyBaseTime = Int32.Parse(dt.Rows.Find("AssemblyBaseTime").Field<string>("SystemValue"));
			this.rookieAssemblyTimeVariance = float.Parse(dt.Rows.Find("RookieAssemblyTimeVariance").Field<string>("SystemValue"));
			this.experiencedAssemblyTimeVariance = float.Parse(dt.Rows.Find("ExperiencedAssemblyTimeVariance").Field<string>("SystemValue"));
			this.seniorAssemblyTimeVariance = float.Parse(dt.Rows.Find("SeniorAssemblyTimeVariance").Field<string>("SystemValue"));

			this.rookieAssemblySpeed = Int32.Parse(dt.Rows.Find("RookieAssemblySpeed").Field<string>("SystemValue"));
			this.experiencedAssemblySpeed = Int32.Parse(dt.Rows.Find("ExperiencedAssemblySpeed").Field<string>("SystemValue"));
			this.seniorAssemblySpeed = Int32.Parse(dt.Rows.Find("SeniorAssemblySpeed").Field<string>("SystemValue"));

			this.testTrayCapacity = Int32.Parse(dt.Rows.Find("TestTrayCapacity").Field<string>("SystemValue"));
		}

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
