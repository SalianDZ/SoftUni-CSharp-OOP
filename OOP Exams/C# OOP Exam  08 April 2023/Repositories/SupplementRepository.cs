using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> supplements = new();
        public void AddNew(ISupplement model)
        {
            supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            if (supplements.Any(x => x.InterfaceStandard == interfaceStandard))
            {
                return supplements.First(x => x.InterfaceStandard == interfaceStandard);
            }

            return null;
        }

        public IReadOnlyCollection<ISupplement> Models()
        {
            return supplements.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            if (supplements.Any(x => x.GetType().Name == typeName))
            {
                return true;
            }

            return false;
        }
    }
}
