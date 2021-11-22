using System;
using System.Data;
using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Extensions
{
    internal static class BooksRepositoryExtensions
    {
        internal static void AddBookRow(this DataTable table, Book book)
        {
            DataRow row = table.NewRow();
            row["id"] = book.Id;
            row["name"] = book.Name;
            row["author"] = book.Author;
            row["publication_date"] = book.PublicationDate;

            table.Rows.Add(row);
        }

        internal static Book CreateBookObject(this DataRow dataRow)
            => new Book()
            {
                Id = (int)dataRow["id"],
                Name = dataRow["name"] as string,
                Author = dataRow["author"] as string,
                PublicationDate = (DateTime)dataRow["publication_date"]
            };

        internal static void UpdateBookRow(this DataRow row, Book book)
        {
            row["name"] = book.Name;
            row["author"] = book.Author;
            row["publication_date"] = book.PublicationDate;
        }

        internal static SqlDataAdapter ProvideAdapterWithInsertParameters(this SqlDataAdapter adapter)
            => adapter.AddInsertParameter("@Name", SqlDbType.NVarChar, 200, "name")
                .AddInsertParameter("@Author", SqlDbType.NVarChar, 100, "author")
                    .AddInsertParameter("@PublicationDate", SqlDbType.Date, int.MaxValue, "publication_date");

        private static SqlDataAdapter AddInsertParameter(this SqlDataAdapter adapter, string parameterName, SqlDbType type, int size, string sourceColumn)
        {
            adapter.InsertCommand.Parameters.Add(parameterName, type, size, sourceColumn);
            return adapter;
        }

        internal static void AddInventoryNumberRow(this DataTable table, InventoryNumber inventoryNumber)
        {
            DataRow row = table.NewRow();
            row["id"] = inventoryNumber.Id;
            row["book_id"] = inventoryNumber.BookId;
            row["number"] = inventoryNumber.Number;

            table.Rows.Add(row);
        }

        internal static InventoryNumber CreateInventoryNumber(this DataRow dataRow)
            => new InventoryNumber()
            {
                Id = (int) dataRow["id"],
                BookId = (int) dataRow["book_id"],
                Number = dataRow["number"] as string
            };
    }
}
