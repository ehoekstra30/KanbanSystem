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
        private static string SELECT_ALL_CONFIG_TABLE =
            "SELECT * FROM ConfigTable; ";


        /* !!! CONSTRUCTOR !!! */
        public SqlServerDal(string connstr)
        {
            this.connstr = connstr;
        }


        public override void UpdateConfiguration(ConfigModel cm)
        {
            SqlCommand cmd = new SqlCommand(SqlServerDal.SP_UPDATE_CONFIG_TABLE);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@HarnessCap", cm.HarnessBinCapacity);
        }


        public override ConfigModel GetAllConfigurationDetails()
        {
            return new ConfigModel();
        }


        private void update(SqlCommand cmd)
        {
            /* Set up connection */
            SqlConnection conn = new SqlConnection(this.connstr);
            cmd.Connection = conn;

            /* OPEN CONNECTION */
            conn.Open();


            /* Execute the stored procedure */
            int rowsAffected = cmd.ExecuteNonQuery();


            /* CLOSE CONNECTION */
            conn.Close();
        }
    }
}
