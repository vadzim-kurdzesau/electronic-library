using System.Linq;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.Comparators;
using ElectronicLibrary.Tests.Extensions;
using ElectronicLibrary.Tests.TestData;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    internal class InventoryNumbersRepositoryTests : BaseTestElectronicLibrary
    {
        protected override void ClearTable()
        {
            ClearTable("inventory_numbers");
            ClearTable("books");
        }

        public override void SetUp()
        {
            ReseedTableIdentifiers("inventory_numbers");
            ReseedTableIdentifiers("books");
        }

        [Test]
        public void InventoryNumbersRepositoryTests_InsertInventoryNumber()
        {
            // Arrange
            int index = 1;
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                Library.InsertBook(book);

                var expected = new InventoryNumber()
                {
                    Id = index,
                    BookId = book.Id,
                    Number = index.ToString()
                };

                index++;

                // Act
                Library.InsertInventoryNumber(expected);

                // Assert
                Assert.IsTrue(new InventoryNumberComparator().Equals(expected, GetElementFromTable<InventoryNumber>(expected.Id)));
            }
        }

        [Test]
        public void BooksRepositoryTests_GetInventoryNumbers()
        {
            // Arrange
            int id = 1;
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                Library.InsertBook(book);

                for (int i = 0; i < 10; id++, i++)
                {
                    var expected = new InventoryNumber()
                    {
                        Id = id,
                        BookId = book.Id,
                        Number = id.ToString()
                    };

                    Library.InsertInventoryNumber(expected);

                    // Act
                    var actual = Library.GetInventoryNumbers(book);

                    // Assert
                    Assert.NotNull(actual.FirstOrDefault(inv => new InventoryNumberComparator().Equals(expected, inv)));
                }
            }
        }
    }
}
