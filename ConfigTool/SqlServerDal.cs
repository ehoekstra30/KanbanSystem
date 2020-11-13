using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool
{
    public class SqlServerDal : AbstractDal
    {
        private string connstr;

        private static string SP_UPDATE_CONFIG_TABLE =
            "EXEC sp_UpdateConfigTable " +
            "@HarnessCap @ReflectorCap @HousingCap @LensCap @BulbCap " +
            "@BezelCap @HarnessMin @ReflectorMin @HousingMin @LensMin " +
            "@BulbMin @BezelMin @RunnerFreq @Tick @NumStations " +
            "@RookieDefect @ExperiencedDefect @SeniorDefect " +
            "@AssemblyTime @RookieVariance @ExperiencedVariance " +
            "@SeniorVariance @RookieSpeed @ExperiencedSpeed @SeniorSpeed " +
            "@TrayCap; ";


        /* !!! CONSTRUCTOR !!! */
        public SqlServerDal(string connstr)
        {
            this.connstr = connstr;
        }


        public override void UpdateConfiguration(ConfigModel cm)
        {
            SqlCommand cmd = new SqlCommand(SqlServerDal.SP_UPDATE_CONFIG_TABLE);
            cmd.CommandType = CommandType.StoredProcedure;
        }
    }
}
