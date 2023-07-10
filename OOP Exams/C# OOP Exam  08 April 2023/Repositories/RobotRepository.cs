using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robots = new();

        public void AddNew(IRobot model)
        {
            robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            if (robots.Any(x => x.InterfaceStandards.Contains(interfaceStandard)))
            {
                return robots.First(x => x.InterfaceStandards.Contains(interfaceStandard));
            }

            return null;
        }

        public IReadOnlyCollection<IRobot> Models()
        {
            return robots.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            if (robots.Any(x => x.Model == typeName))
            {
                robots.Remove(robots.First(x => x.Model == typeName));
                return true;
            }

            return false;
        }
    }
}
