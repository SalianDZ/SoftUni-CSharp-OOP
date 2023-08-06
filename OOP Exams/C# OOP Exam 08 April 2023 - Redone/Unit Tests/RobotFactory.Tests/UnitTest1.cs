using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SupplementConstructorTest()
        {
            Supplement supplement = new("Suppy", 155);
            Assert.IsNotNull(supplement);
            Assert.AreEqual(supplement.Name, "Suppy");
            Assert.AreEqual(supplement.InterfaceStandard, 155);
            Assert.AreEqual(supplement.ToString(), "Supplement: Suppy IS: 155");
        }

        [Test]
        public void RobotConstructorTest()
        {
            Robot robot = new("TeslaRobot", 1000, 123);
            Assert.IsNotNull(robot);
            Assert.AreEqual(robot.Model, "TeslaRobot");
            Assert.AreEqual(robot.Price, 1000);
            Assert.AreEqual(robot.InterfaceStandard, 123);
            Assert.AreEqual(robot.Supplements.Count, 0);
            Assert.AreEqual(robot.Supplements, new List<Supplement>());
            Assert.AreEqual(robot.ToString(), "Robot model: TeslaRobot IS: 123, Price: 1000.00");
        }

        [Test]
        public void FactoryConstructorTest()
        {
            Factory factory = new("Tesla", 10);
            Assert.IsNotNull(factory);
            Assert.AreEqual(factory.Name, "Tesla");
            Assert.AreEqual(factory.Capacity, 10);
            Assert.AreEqual(factory.Supplements.Count, 0);
            Assert.AreEqual(factory.Supplements, new List<Supplement>());
            Assert.AreEqual(factory.Robots, new List<Robot>());
            Assert.AreEqual(factory.Robots.Count , 0);
        }

        [Test]
        public void FactoryProduceRobotMethodPositiveCase()
        {
            Factory factory = new("Tesla", 10);
            string result = factory.ProduceRobot("TeslaRobot", 1000, 123);
            Assert.AreEqual(factory.Robots.Count, 1);
            Assert.AreEqual(factory.Robots[0].Model, "TeslaRobot");
            Assert.AreEqual(factory.Robots[0].Price, 1000);
            Assert.AreEqual(factory.Robots[0].InterfaceStandard, 123);
            Assert.AreEqual(factory.Robots[0].Supplements.Capacity, 0);
            Assert.AreEqual(result, "Produced --> Robot model: TeslaRobot IS: 123, Price: 1000.00");
        }

        [Test]
        public void FactoryProduceRobotMethodNegativeCase()
        {
            Factory factory = new("Tesla", 2);
            factory.ProduceRobot("TeslaRobot", 1000, 123);
            factory.ProduceRobot("TeslaRobot2", 1200, 124);
            string result = factory.ProduceRobot("TeslaRobot3", 1300, 125);
            Assert.AreEqual(factory.Robots.Count, 2);
            Assert.AreEqual(result, "The factory is unable to produce more robots for this production day!");
        }

        [Test]
        public void FactoryProduceSupplementMethodTest()
        {
            Factory factory = new("Tesla", 2);
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = factory.Supplements.Count;
            string result1 = factory.ProduceSupplement("RoboCop", 88);
            string result2 = factory.ProduceSupplement("RoboFop", 89);
            int expectedCountAfterAdding = 2;
            int actualCountAfterAdding = factory.Supplements.Count;
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(expectedCountAfterAdding, actualCountAfterAdding);
            Assert.AreEqual(result1, "Supplement: RoboCop IS: 88");
            Assert.AreEqual(result2, "Supplement: RoboFop IS: 89");
        }

        [Test]
        public void FactoryUpgradeRobotMethodPositiveCaseTest()
        {
            Factory factory = new("Tesla", 2);
            Robot robot = new("TeslaRobot", 1000, 123);
            Supplement supplement = new("RoboCop", 123);
            int expectedRobotSupplementsCountBeforeUpgrade = 0;
            int actualRobotSupplementsCountBeforeUpgrade = robot.Supplements.Count;
            bool result = factory.UpgradeRobot(robot, supplement);
            int expectedRobotSupplementsCountAfterUpgrade = 1;
            int actualRobotSupplementsCountAfterUpgrade = robot.Supplements.Count;
            Assert.AreEqual(expectedRobotSupplementsCountBeforeUpgrade, actualRobotSupplementsCountBeforeUpgrade);
            Assert.AreEqual(expectedRobotSupplementsCountAfterUpgrade, actualRobotSupplementsCountAfterUpgrade);
            Assert.AreEqual(robot.Supplements[0], supplement);
            Assert.IsTrue(result);
        }

        [Test]
        public void FactoryUpgradeRobotMethodNegativeCaseTestIfStandardsAreNotTheSame()
        {
            Factory factory = new("Tesla", 2);
            Robot robot = new("TeslaRobot", 1000, 123);
            Supplement supplement = new("RoboCop", 88);
            int expectedRobotSupplementsCountBeforeUpgrade = 0;
            int actualRobotSupplementsCountBeforeUpgrade = robot.Supplements.Count;
            bool result = factory.UpgradeRobot(robot, supplement);
            int expectedRobotSupplementsCountAfterUpgrade = 0;
            int actualRobotSupplementsCountAfterUpgrade = robot.Supplements.Count;
            Assert.AreEqual(expectedRobotSupplementsCountBeforeUpgrade, actualRobotSupplementsCountBeforeUpgrade);
            Assert.AreEqual(expectedRobotSupplementsCountAfterUpgrade, actualRobotSupplementsCountAfterUpgrade);
            Assert.IsFalse(result);
        }

        [Test]
        public void FactoryUpgradeRobotMethodNegativeCaseTestIfStandardIsAlreadyAdded()
        {
            Factory factory = new("Tesla", 2);
            Robot robot = new("TeslaRobot", 1000, 123);
            Supplement supplement = new("RoboCop", 123);
            factory.UpgradeRobot(robot, supplement);
            int expectedRobotSupplementsCountBeforeUpgrade = 1;
            int actualRobotSupplementsCountBeforeUpgrade = robot.Supplements.Count;
            bool result = factory.UpgradeRobot(robot, supplement);
            int expectedRobotSupplementsCountAfterUpgrade = 1;
            int actualRobotSupplementsCountAfterUpgrade = robot.Supplements.Count;
            Assert.AreEqual(expectedRobotSupplementsCountBeforeUpgrade, actualRobotSupplementsCountBeforeUpgrade);
            Assert.AreEqual(expectedRobotSupplementsCountAfterUpgrade, actualRobotSupplementsCountAfterUpgrade);
            Assert.IsFalse(result);
        }

        [Test]
        public void FactorySellRobotMethodPositiveCase()
        {
            Factory factory = new("Tesla", 2);
            factory.ProduceRobot("TeslaRobot1", 1000, 123);
            factory.ProduceRobot("TeslaRobot2", 1200, 124);
            factory.ProduceRobot("TeslaRobot3", 1500, 125);
            Robot wantedRobot = factory.SellRobot(1300);
            Assert.IsNotNull(wantedRobot);
            Assert.AreEqual(wantedRobot.Price, 1200);
            Assert.AreEqual(wantedRobot.Supplements.Count, 0);
            Assert.AreEqual(wantedRobot.Model, "TeslaRobot2");
            Assert.AreEqual(wantedRobot.InterfaceStandard, 124);
            Assert.AreEqual(wantedRobot.ToString(), "Robot model: TeslaRobot2 IS: 124, Price: 1200.00");
        }

        [Test]
        public void FactorySellRobotMethodNegativeCase()
        {
            Factory factory = new("Tesla", 2);
            factory.ProduceRobot("TeslaRobot1", 1000, 123);
            factory.ProduceRobot("TeslaRobot2", 1200, 124);
            factory.ProduceRobot("TeslaRobot3", 1500, 125);
            Robot wantedRobot = factory.SellRobot(200);
            Assert.IsNull(wantedRobot);
        }
    }
}