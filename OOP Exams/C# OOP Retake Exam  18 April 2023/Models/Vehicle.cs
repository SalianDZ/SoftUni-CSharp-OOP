using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            MaxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            BatteryLevel = 100;
            IsDamaged = false;
        }
        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }
        public double MaxMileage { get; private set; }

        public string LicensePlateNumber
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get; private set; }

        public bool IsDamaged { get; private set; }

        public void ChangeStatus()
        {
            if (IsDamaged)
            {
                IsDamaged = false;
            }
            else
            {
                IsDamaged = true;
            }
        }

        public void Drive(double mileage)
        {
            double percentage = Math.Round((mileage / MaxMileage) * 100);

            if (Model == "CargoVan")
            {
                percentage += 5;
            }
            BatteryLevel -= (int) percentage;
        }

        public void Recharge()
        {
            BatteryLevel = 100;
        }

        public override string ToString()
        {
            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: OK/damaged";
        }
    }
}
