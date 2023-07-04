using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }

        public string LastName => throw new NotImplementedException();

        public double Rating => throw new NotImplementedException();

        public string DrivingLicenseNumber => throw new NotImplementedException();

        public bool IsBlocked => throw new NotImplementedException();

        public void DecreaseRating()
        {
            throw new NotImplementedException();
        }

        public void IncreaseRating()
        {
            throw new NotImplementedException();
        }
    }
}
