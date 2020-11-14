using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool
{
    public abstract class AbstractDal
    {
        public abstract void UpdateConfiguration(ConfigModel cm);
        public abstract ConfigModel GetAllConfigurationDetails();
    }
}
