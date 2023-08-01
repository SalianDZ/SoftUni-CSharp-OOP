using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VehicleConstructorTest()
        {
            Vehicle vehicle = new("Audi", "A5", "PA0000KK");
            Assert.IsNotNull(vehicle);
            Assert.AreEqual(vehicle.Model, "A5");
            Assert.AreEqual(vehicle.Brand, "Audi");
            Assert.AreEqual(vehicle.LicensePlateNumber, "PA0000KK");
            Assert.AreEqual(vehicle.BatteryLevel, 100);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]
        public void GarageConstructorTest()
        {
            Garage garage = new(5);
            Assert.IsNotNull(garage);
            Assert.AreEqual(garage.Capacity, 5);
            Assert.AreEqual(garage.Vehicles.Count, 0);
            List<Vehicle> currentVehicles = new();
            Assert.AreEqual(garage.Vehicles, currentVehicles);
        }

        [Test]
        public void AddVehicleMethodPositiveCase()
        {
            Garage garage = new(5);
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = garage.Vehicles.Count;
            bool result1 = garage.AddVehicle(new("Audi", "A5", "PA0000KK"));
            bool result2 = garage.AddVehicle(new("BMW", "M5", "PA0011KK"));
            int expectedCountAfterAdding = 2;
            int actualCountAfterAdding = garage.Vehicles.Count;
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
            Assert.IsTrue(result2);
            Assert.IsTrue(result1);
            Assert.AreEqual(garage.Vehicles[0].Brand, "Audi");
            Assert.AreEqual(garage.Vehicles[0].Model, "A5");
            Assert.AreEqual(garage.Vehicles[0].LicensePlateNumber, "PA0000KK");
            Assert.AreEqual(garage.Vehicles[1].Brand, "BMW");
            Assert.AreEqual(garage.Vehicles[1].Model, "M5");
            Assert.AreEqual(garage.Vehicles[1].LicensePlateNumber, "PA0011KK");

        }

        [Test]
        public void AddVehicleMethodNegativeCaseWhenCapacityIsFull()
        {
            Garage garage = new(2);
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = garage.Vehicles.Count;
            garage.AddVehicle(new("Audi", "A5", "PA0000KK"));
            garage.AddVehicle(new("BMW", "M5", "PA0011KK"));
            bool result = garage.AddVehicle(new("Mercedes", "M5", "PA1511KK"));
            int expectedCountAfterAdding = 2;
            int actualCountAfterAdding = garage.Vehicles.Count;
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddVehicleMethodNegativeCaseWhenThereIsAlreadyACarWithThisLicencePlateNumber()
        {
            Garage garage = new(5);
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = garage.Vehicles.Count;
            garage.AddVehicle(new("Audi", "A5", "PA0000KK"));
            garage.AddVehicle(new("BMW", "M5", "PA0011KK"));
            bool result = garage.AddVehicle(new ("Audi", "A5", "PA0000KK"));
            int expectedCountAfterAdding = 2;
            int actualCountAfterAdding = garage.Vehicles.Count;
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
            Assert.IsFalse(result);
        }

        [Test]
        public void DriveVehiclesMethodPositiveCaseWithoutAccident()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            garage.AddVehicle(vehicle1);
            garage.DriveVehicle("PA0000KK", 50, false);
            Assert.AreEqual(vehicle1.BatteryLevel, 50);
            Assert.IsFalse(vehicle1.IsDamaged);
        }

        [Test]
        public void DriveVehiclesMethodPositiveCaseWithAccident()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            garage.AddVehicle(vehicle1);
            garage.DriveVehicle("PA0000KK", 50, true);
            bool actualVehicleCondition = vehicle1.IsDamaged;
            Assert.AreEqual(vehicle1.BatteryLevel, 50);
            Assert.IsTrue(actualVehicleCondition);
        }

        [Test]
        public void DriveVehiclesMethodNegativeCaseReturnsNull()
        {
            Garage garage = new(5);
            Exception ex = Assert.Throws<NullReferenceException>(() => garage.DriveVehicle("AAA", 50, false));
        }

        [Test]
        public void DriveVehiclesMethodNegativeCaseVehicleIsDamaged()
        {
            Garage garage = new(5);
            Vehicle vehicle = new("Audi", "A5", "PA0000KK");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PA0000KK", 10, true);
            garage.DriveVehicle("PA0000KK", 50, true);
            int actualBatteryLevel = vehicle.BatteryLevel;
            Assert.AreEqual(90, actualBatteryLevel);

        }

        [Test]
        public void DriveVehiclesMethodNegativeCaseBatteryDrainageBiggerThan100()
        {
            Garage garage = new(5);
            Vehicle vehicle = new("Audi", "A5", "PA0000KK");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PA0000KK", 150, false);
            int actualBatteryLevel = vehicle.BatteryLevel;
            Assert.AreEqual(100, actualBatteryLevel);
        }

        [Test]
        public void DriveVehiclesMethodNegativeBatteryDrainageIsBiggerThanBattery()
        {
            Garage garage = new(5);
            Vehicle vehicle = new("Audi", "A5", "PA0000KK");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PA0000KK", 80, false);
            int expectedBatteryBeforeNegativeTest = 20;
            int actualBatteryBeforeNegativeTest = vehicle.BatteryLevel;
            garage.DriveVehicle("PA0000KK", 80, false);
            int expectedBatteryAfterNegativeTest = 20;
            int actualBatteryAfterNegativeTest = vehicle.BatteryLevel;
            Assert.AreEqual(expectedBatteryBeforeNegativeTest, actualBatteryBeforeNegativeTest);
            Assert.AreEqual(expectedBatteryAfterNegativeTest, actualBatteryAfterNegativeTest);
        }

        [Test]
        public void ChargeVehiclesMethodPositiveCase()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            Vehicle vehicle2 = new("BMW", "M5", "PA0011KK");
            Vehicle vehicle3 = new("Mercedes", "C5", "PA1111KK");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);
            garage.DriveVehicle("PA0000KK", 70, false);
            garage.DriveVehicle("PA0011KK", 60, false);
            garage.DriveVehicle("PA1111KK", 50, false);
            int chargedVehicles = garage.ChargeVehicles(40);
            Assert.AreEqual(chargedVehicles, 2);
        }

        [Test]
        public void ChargeVehiclesMethodNegativeCase()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            Vehicle vehicle2 = new("BMW", "M5", "PA0011KK");
            Vehicle vehicle3 = new("Mercedes", "C5", "PA1111KK");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);
            garage.DriveVehicle("PA0000KK", 30, false);
            garage.DriveVehicle("PA0011KK", 40, false);
            garage.DriveVehicle("PA1111KK", 50, false);
            int chargedVehicles = garage.ChargeVehicles(40);
            Assert.AreEqual(chargedVehicles, 0);
        }

        [Test]
        public void RepairVehiclesMethodPositiveCase()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            Vehicle vehicle2 = new("BMW", "M5", "PA0011KK");
            Vehicle vehicle3 = new("Mercedes", "C5", "PA1111KK");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);
            garage.DriveVehicle("PA0000KK", 30, true);
            garage.DriveVehicle("PA0011KK", 40, true);
            garage.DriveVehicle("PA1111KK", 50, false);
            string result = garage.RepairVehicles();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Vehicles repaired: 2");
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]
        public void RepairVehiclesMethodNegativeCase()
        {
            Garage garage = new(5);
            Vehicle vehicle1 = new("Audi", "A5", "PA0000KK");
            Vehicle vehicle2 = new("BMW", "M5", "PA0011KK");
            Vehicle vehicle3 = new("Mercedes", "C5", "PA1111KK");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);
            garage.DriveVehicle("PA0000KK", 30, false);
            garage.DriveVehicle("PA0011KK", 40, false);
            garage.DriveVehicle("PA1111KK", 50, false);
            string result = garage.RepairVehicles();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Vehicles repaired: 0");
        }
    }
}