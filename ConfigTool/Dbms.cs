using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool
{
    public static class Dbms
    {
        private static AbstractDal dal;     // internal tracked instance of database connection


        /*  FUNCTION    :
         *  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        public static AbstractDal Connect()
        {
            // IDEA -- create a NullDal : AbstractDal that can handle if connection fails??
            // ie return a NullDal object if 1st-time setup() fails
            if (Dbms.dal == null)
            {
                Dbms.setup();
            }
            return Dbms.dal;
        }



        /*  FUNCTION    :
         *  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        public static void Disconnect()
        {
            Dbms.dal = null;
        }



        /*  FUNCTION    :
         *  DESCRIPTION :
         *  PARAMETERS  :
         *  ALTERS      :
         *  RETURNS     :
         */
        private static void setup()
        {
            //first time setup of connection...
            // use the configuration manager
            switch (ConfigurationManager.ConnectionStrings["kanban"].ProviderName)
            {
                case "System.Data.SqlClient":
                    Dbms.dal = new SqlServerDal(
                        ConfigurationManager.ConnectionStrings["kanban"].ConnectionString);
                    break;
                default:
                    Dbms.dal = new NullDal();
                    break;
            }
        }
    }
}
