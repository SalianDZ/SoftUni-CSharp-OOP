using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;

namespace VendingRetail.Tests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CoffeeMatConstructor()
        {
            CoffeeMat coffeeMat = new(6, 3);
            Assert.IsNotNull(coffeeMat);
            Assert.AreEqual(coffeeMat.ButtonsCount, 3);
            Assert.AreEqual(coffeeMat.WaterCapacity, 6);
            Assert.AreEqual(coffeeMat.Income, 0);
            Type type = typeof(CoffeeMat);
            FieldInfo field = type.GetField("drinks", BindingFlags.NonPublic | BindingFlags.Instance);
            Dictionary<string, double> drinks = (Dictionary<string, double>)field.GetValue(coffeeMat);
            FieldInfo field2 = type.GetField("waterTankLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int waterTankLevel = (int) field2.GetValue(coffeeMat);
            Assert.AreEqual(drinks.Count, 0);
            Assert.AreEqual(waterTankLevel, 0);
        }

        [Test]
        public void FillWaterTankMethodPositiveTest()
        {
            CoffeeMat coffeeMat = new(6, 3);
            string result = coffeeMat.FillWaterTank();
            Type type = typeof(CoffeeMat);
            FieldInfo field2 = type.GetField("waterTankLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int waterTankLevel = (int)field2.GetValue(coffeeMat);
            Assert.AreEqual(result, "Water tank is filled with 6ml");
            Assert.AreEqual(waterTankLevel, 6);
        }

        [Test]
        public void FillWaterTankMethodNegativeTest()
        {
            CoffeeMat coffeeMat = new(6, 3);
            coffeeMat.FillWaterTank();
            string result = coffeeMat.FillWaterTank();
            Assert.AreEqual(result, "Water tank is already full!");
        }

        [Test]
        public void AddDrinkTankMethodPositiveTest()
        {
            CoffeeMat coffeeMat = new(6, 3);
            coffeeMat.AddDrink("Cola", 1.70);
            bool result = coffeeMat.AddDrink("Pepsi", 2.50);
            Type type = typeof(CoffeeMat);
            FieldInfo field = type.GetField("drinks", BindingFlags.NonPublic | BindingFlags.Instance);
            Dictionary<string, double> drinks = (Dictionary<string, double>)field.GetValue(coffeeMat);
            Assert.AreEqual(drinks.Count, 2);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddDrinkTankMethodNegativeCaseWhenCapacityIsFullTest()
        {
            CoffeeMat coffeeMat = new(6, 3);
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            coffeeMat.AddDrink("Monster", 1.80);
            bool result = coffeeMat.AddDrink("Pepsi", 2.50);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddDrinkTankMethodNegativeCaseWhenThereIsAlreadySuchANameTest()
        {
            CoffeeMat coffeeMat = new(6, 3);
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            bool result = coffeeMat.AddDrink("Cola", 2.50);
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrinkMethodPositiveTest()
        {
            CoffeeMat coffeeMat = new(100, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            string result = coffeeMat.BuyDrink("Cola");
            Assert.AreEqual(result, "Your bill is 1.70$");
            Assert.AreEqual(coffeeMat.Income, 1.70);
            Type type = typeof(CoffeeMat);
            FieldInfo field2 = type.GetField("waterTankLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int waterTankLevel = (int)field2.GetValue(coffeeMat);
            Assert.AreEqual(waterTankLevel, 20);
        }

        [Test]
        public void BuyDrinkMethodNegativeCaseWhenWaterTankLevelIsUnder80Test()
        {
            CoffeeMat coffeeMat = new(79, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            string result = coffeeMat.BuyDrink("Cola");
            Assert.AreEqual(result, "CoffeeMat is out of water!");
            Assert.AreEqual(coffeeMat.Income, 0);
            Type type = typeof(CoffeeMat);
            FieldInfo field2 = type.GetField("waterTankLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int waterTankLevel = (int)field2.GetValue(coffeeMat);
            Assert.AreEqual(waterTankLevel, 79);
        }

        [Test]
        public void BuyDrinkMethodNegativeCaseWhenThereIsNoSuchADrinkTest()
        {
            CoffeeMat coffeeMat = new(100, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            string result = coffeeMat.BuyDrink("Pepsi");
            Assert.AreEqual(result, "Pepsi is not available!");
            Assert.AreEqual(coffeeMat.Income, 0);
            Type type = typeof(CoffeeMat);
            FieldInfo field2 = type.GetField("waterTankLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int waterTankLevel = (int)field2.GetValue(coffeeMat);
            Assert.AreEqual(waterTankLevel, 100);
        }

        [Test]
        public void CollectIncomeTest()
        {
            CoffeeMat coffeeMat = new(200, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Cola", 1.70);
            coffeeMat.AddDrink("Sprite", 1.80);
            coffeeMat.AddDrink("Monster", 3.00);
            coffeeMat.BuyDrink("Cola");
            coffeeMat.BuyDrink("Sprite");
            double expectedIncomeBeforeCollection = 3.50;
            double actualIncomeBeforeCollection = coffeeMat.Income;
            double result = coffeeMat.CollectIncome();
            double expectedIncomeAfterCollection = 0;
            double actualIncomeAfterCollection = coffeeMat.Income;
            Assert.AreEqual(result, 3.50);
            Assert.AreEqual(expectedIncomeBeforeCollection, actualIncomeBeforeCollection);
            Assert.AreEqual(expectedIncomeAfterCollection, actualIncomeAfterCollection);
        }
    }
}