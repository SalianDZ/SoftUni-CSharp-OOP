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
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;
        public void AddModel(IUser model)
        {
            users.Add(model);
        }

        public IUser FindById(string identifier)
        {
            return users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);
        }

        public IReadOnlyCollection<IUser> GetAll()
        {
            return users.AsReadOnly();
        }

        public bool RemoveById(string identifier)
        {
            if (users.Any(u => u.DrivingLicenseNumber == identifier))
            {
                users.Remove(users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier));
                return true;
            }

            return false;
        }
    }
}
