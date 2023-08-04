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
            return robots.FirstOrDefault(x => x.InterfaceStandards.Contains(interfaceStandard));
        }

        public IReadOnlyCollection<IRobot> Models() => robots.AsReadOnly();

        public bool RemoveByName(string typeName)
        {
            return robots.Remove(robots.FirstOrDefault(x => x.Model == typeName)); // this can cause errors
        }
    }
}
