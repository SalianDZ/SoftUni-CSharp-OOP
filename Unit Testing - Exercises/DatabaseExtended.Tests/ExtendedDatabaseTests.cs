namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Reflection.Metadata.Ecma335;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;
        [SetUp]
        public void SetUp()
        {
            database = new();
        }

        [TearDown]
        public void TearDown()
        {
            database = null;
        }

        [Test]
        public void TestIfFindByIdMethodRetunsTheCorrectPerson()
        {
            database = new(new Person(12, "Pesho"));
            Person person = database.FindById(12);
            Assert.AreEqual(12, person.Id);
            Assert.AreEqual("Pesho", person.UserName);
        }

        [Test]
        public void TestIfFindByIdMethodThrowsExceptionWhenIdDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(12));
        }

        [Test]
        public void TestIfFindByIdMethodThrowsExceptionWhenIdIsNegativeNumber()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-15));
        }

        [Test]
        public void TestIfFindByUsernameMethodRetunsTheCorrectPerson()
        {
            database = new(new Person(12, "Pesho"));
            Person person = database.FindByUsername("Pesho");
            Assert.AreEqual(12, person.Id);
            Assert.AreEqual("Pesho", person.UserName);
        }

        [Test]
        public void TestIfFindByUsernameMethodThrowsExceptionWhenUsernameIsInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("Pesho"));
        }

        [Test]
        public void TestIfFindByUsernameMethodThrowsExceptionWhenUsernameIsNull()
        {
            string name = null;
            string secondName = string.Empty;
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(name));
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(secondName));
        }

        [Test]
        public void TestIfAddMethodActuallyAddAnElement()
        {

            database.Add(new Person(5, "Gosho"));
            Assert.AreEqual(1, database.Count);
        }

        [Test]
        public void TestIfAddMethodThrowsExceptionWhenExceedingTheLimit()
        {
            Person[] people = GeneratePeople();
            database = new(people);
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(17, "17")));
        }

        private Person[] GeneratePeople()
        {
            Person[] result = new Person[16];

            for (int i = 0; i < 16; i++)
            {
                result[i] = new Person(i, i.ToString());
            }

            return result;
        }

        [Test]
        public void TestIfAddMethodThrowsExceptionWhenWeAddAPersonWithARepetitiveUsername()
        {
            Person person = new(1, "Gosho");
            database.Add(person);
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(2, "Gosho")));
        }

        [Test]
        public void TestIfAddMethodThrowsExceptionWhenWeAddAPersonWithARepetitiveId()
        {
            Person person = new(5, "Gosho");
            database.Add(person);
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(5, "Pesho")));
        }

        [Test]
        public void TestIfRemoveMethodActuallyRemovesAnElement()
        {
            database = new(new Person(1, "Gosho"), new Person(2, "Pesho"));
            database.Remove();
            Assert.AreEqual(1, database.Count);
        }

        [Test]
        public void TestIfRemoveMethodThrowsExceptionWhenCollectionIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
    }
}