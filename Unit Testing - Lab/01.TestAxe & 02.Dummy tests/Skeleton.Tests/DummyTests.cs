using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void TestIfDummyLosesHealthWhenAttacked()
        {
            Dummy target = new(10, 10);
            target.TakeAttack(5);

            Assert.AreEqual(target.Health, 5, "Health doesn't decrease when it is attacked!");
        }

        [Test]
        public void TestIfDeadDummyThrowsExceptionIfAttacked()
        {
            Dummy target = new(0, 10);
            Axe axe = new(5, 10);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(target));
        }

        [Test]
        public void TestIfAliveDummyCantGiveXP()
        {
            Dummy target = new(10, 10);
            Assert.Throws<InvalidOperationException>(() => target.GiveExperience());
        }

        [Test]
        public void TestIfDeadDummyCanGiveXP()
        {
            Dummy target = new(0, 10);

            int experience = target.GiveExperience();
            Assert.AreEqual(experience, target.GiveExperience(), "Dead dummy does not give experience!");
        }
    }
}