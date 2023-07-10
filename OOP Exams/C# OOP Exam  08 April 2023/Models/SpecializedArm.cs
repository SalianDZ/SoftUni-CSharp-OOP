using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        private const int InterfaceStandard = 10045;
        private const int BatteryUsage = 10000;
        public SpecializedArm()
            : base(InterfaceStandard, BatteryUsage)
        {
        }
    }
}
