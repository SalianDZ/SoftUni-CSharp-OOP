using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeGettersTest()
        {
            Axe axe = new Axe(10, 10);
            Assert.AreEqual(10, axe.AttackPoints);
            Assert.AreEqual(10, axe.DurabilityPoints);
        }

        [Test]
        public void TestIfWeaponLosesDurabilityAfterAHit()
        {
            Axe axe = new(10, 10);
            Dummy target = new Dummy(10, 10);
            axe.Attack(target);

            Assert.AreEqual(9, axe.DurabilityPoints, "Axe Durability doesn't change after attack");
        }

        [Test]
        public void TestIfABrokenWeaponCanAttack()
        { 
            Axe axe = new(10, 0);
            Dummy target = new Dummy(10, 10);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(target));
        }


    }
}