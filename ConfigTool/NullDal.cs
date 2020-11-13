using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool
{
    public class NullDal : AbstractDal
    {
        public override void UpdateConfiguration(ConfigModel cm)
        {
            // do nothing;
        }
    }
}
