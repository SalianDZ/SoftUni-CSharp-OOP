using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;
        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            return routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
        }

        public IReadOnlyCollection<IRoute> GetAll()
        {
            return routes.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            if (routes.Any(r => r.RouteId == int.Parse(identifier)))
            {
                routes.Remove(routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier)));
                return true;
            }

            return false;
        }
    }
}
