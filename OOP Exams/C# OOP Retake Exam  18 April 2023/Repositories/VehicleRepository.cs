using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles = new();
        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier)
        {
            return vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier);
        }

        public IReadOnlyCollection<IVehicle> GetAll()
        {
            return vehicles.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            if (vehicles.Any(v => v.LicensePlateNumber == identifier))
            {
                vehicles.Remove(vehicles.FirstOrDefault(v => v.LicensePlateNumber == identifier));
                return true;
            }

            return false;
        }

        public bool Contains(string licensePlateNumber)
        {
            if (vehicles.Any(x => x.LicensePlateNumber == licensePlateNumber))
            {
                return true;
            }

            return false;
        }
    }
}

