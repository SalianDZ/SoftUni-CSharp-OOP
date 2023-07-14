namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        Car car;
        [SetUp]
        public void SetUp()
        {
            car = new("Volkswagen", "Golf", 6, 50);
        }

        [TearDown]
        public void TearDown()
        {
            car = null;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual("Volkswagen", car.Make);
            Assert.AreEqual("Golf", car.Model);
            Assert.AreEqual(6, car.FuelConsumption);
            Assert.AreEqual(50, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void TestIfMakeThrowsAnExceptionWhenInputIsNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, "Gold", 6, 50));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void TestIfModelThrowsAnExceptionWhenInputIsNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car("Volkswagen", model, 6, 50));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfFuelConsumptionThrowsAnExceptionWhenInputIsNullOrEmpty(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car("Volkswagen", "Golf", fuelConsumption, 50));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfFuelCapacityThrowsAnExceptionWhenInputIsNullOrEmpty(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car("Volkswagen", "Golf", 6, fuelCapacity));
        }

        [Test]
        public void TestIfRefuelMethodActuallyRefuelsACar()
        {
            car.Refuel(30);

            Assert.AreEqual(30, car.FuelAmount);
        }


        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfRefuelMethodThrowsExceptionWhenTheGivenAmountIsZeroOrNegative(double litres)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(litres));
        }

        [Test]
        public void TestIfRefuelMethodDoesNotAcceptMoreFuelThanTheAllowedQuantity()
        {
            car.Refuel(55);
            Assert.AreEqual(50, car.FuelAmount);
        }

        [Test]
        public void TestIfDriveMethodThrowsExceptionWhenTheFuelAmountIsNotEnough()
        {
            Assert.Throws<InvalidOperationException>(() => car.Drive(10));
        }

        [Test]
        public void TestIfCarHasEnoughFuelToMakeADrive()
        {
            car.Refuel(10);
            car.Drive(100);
            Assert.AreEqual(4, car.FuelAmount);
        }
    }
}