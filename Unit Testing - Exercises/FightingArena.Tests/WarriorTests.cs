namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;
        [SetUp]
        public void SetUp()
        {
            warrior = new("Pesho", 50, 100);
        }

        [TearDown]
        public void TearDown()
        {
            warrior = null;
        }

        [Test]
        public void WarriorConstructorTest()
        {
            Assert.AreEqual("Pesho", warrior.Name);
            Assert.AreEqual(50, warrior.Damage);
            Assert.AreEqual(100, warrior.HP);
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void TestIfNameThrowsAnExceptionWhenNameIsNullOrEmptyOrWhitespace(string name)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, 50, 100));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-6)]
        public void TestIfDamageThrowsAnExceptionWhenInputIsZeroOrNegative(int damage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior("Pesho", damage, 100));
        }

        [Test]
        public void TestIfHPThrowsAnExceptionWhenInputIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Warrior("Pesho", 50, -10));
        }

        [Test]
        public void TestIfAttackMethodActuallyAttacksOtherWarriorWhenWeDontKillEnemy()
        {
            Warrior enemy = new("Gosho", 20, 200);
            warrior.Attack(enemy);

            Assert.AreEqual(80, warrior.HP);
            Assert.AreEqual(150, enemy.HP);
        }

        [Test]
        public void TestIfAttackMethodActuallyAttacksOtherWarriorWhenWeKillEnemy()
        {
            Warrior enemy = new("Gosho", 20, 40);
            warrior.Attack(enemy);

            Assert.AreEqual(80, warrior.HP);
            Assert.AreEqual(0, enemy.HP);
        }


        [Test]
        [TestCase(30)]
        [TestCase(20)]
        public void TestIfAttackMethodThrowsAnExceptionWhenOurHPIsLessThanOrEqualTo30(int hp)
        {
            Warrior warrior = new("Pesheto", 20, hp);
            Warrior enemy = new("Gosheto", 20, 109);
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(enemy));
        }

        [Test]
        [TestCase(30)]
        [TestCase(20)]
        public void TestIfAttackMethodThrowsAnExceptionWhenEnemyHPIsLessThanOrEqualTo30(int hp)
        {
            Warrior warrior = new("Pesho", 20, 100);
            Warrior enemy = new("Gosho", 20, hp);
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(enemy));
        }


        [Test]
        public void TestIfWarriorTriesToAttackAStrongerEnemy()
        {
            Warrior enemy = new("Gosho", 150, 100);
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(enemy));
        }
    }
}