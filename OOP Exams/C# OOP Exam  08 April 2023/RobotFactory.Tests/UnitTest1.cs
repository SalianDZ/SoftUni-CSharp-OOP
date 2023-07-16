using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace RobotFactory.Tests
{
    public class Tests
    {
        Factory factory;

        [SetUp]
        public void Setup()
        {
            factory = new("Tesla", 10);
        }

        [TearDown]
        public void TearDown()
        {
            factory = null;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual("Tesla", factory.Name);
            Assert.AreEqual(10, factory.Capacity);
            Assert.AreEqual(0, factory.Robots.Count);
            Assert.AreEqual(0, factory.Supplements.Count);
        }

        [Test]
        public void TestIfProduceRobotMethodReturnsMessageAboutFullCapacity()
        {
            factory = new("Audi", 2);
            factory.ProduceRobot("A7", 2500, 12);
            factory.ProduceRobot("A9", 2700, 14);
            string result = factory.ProduceRobot("A8", 2200, 13);
            Assert.AreEqual(result, "The factory is unable to produce more robots for this production day!");
        }

        [Test]
        public void TestIfProduceRobotMethodActuallyAddsARobot()
        {
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = factory.Robots.Count;
            string firstResult = factory.ProduceRobot("A7", 2500, 12);
            string secondResult = factory.ProduceRobot("A9", 2700, 14);
            Assert.AreEqual(firstResult, "Produced --> Robot model: A7 IS: 12, Price: 2500.00");
            Assert.AreEqual(secondResult, "Produced --> Robot model: A9 IS: 14, Price: 2700.00");
            Assert.AreEqual(factory.Robots.Count, 2);
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
        }

        [Test]
        public void TestIfProduceSupplementMethodActuallyAddsASupplement()
        {
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = factory.Supplements.Count;
            string result = factory.ProduceSupplement("Coca Cola", 10);
            Assert.AreEqual(1, factory.Supplements.Count);
            Assert.AreEqual("Coca Cola", factory.Supplements[0].Name);
            Assert.AreEqual(10, factory.Supplements[0].InterfaceStandard);
            Assert.AreEqual(result, "Supplement: Coca Cola IS: 10");
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
        }

        [Test]
        public void TestIfUpgradeRobotMethodActuallyAddSupplementToItsSupplements()
        {
            factory.ProduceRobot("A7", 2500, 10);
            factory.ProduceSupplement("Coca Cola", 10);
            bool result = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIfUpgradeRobotMethodReturnsTheCorrectBooleanWhenThereIsAlreadySuchASupplement()
        {
            factory.ProduceRobot("A7", 2500, 10);
            factory.ProduceSupplement("Coca Cola", 10);
            factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            bool result = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIfUpgradeRobotMethodReturnsTheCorrectBooleanWhenTheInterfaceIsNotTheSame()
        {
            factory.ProduceRobot("A7", 2500, 10);
            factory.ProduceSupplement("Coca Cola", 12);
            bool result = factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIfSellRobotMethodReturnsNullWhenThereIsNoRobotWithSuchAPrice()
        {
            Robot robot = factory.SellRobot(2000);
            Assert.AreEqual(robot, null);
        }

        [Test]
        public void SellRobot_Successful()
        {
            Factory factory = new Factory("SpaceX", 10);

            factory.ProduceRobot("Robo-3", 2000, 22);
            factory.ProduceRobot("Robo-3", 2500, 22);
            factory.ProduceRobot("Robo-3", 30000, 22);

            Robot robot = factory.Robots.FirstOrDefault(r => r.Price == 2000);

            var robotSold = factory.SellRobot(2499);

            Assert.AreSame(robot, robotSold);

        }

        [Test]
        public void RobotsGetterTest()
        {
            factory.ProduceRobot("A7", 2500, 12);
            factory.ProduceRobot("A9", 2700, 14);

            List<Robot> currentRobots = factory.Robots;
            CollectionAssert.AreEqual(currentRobots, factory.Robots);
            Assert.AreEqual(factory.Robots.Count, 2);
        }

        [Test]
        public void SupplementsGetterTest()
        {
            factory.ProduceSupplement("Coca Cola", 10);
            factory.ProduceSupplement("Pepsi", 11);

            List<Supplement> currentSupplements = factory.Supplements;
            CollectionAssert.AreEqual(currentSupplements, factory.Supplements);
            Assert.AreEqual(factory.Supplements.Count, 2);
        }
    }
}