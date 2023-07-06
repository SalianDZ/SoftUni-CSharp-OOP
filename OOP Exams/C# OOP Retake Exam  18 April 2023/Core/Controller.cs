using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using System.Linq;
using System.Text;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private UserRepository users;
        private VehicleRepository vehicles;
        private RouteRepository routes;
        public Controller()
        {
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeId = this.routes.GetAll().Count + 1;

            IRoute existingRoute = this.routes
                .GetAll()
                .FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint);

            if (existingRoute != null)
            {
                if (existingRoute.Length == length)
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                else if (existingRoute.Length < length)
                {
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
                }
                else if (existingRoute.Length > length)
                {
                    existingRoute.LockRoute();
                }
            }
            IRoute newRoute = new Route(startPoint, endPoint, length, routeId);
            this.routes.AddModel(newRoute);

            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }


        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            IUser currentUser = users.FindById(drivingLicenseNumber);

            if (currentUser.IsBlocked)
            {
                string result = string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
                return result;
            }

            IVehicle currentVehicle = vehicles.FindById(licensePlateNumber);

            if (currentVehicle.IsDamaged)
            {
                string result = string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
                return result;
            }

            IRoute currentRoute = routes.FindById(routeId);

            if (currentRoute.IsLocked)
            {
                string result = string.Format(OutputMessages.RouteLocked, routeId);
                return result;
            }

            currentVehicle.Drive(currentRoute.Length);

            if (isAccidentHappened)
            {
                currentVehicle.ChangeStatus();
                currentUser.DecreaseRating();
            }
            else
            {
                currentUser.IncreaseRating();
            }

            return currentVehicle.ToString();
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            string result;
            if (users.Contains(drivingLicenseNumber))
            {
                result = string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
                return result;
            }
            IUser currentUser = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(currentUser);
            result = string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
            return result;
        }

        public string RepairVehicles(int count)
        {
            var damagedVehicles = this.vehicles.GetAll().Where(v => v.IsDamaged == true).OrderBy(v => v.Brand).ThenBy(v => v.Model);

            int vehiclesCount = 0;

            if (damagedVehicles.Count() < count)
            {
                vehiclesCount = damagedVehicles.Count();
            }
            else
            {
                vehiclesCount = count;
            }

            var selectedVehicles = damagedVehicles.ToArray().Take(vehiclesCount);

            foreach (var vehicle in selectedVehicles)
            {
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }

            return string.Format(OutputMessages.RepairedVehicles, vehiclesCount);
        }


        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            string vehicleTypeCopy = vehicleType;
            vehicleType = vehicleType.ToLower();
            string result;
            if (vehicleType != "passengercar" && vehicleType != "cargovan")
            {
                result = string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleTypeCopy);
                return result;
            }

            if (vehicles.Contains(licensePlateNumber))
            {
                result = string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
                return result;
            }


            IVehicle vehicle;
            if (vehicleType == "passengercar")
            {
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            else
            {
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            }

            vehicles.AddModel(vehicle);
            result = string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
            return result;
        }

        public string UsersReport()
        {
            StringBuilder sb = new();
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in this.users.GetAll().OrderByDescending(u => u.Rating).ThenBy(u => u.LastName).ThenBy(u => u.FirstName))
            {
                sb.AppendLine(user.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
