using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void WeaponConstructorTest()
            {
                Weapon weapon = new Weapon("Pistol", 200, 5);
                Assert.IsNotNull(weapon);
                Assert.AreEqual(weapon.Name, "Pistol");
                Assert.AreEqual(weapon.Price, 200);
                Assert.AreEqual(weapon.DestructionLevel, 5);
            }

            [Test]
            public void WeaponPriceNegativeCase()
            {
                Exception ex = Assert.Throws<ArgumentException>(() => new Weapon("Pistol", -20, 5));
                Assert.AreEqual(ex.Message, "Price cannot be negative.");
            }

            [Test]
            public void WeaponIncreaseDestructionLevelMethod()
            {
                Weapon weapon = new Weapon("Pistol", 200, 5);
                Assert.IsNotNull(weapon);
                int expectedLevelBeforeIncreasing = 5;
                int actualLevelBeforeIncreasing = weapon.DestructionLevel;
                weapon.IncreaseDestructionLevel();
                int expectedLevelAfterIncreasing = 6;
                int actualLevelAfterIncreasing = weapon.DestructionLevel;
                Assert.AreEqual(expectedLevelBeforeIncreasing, actualLevelBeforeIncreasing);
                Assert.AreEqual(expectedLevelAfterIncreasing, actualLevelAfterIncreasing);
            }

            [Test]
            [TestCase(10)]
            [TestCase(15)]
            public void TestIfWeaponIsNuclearPositiveCase(int value)
            {
                Weapon weapon = new Weapon("Pistol", 200, value);
                Assert.IsTrue(weapon.IsNuclear);
            }

            [Test]
            public void TestIfWeaponIsNuclearNegativeCase()
            {
                Weapon weapon = new Weapon("Pistol", 200, 5);
                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]
            public void PlanetConstructorTest()
            {
                Planet planet = new Planet("Mars", 200);
                Assert.IsNotNull(planet);
                Assert.AreEqual(planet.Name, "Mars");
                Assert.AreEqual(planet.Budget, 200);
                Assert.AreEqual(planet.Weapons, new List<Weapon>());
                Assert.AreEqual(planet.Weapons.Count, 0);
                Assert.AreEqual(planet.MilitaryPowerRatio, 0);
            }

            [Test]
            [TestCase(null)]
            [TestCase("")]
            public void PlanetNameNegativeCase(string value)
            {
                Exception ex = Assert.Throws<ArgumentException>(() => new Planet(value, 200));
                Assert.AreEqual(ex.Message, "Invalid planet Name");
            }

            [Test]
            public void PlanetBudgetNegativeCase()
            {
                Exception ex = Assert.Throws<ArgumentException>(() => new Planet("Mars", -100));
                Assert.AreEqual(ex.Message, "Budget cannot drop below Zero!");
            }

            [Test]
            public void PlanetAddWeaponPositiveTest()
            {
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Weapon weapon2 = new Weapon("AutoPistol", 300, 6);
                Planet planet = new Planet("Mars", 200);
                int expectedWeaponsCountBeforeAdding = 0;
                int actualWeaponsCountBeforeAdding = planet.Weapons.Count;
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                int expectedWeaponsCountAfterAdding = 2;
                int actualWeaponsCountAfterAdding = planet.Weapons.Count;
                Assert.AreEqual(expectedWeaponsCountBeforeAdding, actualWeaponsCountBeforeAdding);
                Assert.AreEqual(expectedWeaponsCountAfterAdding, actualWeaponsCountAfterAdding);
                Assert.AreEqual(planet.Weapons.First(), weapon1);
                Assert.AreEqual(planet.Weapons.Last(), weapon2);
            }

            [Test]
            public void PlanetAddWeaponNegativeTest()
            {
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Planet planet = new Planet("Mars", 200);
                int expectedWeaponsCountBeforeAdding = 0;
                int actualWeaponsCountBeforeAdding = planet.Weapons.Count;
                planet.AddWeapon(weapon1);
                Exception ex = Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon1));
                int expectedWeaponsCountAfterAdding = 1;
                int actualWeaponsCountAfterAdding = planet.Weapons.Count;
                Assert.AreEqual(expectedWeaponsCountBeforeAdding, actualWeaponsCountBeforeAdding);
                Assert.AreEqual(expectedWeaponsCountAfterAdding, actualWeaponsCountAfterAdding);
                Assert.AreEqual(planet.Weapons.First(), weapon1);
                Assert.AreEqual(ex.Message, "There is already a Pistol weapon.");
            }

            [Test]
            public void PlanetMilitaryPowerRatioTest()
            {
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Weapon weapon2 = new Weapon("AutoPistol", 300, 6);
                Planet planet = new Planet("Mars", 200);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                Assert.AreEqual(planet.MilitaryPowerRatio, 9);
            }

            [Test]
            public void PlanetProfitMethodTest()
            {
                Planet planet = new Planet("Mars", 200);
                double expectedPlanetBudgetBeforeProfit = 200;
                double actualPlanetBudgetBeforeProfit = planet.Budget;
                planet.Profit(50);
                double expectedPlanetBudgetAfterProfit = 250;
                double actualPlanetBudgetAfterProfit = planet.Budget;
                Assert.AreEqual(expectedPlanetBudgetBeforeProfit, actualPlanetBudgetBeforeProfit);
                Assert.AreEqual(expectedPlanetBudgetAfterProfit, actualPlanetBudgetAfterProfit);
            }

            [Test]
            public void PlanetSpendFundsMethodPositiveTest()
            {
                Planet planet = new Planet("Mars", 200);
                double expectedPlanetBudgetBeforeSpending = 200;
                double actualPlanetBudgetBeforeSpending = planet.Budget;
                planet.SpendFunds(50);
                double expectedPlanetBudgetAfterSpending = 150;
                double actualPlanetBudgetAfterSpending = planet.Budget;
                Assert.AreEqual(expectedPlanetBudgetBeforeSpending, actualPlanetBudgetBeforeSpending);
                Assert.AreEqual(expectedPlanetBudgetAfterSpending, actualPlanetBudgetAfterSpending);
            }

            [Test]
            public void PlanetSpendFundsMethodNegativeTest()
            {
                Planet planet = new Planet("Mars", 200);
                double expectedPlanetBudgetBeforeSpending = 200;
                double actualPlanetBudgetBeforeSpending = planet.Budget;
                Exception ex = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(250));
                double expectedPlanetBudgetAfterSpending = 200;
                double actualPlanetBudgetAfterSpending = planet.Budget;
                Assert.AreEqual(expectedPlanetBudgetBeforeSpending, actualPlanetBudgetBeforeSpending);
                Assert.AreEqual(expectedPlanetBudgetAfterSpending, actualPlanetBudgetAfterSpending);
                Assert.AreEqual(ex.Message, "Not enough funds to finalize the deal.");
            }

            [Test]
            public void PlanetRemoveWeaponPositiveTest()
            {
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Weapon weapon2 = new Weapon("AutoPistol", 300, 6);
                Planet planet = new Planet("Mars", 200);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                int expectedWeaponsCountBeforeRemoving = 2;
                int actualWeaponsCountBeforeRemoving = planet.Weapons.Count;
                planet.RemoveWeapon("Pistol");
                int expectedWeaponsCountAfterRemoving = 1;
                int actualWeaponsCountAfterRemoving = planet.Weapons.Count;
                Assert.AreEqual(expectedWeaponsCountBeforeRemoving, actualWeaponsCountBeforeRemoving);
                Assert.AreEqual(expectedWeaponsCountAfterRemoving, actualWeaponsCountAfterRemoving);
            }

            [Test]
            public void PlanetRemoveWeaponNegativeTest()
            {
                Weapon weapon = new Weapon("AutoPistol", 300, 6);
                Planet planet = new Planet("Mars", 200);
                planet.AddWeapon(weapon);
                int expectedWeaponsCountBeforeRemoving = 1;
                int actualWeaponsCountBeforeRemoving = planet.Weapons.Count;
                planet.RemoveWeapon("Pistol");
                int expectedWeaponsCountAfterRemoving = 1;
                int actualWeaponsCountAfterRemoving = planet.Weapons.Count;
                Assert.AreEqual(expectedWeaponsCountBeforeRemoving, actualWeaponsCountBeforeRemoving);
                Assert.AreEqual(expectedWeaponsCountAfterRemoving, actualWeaponsCountAfterRemoving);
            }

            [Test]
            public void PlanetUpgradeWeaponPositiveCase()
            {
                Weapon weapon = new Weapon("AutoPistol", 300, 6);
                Planet planet = new Planet("Mars", 200);
                planet.AddWeapon(weapon);
                int expectedWeaponLevelBeforeUpgrade = 6;
                int actualWeaponLevelBeforeUpgrade = weapon.DestructionLevel;
                planet.UpgradeWeapon("AutoPistol");
                int expectedWeaponLevelAfterUpgrade = 7;
                int actualWeaponLevelAfterUpgrade = weapon.DestructionLevel;
                Assert.AreEqual(expectedWeaponLevelBeforeUpgrade, actualWeaponLevelBeforeUpgrade);
                Assert.AreEqual(expectedWeaponLevelAfterUpgrade, actualWeaponLevelAfterUpgrade);
            }

            [Test]
            public void PlanetUpgradeWeaponNegativeCase()
            {
                Planet planet = new Planet("Mars", 200);
                Exception ex = Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("Pistol"));
                Assert.AreEqual(ex.Message, "Pistol does not exist in the weapon repository of Mars");
            }

            [Test]
            public void PlanetDestructOpponentPositiveCase()
            {
                Planet planet = new Planet("Mars", 200);
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Weapon weapon2 = new Weapon("AutoPistol", 300, 6);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                Planet planetOpponent = new Planet("Jupiter", 100);
                string result = planet.DestructOpponent(planetOpponent);
                Assert.AreEqual(result, "Jupiter is destructed!");
            }

            [Test]
            public void PlanetDestructOpponentNegativeCase()
            {
                Planet planet = new Planet("Mars", 200);
                Weapon weapon1 = new Weapon("Pistol", 150, 3);
                Weapon weapon2 = new Weapon("AutoPistol", 300, 6);
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                Planet planetOpponent = new Planet("Jupiter", 100);
                Exception ex = Assert.Throws<InvalidOperationException>(() => planetOpponent.DestructOpponent(planet));
                Assert.AreEqual(ex.Message, "Mars is too strong to declare war to!");
            }
        }
    }
}
