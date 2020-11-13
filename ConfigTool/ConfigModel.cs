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
    }
}
