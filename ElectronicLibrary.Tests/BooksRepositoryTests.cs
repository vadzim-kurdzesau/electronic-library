using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.TestData;
using ElectronicLibrary.Tests.Comparators;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class BooksRepositoryTests
    {
        private readonly ElectronicLibraryService _library;

        public BooksRepositoryTests()
        {
            ReseedReadersIdentifiers(ConfigurationManager.ConnectionString);
            this._library = TestElectronicLibrary.LibraryService;
        }

        private static void ReseedReadersIdentifiers(string connectionString)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            new SqlCommand("DBCC CHECKIDENT ('ElectronicLibrary.dbo.books', RESEED, 0);", sqlConnection).ExecuteNonQuery();
        }

        [Order(0)]
        [TestCaseSource(typeof(Books), nameof(Books.GetList))]
        public void BooksRepositoryTests_InsertBook(Book book)
        {
            this._library.InsertBook(book);
            Assert.Pass();
        }

        [Order(1)]
        [Test]
        public void BooksRepositoryTests_InsertInventoryNumber()
        {
            for (int i = 1; i <= Books.GetList().Count(); i++)
            {
                this._library.InsertInventoryNumber(new InventoryNumber()
                {
                    Id = i,
                    BookId = i,
                    Number = i.ToString()
                });
            }

            Assert.Pass();
        }

        [Order(1)]
        [Test]
        public void BooksRepositoryTests_FindBookByName()
        {
            foreach (var testData in Books.GetList())
            {
                var expected = testData.Arguments[0] as Book;
                var actual = this._library.GetBookByName(expected.Name).FirstOrDefault();

                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Order(1)]
        [Test]
        public void BooksRepositoryTests_FindBookByAuthor()
        {
            foreach (var testData in Books.GetList())
            {
                var expected = testData.Arguments[0] as Book;
                var actual = this._library.GetBookByAuthor(expected.Author).FirstOrDefault();

                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Order(1)]
        [Test]
        public void BooksRepositoryTests_GetBook()
        {
            foreach (var testData in Books.GetList())
            {
                var expected = testData.Arguments[0] as Book;
                var actual = this._library.GetBook(expected.Id);

                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Order(1)]
        [Test]
        public void BooksRepositoryTests_GetInventoryNumbers()
        {
            foreach (var testData in Books.GetList())
            {
                var expected = testData.Arguments[0] as Book;
                var actual = this._library.GetInventoryNumber(expected).First();

                Assert.AreEqual(expected.Id, actual.BookId);
            }
        }

        [Order(2)]
        [Test]
        public void BooksRepositoryTests_GetAllBooks()
        {
            int index = 0;
            var expected = GetExpectedBooks().ToArray();
            foreach (var actual in this._library.GetAllBooks())
            {
                Assert.IsTrue(new BookComparator().Equals(expected[index], actual));
                index++;
            }
        }

        [Order(3)]
        [Test]
        public void BooksRepositoryTests_UpdateBook()
        {
            var expected = Books.GetList().First().Arguments[0] as Book;
            expected.Author = "Jane Austen";

            this._library.UpdateBook(expected);
            Assert.IsTrue(new BookComparator().Equals(expected, this._library.GetBookByAuthor(expected.Author).First()));
        }

        [Order(4)]
        [Test]
        public void BooksRepositoryTests_DeleteBook()
        {
            for (int i = 1; i <= Books.GetList().Count(); i++)
            {
                this._library.DeleteBook(i);
            }

            Assert.IsEmpty(_library.GetAllBooks());
        }

        private static IEnumerable<Book> GetExpectedBooks()
        {
            List<Book> expected = new List<Book>();
            foreach (var testData in Books.GetList().ToArray())
            {
                expected.Add(testData.Arguments[0] as Book);
            }

            return expected;
        }
    }
}
