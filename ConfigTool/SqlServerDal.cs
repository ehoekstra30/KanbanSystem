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



            // SET ALL PARAMETERS
            cmd.Parameters.AddWithValue("@HarnessCap", cm.HarnessBinCapacity.ToString());
            cmd.Parameters.AddWithValue("@ReflectorCap", cm.ReflectorBinCapacity.ToString());
            cmd.Parameters.AddWithValue("@HousingCap", cm.HousingBinCapacity.ToString());
            cmd.Parameters.AddWithValue("@LensCap", cm.LensBinCapacity.ToString());
            cmd.Parameters.AddWithValue("@BulbCap", cm.BulbBinCapacity.ToString());
            cmd.Parameters.AddWithValue("@BezelCap", cm.BezelBinCapacity.ToString());

            cmd.Parameters.AddWithValue("@HarnessMin", cm.HarnessBinMinimum.ToString());
            cmd.Parameters.AddWithValue("@ReflectorMin", cm.ReflectorBinMinimum.ToString());
            cmd.Parameters.AddWithValue("@HousingMin", cm.HousingBinMinimum.ToString());
            cmd.Parameters.AddWithValue("@LensMin", cm.LensBinMinimum.ToString());
            cmd.Parameters.AddWithValue("@BulbMin", cm.BulbBinMinimum.ToString());
            cmd.Parameters.AddWithValue("@BezelMin", cm.BezelBinMinimum.ToString());

            cmd.Parameters.AddWithValue("@RunnerFreq", cm.RunnerFrequency.ToString());
            cmd.Parameters.AddWithValue("@Tick", cm.SecondsPerTick.ToString());
            cmd.Parameters.AddWithValue("@NumStations", cm.NumAssemblyStations.ToString());

            cmd.Parameters.AddWithValue("@RookieDefect", cm.RookieDefectRate.ToString());
            cmd.Parameters.AddWithValue("@ExperiencedDefect", cm.ExperiencedDefectRate.ToString());
            cmd.Parameters.AddWithValue("@SeniorDefect", cm.SeniorDefectRate.ToString());

            cmd.Parameters.AddWithValue("@AssemblyTime", cm.AssemblyBaseTime.ToString());
            cmd.Parameters.AddWithValue("@RookieVariance", cm.RookieAssemblyTimeVariance.ToString());
            cmd.Parameters.AddWithValue("@ExperiencedVariance", cm.ExperiencedAssemblyTimeVariance.ToString());
            cmd.Parameters.AddWithValue("@SeniorVariance", cm.SeniorAssemblyTimeVariance.ToString());

            cmd.Parameters.AddWithValue("@RookieSpeed", cm.RookieAssemblySpeed.ToString());
            cmd.Parameters.AddWithValue("@ExperiencedSpeed", cm.ExperiencedAssemblySpeed.ToString());
            cmd.Parameters.AddWithValue("@SeniorSpeed", cm.SeniorAssemblySpeed.ToString());

            cmd.Parameters.AddWithValue("@TrayCap", cm.TestTrayCapacity.ToString());




            this.update(cmd);
        }


        public override ConfigModel GetAllConfigurationDetails()
        {
            SqlCommand cmd = new SqlCommand(SqlServerDal.SELECT_ALL_CONFIG_TABLE);
            cmd.CommandType = CommandType.StoredProcedure;

            return new ConfigModel(this.select(cmd));
        }


        private DataTable select(SqlCommand cmd)
        {
            SqlConnection conn = new SqlConnection(this.connstr);
            cmd.Connection = conn;

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            conn.Close();

            return dt;
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
