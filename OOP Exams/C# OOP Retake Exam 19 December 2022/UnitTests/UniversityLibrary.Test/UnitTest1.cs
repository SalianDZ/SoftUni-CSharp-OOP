namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TextBookConstructorTest()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno","Salian","Adventure");
            Assert.IsNotNull(textBook);
            Assert.AreEqual(textBook.Title, "KnigaNomerEdno");
            Assert.AreEqual(textBook.Author, "Salian");
            Assert.AreEqual(textBook.Category, "Adventure");

        }

        [Test]
        public void UniversityLibraryConstructorTest()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno", "Salian", "Adventure");
            universityLibrary.AddTextBookToLibrary(textBook);
            int expectedCount = 1;
            Assert.IsNotNull(universityLibrary);
            Assert.AreEqual(universityLibrary.Catalogue[0], textBook);
            Assert.AreEqual(expectedCount, universityLibrary.Catalogue.Count);
        }

        [Test]
        public void TestIfAddMethodWorks()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno", "Salian", "Adventure");
            int expectedCountBeforeAdding = 0;
            int actualCountBeforeAdding = universityLibrary.Catalogue.Count;
            string result = universityLibrary.AddTextBookToLibrary(textBook);
            int expectedCountAfterAdding = 1;
            Assert.AreEqual(universityLibrary.Catalogue[0], textBook);
            Assert.AreEqual(expectedCountAfterAdding, universityLibrary.Catalogue.Count);
            Assert.AreEqual(expectedCountBeforeAdding, actualCountBeforeAdding);
            Assert.AreEqual(result, textBook.ToString());
        }

        [Test]
        public void TestIfLoanMethodThrowsExceptionWhenThereIsNoSuchBook()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            Assert.Throws<NullReferenceException>(() => universityLibrary.LoanTextBook(1, "Musty"));
        }

        [Test]
        public void TestIfLoanMethodReturnsTheCorrectMessageWhenAPersonLoansABook()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno", "Salian", "Adventure");
            universityLibrary.AddTextBookToLibrary(textBook);

            string expectedLastHolder = null;
            string actualLastHolder = textBook.Holder;
            string result = universityLibrary.LoanTextBook(1, "Musty");
            string expectedNewHolder = "Musty";
            string actualNewHolder = textBook.Holder;

            Assert.AreEqual(result, "KnigaNomerEdno loaned to Musty.");
            Assert.AreEqual(expectedLastHolder, actualLastHolder);
            Assert.AreEqual(expectedNewHolder, actualNewHolder);
        }

        [Test]
        public void TestIfLoanMethodDoNotAllowToGetABookWhenItIsNotReturned()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno", "Salian", "Adventure");
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.LoanTextBook(1, "Musty");
            string result = universityLibrary.LoanTextBook(1, "Musty");

            Assert.AreEqual(textBook.Holder, "Musty");
            Assert.AreEqual(result, "Musty still hasn't returned KnigaNomerEdno!");
        }

        [Test]
        public void TestIfReturnMethodThrowsExceptionWhenThereIsNoSuchBook()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            Assert.Throws<NullReferenceException>(() => universityLibrary.ReturnTextBook(1));
        }

        [Test]
        public void TestIfReturnMethodRemovesHolderName()
        {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook textBook = new TextBook("KnigaNomerEdno", "Salian", "Adventure");
            universityLibrary.AddTextBookToLibrary(textBook);
            universityLibrary.LoanTextBook(1, "Musty");
            string expectedLastHolder = "Musty";
            string actualLastHolder = textBook.Holder;
            string result = universityLibrary.ReturnTextBook(1);
            string expectedNewHolder = string.Empty;
            string actualNewHolder = textBook.Holder;
            Assert.AreEqual(expectedLastHolder, actualLastHolder);
            Assert.AreEqual(expectedNewHolder, actualNewHolder);
            Assert.AreEqual(result, "KnigaNomerEdno is returned to the library.");
        }
    }
}