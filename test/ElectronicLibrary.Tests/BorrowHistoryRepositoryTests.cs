using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.Comparators;
using ElectronicLibrary.Tests.Extensions;
using ElectronicLibrary.Tests.TestData;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    internal class BorrowHistoryRepositoryTests : BaseTestElectronicLibrary
    {
        protected override void ClearTable()
        {
            ClearTable("books");
            ClearTable("inventory_numbers");
            ClearTable("readers");
            ClearTable("borrow_history");
        }

        public override void SetUp()
        {
            ReseedTableIdentifiers("books");
            ReseedTableIdentifiers("inventory_numbers");
            ReseedTableIdentifiers("readers");
            ReseedTableIdentifiers("borrow_history");
        }

        [Test]
        public void BorrowHistoryRepositoryTests_TakeBook()
        {
            // Arrange
            Arrange();
            var books = Library.GetAllBooks().ToArray();
            var readers = Library.GetAllReaders().ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                var expected = Library.GetInventoryNumbers(books[i]).First();

                // Act
                var actual = Library.TakeBook(books[i], readers[i]);

                // Assert
                Assert.IsTrue(new InventoryNumberComparator().Equals(expected, actual));

                // Fluent Mapper doesn't work, if you have same column names mapped even in different mappers and different tables
                //Assert.NotNull(this.GetElementFromTable<BorrowHistoryRecord>(i + 1));
                Assert.NotNull(GetBorrowHistoryRecords(i + 1).FirstOrDefault());
            }
        }

        private static IEnumerable<BorrowHistoryRecord> GetBorrowHistoryRecords(int id)
        {
            var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            sqlConnection.Open();

            var command = new SqlCommand($"SELECT * FROM dbo.borrow_history WHERE id = {id};", sqlConnection);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                foreach (var row in reader)
                {
                    yield return new BorrowHistoryRecord()
                    {
                        Id = (int) reader[0],
                        ReaderId = (int) reader[1],
                        InventoryNumberId = (int) reader[2],
                        BorrowDate = (DateTime) reader[3],
                        ReturnDate = reader.GetNullableDateTime("return_date")
                    };
                }
            }
            else
            {
                yield return null;
            }
        }

        [Test]
        public void BorrowHistoryRepositoryTests_ReturnBook()
        {
            // Arrange
            Arrange();
            var books = Library.GetAllBooks().ToArray();
            var readers = Library.GetAllReaders().ToArray();

            for (int i = 0; i < books.Length; i++)
            {
                var expected = Library.GetInventoryNumbers(books[i]).First();
                var actual = Library.TakeBook(books[i], readers[i]);

                // Act
                Library.ReturnBook(readers[i], expected);

                var a = GetElementFromTable<BorrowHistoryRecord>(i + 1);

                // Assert
                //Assert.NotNull(this.GetElementFromTable<BorrowHistoryRecord>(i + 1).ReturnDate);
                Assert.NotNull(GetBorrowHistoryRecords(i + 1).FirstOrDefault()?.ReturnDate);
            }
        }

        private void Arrange()
        {
            int i = 1;
            foreach (var book in Books.GetList().ExtractData<Book>())
            {
                Library.InsertBook(book);
                Library.InsertInventoryNumber(new InventoryNumber()
                {
                    Id = i,
                    BookId = book.Id,
                    Number = i.ToString()
                });

                i++;
            }

            foreach (var reader in Readers.GetList().ExtractData<Reader>())
            {
                Library.InsertReader(reader);
            }
        }
    }
}
