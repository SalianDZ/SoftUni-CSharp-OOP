namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
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
        public void TestIfFetchMethodReturnCollection()
        {
            database = new(1, 2 ,3);
            int[] result = database.Fetch();
            Assert.That(new int[] { 1, 2, 3}, Is.EquivalentTo(result));
        }

        [Test]
        public void TestIfAddMethodActuallyAddAnElement()
        {
            database.Add(5);
            Assert.AreEqual(1, database.Count);
        }

        [Test]
        public void TestIfAddMethodThrowsException()
        {
            database = new(1, 2, 3, 4, 5, 6, 7, 8 ,9, 10, 11, 12, 13, 14, 15, 16);
            Assert.Throws<InvalidOperationException>(() => database.Add(17));
        }

        [Test]
        public void TestIfRemoveMethodActuallyRemovesAnElement()
        {
            database = new(1, 2, 3);
            database.Remove();
            Assert.AreEqual(2, database.Count);
        }

        [Test]
        public void TestIfRemoveMethodThrowsExceptionWhenCollectionIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
    }
}
