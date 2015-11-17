using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyWeb.ControllerHelper
{



    public static class Enumerations
    {
        public enum PersonsAttributeCategoryEnum
        {
            EmergencyContact=1
        };

        public enum ModifyActionRequired
        {
            None = 0,
            Add = 1,
            Update = 2,
            Delete = 3
        };
    }
}
