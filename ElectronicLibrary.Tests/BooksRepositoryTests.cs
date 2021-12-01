using System.Linq;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.TestData;
using ElectronicLibrary.Tests.Comparators;
using ElectronicLibrary.Tests.Extensions;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class BooksRepositoryTests : BaseTestElectronicLibrary
    {
        protected override void ClearTable()
            => this.ClearTable("books");

        public override void SetUp()
            => ReseedTableIdentifiers("books");
        
        [Test]
        public void BooksRepositoryTests_InsertBook()
        {
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                // Arrange
                this.Library.InsertBook(book);

                // Act
                var actual = GetElementFromTable<Book>(book.Id);

                // Assert
                Assert.IsTrue(new BookComparator().Equals(book, actual));
            }
        }

        [Test]
        public void BooksRepositoryTests_GetBookByName()
        {
            foreach (var expected in Books.GetList().ExtractData<Book>())
            {
                // Arrange
                this.Library.InsertBook(expected);

                // Act
                var actual = this.Library.GetBookByName(expected.Name).FirstOrDefault();

                // Assert
                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void BooksRepositoryTests_GetBookByAuthor()
        {
            foreach (var expected in Books.GetList().ExtractData<Book>())
            {
                // Arrange
                this.Library.InsertBook(expected);

                // Act
                var actual = this.Library.GetBookByAuthor(expected.Author).FirstOrDefault();

                // Assert
                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void BooksRepositoryTests_GetBook()
        {
            foreach (var expected in Books.GetList().ExtractData<Book>())
            {
                // Arrange
                this.Library.InsertBook(expected);

                // Act
                var actual = this.Library.GetBook(expected.Id);

                // Assert
                Assert.IsTrue(new BookComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void BooksRepositoryTests_GetAllBooks()
        {
            // Arrange
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                this.Library.InsertBook(book);
            }

            int index = 0;
            var expected = Books.GetList().ExtractData<Book>().ToArray();

            // Act
            foreach (var actual in this.Library.GetAllBooks())
            {
                // Assert
                Assert.IsTrue(new BookComparator().Equals(expected[index++], actual));
            }
        }

        [Test]
        public void BooksRepositoryTests_UpdateBook()
        {
            // Arrange
            var expected = Books.GetList().ExtractData<Book>().First();
            this.Library.InsertBook(expected);

            expected.Author = "Jane Austen";

            // Act
            this.Library.UpdateBook(expected);

            // Assert
            Assert.IsTrue(new BookComparator().Equals(expected, this.Library.GetBookByAuthor(expected.Author).FirstOrDefault()));
        }

        [Test]
        public void BooksRepositoryTests_DeleteBook()
        {
            // Arrange
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                this.Library.InsertBook(book);
            }

            // Act
            for (int i = 1; i <= Books.GetList().Count(); i++)
            {
                this.Library.DeleteBook(i);

                // Assert
                Assert.IsNull(GetElementFromTable<Book>(i));
            }

            // Assert
            Assert.IsEmpty(Library.GetAllBooks());
        }
    }
}
