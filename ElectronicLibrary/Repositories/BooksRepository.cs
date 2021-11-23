using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ElectronicLibrary.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class BooksRepository : BaseRepository
    {
        private readonly DataSet _booksDataSet;
        private DataTable _booksTable;
        private DataTable _inventoryNumbersTable;

        internal BooksRepository(string connectionString)
            : base(connectionString)
        {
            using var sqlConnection = this.GetSqlConnection();
            this._booksDataSet = new DataSet();
            this.InitializeAndFillDataSet(sqlConnection);
            this.EstablishRelationship();
        }

        private void InitializeAndFillDataSet(SqlConnection sqlConnection)
        {
            var booksAdapter = InitializeBooksAdapter(sqlConnection);
            var bookNumbersAdapter = InitializeBookNumbersAdapterAdapter(sqlConnection);

            this.InitializeAndFillTables(booksAdapter, bookNumbersAdapter);
        }

        private void InitializeAndFillTables(SqlDataAdapter booksAdapter, SqlDataAdapter bookNumbersAdapter)
        {
            this._booksTable = this.InitializeAndFillBooksTable(booksAdapter);
            this._inventoryNumbersTable = this.InitializeAndFillBookNumbersTable(bookNumbersAdapter);
        }

        private SqlDataAdapter InitializeBooksAdapter(SqlConnection sqlConnection)
        {
            const string selectBooksQuery = "SELECT * FROM dbo.books;";
            return new SqlDataAdapter(selectBooksQuery, sqlConnection);
        }

        private SqlDataAdapter InitializeBookNumbersAdapterAdapter(SqlConnection sqlConnection)
        {
            const string selectBookNumberQuery = "SELECT * FROM dbo.inventory_numbers;";
            return new SqlDataAdapter(selectBookNumberQuery, sqlConnection);
        }

        private DataTable InitializeAndFillBooksTable(SqlDataAdapter booksAdapter)
        {
            booksAdapter.Fill(this._booksDataSet, "Books");
            this._booksDataSet.Tables["Books"].PrimaryKey = new DataColumn[1]
            {
                this._booksDataSet.Tables["Books"].Columns["id"]
            };

            return this._booksDataSet.Tables["Books"];
        }

        private DataTable InitializeAndFillBookNumbersTable(SqlDataAdapter bookNumbersAdapter)
        {
            bookNumbersAdapter.Fill(this._booksDataSet, "InventoryNumbers");
            return this._booksDataSet.Tables["InventoryNumbers"];
        }

        private void EstablishRelationship()
        {
            var parentColumn = this._booksDataSet.Tables["InventoryNumbers"].Columns["book_id"];
            var childColumn = this._booksDataSet.Tables["Books"].Columns["id"];

            this.CreateManyToManyRelationship(parentColumn, childColumn);
        }

        private void CreateManyToManyRelationship(DataColumn parentColumn, DataColumn childColumn)
        {
            this._booksDataSet.Relations.Add("Books_To_InventoryNumbers", parentColumn, childColumn, false);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            foreach (DataRow dataRow in this._booksTable.Rows)
            {
                yield return dataRow.CreateBookObject();
            }
        }

        public void InsertBook(Book book) 
            => this._booksDataSet.Tables["Books"].AddBookRow(book);

        public Book GetBook(int id)
            => this._booksTable.Rows.Find(id).CreateBookObject();

        public IEnumerable<Book> FindBooksByName(string name)
        {
            foreach (DataRow row in this._booksTable.Rows)
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
            foreach (DataRow row in this._booksTable.Rows)
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
            var row = this._booksTable.Rows.Find(id);
            row.Delete();
        }

        public void UpdateBook(Book book)
        {
            var row = this._booksTable.Rows.Find(book.Id);
            row.UpdateBookRow(book);
        }

        public IEnumerable<InventoryNumber> GetInventoryNumbers(Book book) 
            => this.GetInventoryNumbers(book.Id);

        public IEnumerable<InventoryNumber> GetInventoryNumbers(int id)
        {
            var rows = this._booksTable.Rows[id - 1].GetParentRows("Books_To_InventoryNumbers");
            foreach (var row in rows)
            {
                yield return row.CreateInventoryNumber();
            }
        }

        public void InsertInventoryNumber(InventoryNumber inventoryNumber)
            => this._inventoryNumbersTable.AddInventoryNumberRow(inventoryNumber);

        internal void SaveChanges(SqlConnection connection)
        {
            var changes = this._booksDataSet.GetChanges();
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
