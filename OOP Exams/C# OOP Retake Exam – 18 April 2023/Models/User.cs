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
        private string lastName;
        private string drivingLicenceNumber;

        public User(string firstName, string lastName, string drivingLicenceNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenceNumber;
            Rating = 0;
            IsBlocked = false;
        }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }

        public double Rating { get; private set; }

        public string DrivingLicenseNumber
        {
            get => drivingLicenceNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.LicenceNumberRequired);
                }
                firstName = value;
            }
        }

        public bool IsBlocked { get; private set; }

        public void DecreaseRating()
        {
            Rating -= 2;

            if (Rating < 0.0)
            {
                Rating = 0;
            }
        }

        public void IncreaseRating()
        {
            Rating += 0.5;

            if (Rating > 10)
            {
                Rating = 10;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}";
        }
    }
}
