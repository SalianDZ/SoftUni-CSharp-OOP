using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users = new();
        private VehicleRepository vehicles = new();
        private RouteRepository routes = new();

        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            if (routes.GetAll().Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length == length))
            {
                throw new ArgumentException(String.Format(OutputMessages.RouteExisting, startPoint, endPoint, length));
            }
            else if (routes.GetAll().Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length < length))
            {
                throw new ArgumentException(String.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint));
            }
            else
            {
                if (routes.GetAll().Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length > length))
                {
                    routes.GetAll()
                    .FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length > length)
                    .LockRoute();
                }

                int currentId = routes.GetAll().Count + 1;
                IRoute route = new Route(startPoint, endPoint, length, currentId);
                routes.AddModel(route);
                return String.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
            }
        } // done

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser user = users.FindById(drivingLicenseNumber);

            if (user.IsBlocked)
            {
                throw new ArgumentException(String.Format(OutputMessages.UserBlocked, drivingLicenseNumber));
            }

            IVehicle vehicle = vehicles.FindById(licensePlateNumber);

            if (vehicle.IsDamaged)
            {
                throw new ArgumentException(String.Format(OutputMessages.VehicleDamaged, licensePlateNumber));
            }

            IRoute route = routes.FindById(routeId);

            if (route.IsLocked)
            {
                throw new ArgumentException(String.Format(OutputMessages.RouteLocked, routeId));
            }

            vehicle.Drive(route.Length);

            if (isAccidentHappened)
            {
                vehicle.ChangeStatus();
                user.DecreaseRating();
            }
            else
            {
                user.IncreaseRating();
            }

            return vehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.GetAll().Any(x => x.DrivingLicenseNumber == drivingLicenseNumber))
            {
                throw new ArgumentException(String.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber));
            }

            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return String.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        } // done

        public string RepairVehicles(int count)
        {
            int counter = 0;
            foreach (var vehicle in vehicles.GetAll().OrderBy(x => x.Brand).ThenBy(x => x.Model).Where(x => x.IsDamaged == true))
            { 
                vehicle.Recharge();
                vehicle.ChangeStatus();
                counter++;

                if (counter == count)
                {
                    break;
                }
            }

            return String.Format(OutputMessages.RepairedVehicles, counter);
        } // done

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (vehicleType != "PassengerCar" && vehicleType != "CargoVan")
            {
                throw new ArgumentException(String.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType));
            }

            if (vehicles.GetAll().Any(x => x.LicensePlateNumber == licensePlateNumber))
            {
                throw new ArgumentException(String.Format(OutputMessages.LicensePlateExists, licensePlateNumber));
            }

            IVehicle vehicle;

            if (vehicleType == "PassengerCar")
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }

            vehicles.AddModel(vehicle);
            return String.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        } // done

        public string UsersReport()
        {
            StringBuilder sb = new();
            sb.AppendLine("*** E-Drive-Rent ***");

            foreach (var user in users.GetAll().OrderByDescending(x => x.Rating).ThenBy(x => x.LastName).ThenBy(x => x.FirstName))
            {
                sb.AppendLine(user.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
