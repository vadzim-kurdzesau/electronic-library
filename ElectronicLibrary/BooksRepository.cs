using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ElectronicLibrary.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary
{
    public class BooksRepository
    {
        private readonly DataSet booksDataSet;
        private readonly DataTable booksTable;
        private readonly DataTable inventoryNumbers;

        internal BooksRepository(SqlConnection sqlConnection)
        {
            // TODO: refactor constructor

            const string selectBooksQuery = "SELECT * FROM dbo.books;", selectBookNumberQuery = "SELECT * FROM dbo.inventory_numbers;";

            this.booksDataSet = new DataSet();

            SqlDataAdapter booksAdapter = new SqlDataAdapter(selectBooksQuery, sqlConnection);
            SqlDataAdapter bookNumbersAdapter = new SqlDataAdapter(selectBookNumberQuery, sqlConnection);

            booksAdapter.Fill(this.booksDataSet, "Books");
            bookNumbersAdapter.Fill(this.booksDataSet, "InventoryNumbers");

            this.booksDataSet.Tables["Books"].PrimaryKey = new DataColumn[1]
                {this.booksDataSet.Tables["Books"].Columns["id"]};

            this.booksDataSet.Relations.Add("Books_To_InventoryNumbers", 
                this.booksDataSet.Tables["InventoryNumbers"].Columns["book_id"],
                    this.booksDataSet.Tables["Books"].Columns["id"], false);

            this.booksTable = this.booksDataSet.Tables["Books"];
            this.inventoryNumbers = this.booksDataSet.Tables["InventoryNumbers"];
        }

        public IEnumerable<Book> GetAllBooks()
        {
            foreach (DataRow dataRow in this.booksTable.Rows)
            {
                yield return dataRow.CreateBookObject();
            }
        }

        public void InsertBook(Book book) 
            => this.booksDataSet.Tables["Books"].AddBookRow(book);

        public Book GetBook(int id)
            => this.booksTable.Rows.Find(id).CreateBookObject();

        public IEnumerable<Book> FindBooksByName(string name)
        {
            foreach (DataRow row in this.booksTable.Rows)
            {
                var book = row.CreateBookObject();
                if (book.Name == name)
                {
                    yield return book;
                }
            }
        }

        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            foreach (DataRow row in this.booksTable.Rows)
            {
                var book = row.CreateBookObject();
                if (book.Author == author)
                {
                    yield return book;
                }
            }
        }

        public void DeleteBook(int id)
        {
            var row = this.booksTable.Rows.Find(id);
            row.Delete();
        }

        public void UpdateBook(Book book)
        {
            var row = this.booksTable.Rows.Find(book.Id);
            row.UpdateBookRow(book);
        }

        public IEnumerable<InventoryNumber> GetInventoryNumbers(Book book) 
            => this.GetInventoryNumbers(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(int id)
        {
            var rows = this.booksTable.Rows[id].GetParentRows("Books_To_InventoryNumbers");
            foreach (var row in rows)
            {
                yield return row.CreateInventoryNumber();
            }
        }

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
            => this.inventoryNumbers.AddInventoryNumberRow(inventoryNumber);

        internal void SaveChanges(SqlConnection connection)
        {
            var changes = this.booksDataSet.GetChanges();
            SqlDataAdapter adapter = new SqlDataAdapter()
            {
                InsertCommand = new SqlCommand("INSERT dbo.books VALUES (@Name, @Author, @PublicationDate)", connection),
                DeleteCommand = new SqlCommand("DELETE dbo.books WHERE dbo.books.id = @Id", connection)
            };

            adapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, int.MaxValue, "id");

            if (changes != null)
            {
                adapter.ProvideAdapterWithInsertParameters().Update(changes, "Books");
            }
        }
    }
}
