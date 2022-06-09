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
                this.Library.InsertBook(book);

                var expected = new InventoryNumber()
                {
                    Id = index,
                    BookId = book.Id,
                    Number = index.ToString()
                };

                // Act
                this.Library.InsertInventoryNumber(expected);

                // Assert
                Assert.IsTrue(new InventoryNumberComparator().Equals(expected, GetElementFromTable<InventoryNumber>(expected.Id)));

                index++;
            }
        }

        [Test]
        public void BooksRepositoryTests_GetInventoryNumbers()
        {
            // Arrange
            int id = 1;
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                this.Library.InsertBook(book);

                for (int i = 0; i < 10; id++, i++)
                {
                    var expected = new InventoryNumber()
                    {
                        Id = id,
                        BookId = book.Id,
                        Number = id.ToString()
                    };

                    this.Library.InsertInventoryNumber(expected);

                    // Act
                    var actual = this.Library.GetInventoryNumbers(book);

                    // Assert
                    Assert.NotNull(actual.FirstOrDefault(inv => new InventoryNumberComparator().Equals(expected, inv)));
                }
            }
        }
    }
}
