namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class ArenaTests
    {
        Arena arena;
        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [TearDown]
        public void TearDown()
        {
            arena = null;

        }
        [Test]
        public void ArenaConstructorTest()
        {
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void TestIfWarriorGetterWorks()
        {
            Warrior warrior = new("Gosho", 50, 150);
            arena.Enroll(warrior);
            List<Warrior> warriors = new List<Warrior>();
            warriors.Add(warrior);
            Assert.That(warriors, Is.EquivalentTo(arena.Warriors));
        }

        [Test]
        public void TestIfEnrollMethodWorksProperly()
        {
            Warrior warrior = new("Gosho", 50, 150);
            arena.Enroll(warrior);

            Assert.AreEqual(arena.Count, 1);
        }

        [Test]
        public void TestIfEnrollMethodThrowsAnExceptionIfThereIsAlreadyAWarriorWithThisName()
        {
            Warrior warrior = new("Gosho", 50, 150);
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Gosho", 40, 120)));
        }

        [Test]
        public void TestIfFightMethodThrowsAnExceptionIfAttackerOrDefenderNameIsNull()
        {
            Warrior attacker = new("Pesho", 30, 100);
            arena.Enroll(attacker);
            Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker.Name, "Gosho"));
        }

        [Test]
        public void TestIfFightMethodWorksProperly ()
        {
            Warrior attacker = new("Pesho", 30, 100);
            Warrior deffener = new("Gosho", 20, 150);
            arena.Enroll(attacker);
            arena.Enroll(deffener);
            arena.Fight(attacker.Name, deffener.Name);
            Assert.AreEqual(attacker.HP, 80);
            Assert.AreEqual(deffener.HP, 120);
        }
    }
}
