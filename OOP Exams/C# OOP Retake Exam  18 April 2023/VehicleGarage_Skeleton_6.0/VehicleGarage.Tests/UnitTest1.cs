using NUnit.Framework;
using System;
using System.Linq;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Garage garage;
        [SetUp]
        public void Setup()
        {
            garage = new(10);
        }

        [TearDown]
        public void TearDown()
        {
            garage = null;
        }

        [Test]
        public void GarageConstructorTest()
        {
            Assert.AreEqual(garage.Capacity, 10);
            Assert.AreEqual(garage.Vehicles.Count, 0);
        }

        [Test]
        public void TestIfAddVehicleMethodReturnsTheCorrectBooleanWhenCapacityIsFull()
        {
            garage = new(2);
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "PA0910BB"));
            garage.AddVehicle(new Vehicle("Tesla", "Model Y", "PA5400BB"));
            bool result = garage.AddVehicle(new Vehicle("Tesla", "Model 3", "PA5400BB"));
            Assert.IsFalse(result);
            Assert.AreEqual(2, garage.Vehicles.Count);
        }

        [Test]
        public void TestIfAddVehicleMethodReturnsTheCorrectBooleanWhenThereIsAlreadySuchALicensePlate()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "PA0910BB"));
            bool result = garage.AddVehicle(new Vehicle("Tesla", "Model S", "PA0910BB"));
            Assert.IsFalse(result);
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]
        public void TestIfAddMethodActuallyAddAVehicleToTheCollection()
        {
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = garage.Vehicles.Count;

            bool result = garage.AddVehicle(new Vehicle("Tesla", "Model S", "PA0910BB"));

            int expectedCountAfterAdding = 1;
            int actualCountAfterAdding = garage.Vehicles.Count;

            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
            Assert.IsTrue(result);
            Assert.AreEqual(garage.Vehicles[0].Brand, "Tesla");
            Assert.AreEqual(garage.Vehicles[0].Model, "Model S");
            Assert.AreEqual(garage.Vehicles[0].LicensePlateNumber, "PA0910BB");
        }

        [Test]
        public void Garage_ChargeVihicle()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("CT7006H", 51, false);
            garage.DriveVehicle("H7806AH", 51, false);
            garage.DriveVehicle("P7006XX", 51, false);
            garage.DriveVehicle("PB6006PA", 50, false);

            int actualChargedVehicles = garage.ChargeVehicles(49);

            Assert.AreEqual(3, actualChargedVehicles);
        }


        [Test]
        public void TestIfDriveMethodWillNotDriveACarIfTheVehicleIsDamaged()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "CA0505VG"));
            garage.DriveVehicle("CA0505VG", 30, true);
            garage.DriveVehicle("CA0505VG", 30, true);
            Assert.AreEqual(garage.Vehicles.FirstOrDefault().BatteryLevel, 70);
        }

        [Test]
        public void TestIfDriveMethodWillNotDriveACarIfBatteryDrainageIsGreaterThan100()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "CA0505VG"));
            garage.DriveVehicle("CA0505VG", 150, false);
            Assert.AreEqual(garage.Vehicles.FirstOrDefault().BatteryLevel, 100);
        }

        [Test]
        public void TestIfDriveMethodWillNotDriveACarIfBatteryLevelIsLessThanBatteryDrainage()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "CA0505VG"));
            garage.DriveVehicle("CA0505VG", 70, false);
            garage.DriveVehicle("CA0505VG", 70, false);
            Assert.AreEqual(garage.Vehicles.FirstOrDefault().BatteryLevel, 30);
        }

        [Test]
        public void TestIfDriveMethodThrowsExceptionWhenTheCarIsNull()
        {
            Assert.Throws<NullReferenceException>(() => garage.DriveVehicle("CA0505VG", 70, false));
        }

        [Test]
        public void TestIfDriveMethodDrivesACarButNoAccidentHappens()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "CA0505VG"));
            garage.DriveVehicle("CA0505VG", 70, false);
            Assert.AreEqual(garage.Vehicles.FirstOrDefault().BatteryLevel, 30);
            Assert.IsFalse(garage.Vehicles.FirstOrDefault().IsDamaged);
        }

        [Test]
        public void TestIfDriveMethodDrivesACarButAccidentHappens()
        {
            garage.AddVehicle(new Vehicle("Tesla", "Model S", "CA0505VG"));
            garage.DriveVehicle("CA0505VG", 70, true); 
            Assert.AreEqual(garage.Vehicles.FirstOrDefault().BatteryLevel, 30);
            Assert.IsTrue(garage.Vehicles.FirstOrDefault().IsDamaged);
        }

        [Test]
        public void Garage_RepairVihicles()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("CT7006H", 51, true);
            garage.DriveVehicle("H7806AH", 51, true);
            garage.DriveVehicle("P7006XX", 51, true);
            garage.DriveVehicle("PB6006PA", 50, false);

            string actualResult = garage.RepairVehicles();
            string expectedResult = "Vehicles repaired: 3";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsFalse(car.IsDamaged);
            Assert.IsFalse(van.IsDamaged);
            Assert.IsFalse(truck.IsDamaged);
        }
    }
}