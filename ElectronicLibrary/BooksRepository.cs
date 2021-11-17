using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary
{
    public class BooksRepository
    {
        private readonly DataSet booksDataSet;

        internal BooksRepository(SqlConnection sqlConnection)
        {
            const string selectBooksQuery = "SELECT * FROM dbo.books;", selectBookNumberQuery = "SELECT * FROM dbo.inventory_numbers;";

            this.booksDataSet = new DataSet();

            SqlDataAdapter booksAdapter = new SqlDataAdapter(selectBooksQuery, sqlConnection);
            SqlDataAdapter bookNumbersAdapter = new SqlDataAdapter(selectBookNumberQuery, sqlConnection);

            booksAdapter.Fill(this.booksDataSet, "Books");
            bookNumbersAdapter.Fill(this.booksDataSet, "BookNumbers");

            this.booksDataSet.Tables["Books"].PrimaryKey = new DataColumn[1]
                {this.booksDataSet.Tables["Books"].Columns["id"]};

            this.booksDataSet.Relations.Add("Books_To_BookNumbers", 
                this.booksDataSet.Tables["BookNumbers"].Columns["book_id"],
                    this.booksDataSet.Tables["Books"].Columns["id"], false);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            foreach (DataRow dataRow in this.booksDataSet.Tables["Books"].Rows)
            {
                yield return CreateBookObject(dataRow);
            }
        }

        public void InsertBook(Book book) 
            => AddBookRow(this.booksDataSet.Tables["Books"], book);

        public void DeleteBook(int id)
        {
            var row = this.booksDataSet.Tables["Books"].Rows.Find(id);
            row.Delete();
        }

        private static void AddBookRow(DataTable table, Book book)
        {
            DataRow row = table.NewRow();
            row["name"] = book.Name;
            row["author"] = book.Author;
            row["publication_date"] = book.PublicationDate;

            table.Rows.Add(row);
        }

        internal void SaveChanges(SqlConnection connection)
        {
            var changes = this.booksDataSet.GetChanges();
            SqlDataAdapter adapter = new SqlDataAdapter()
            {
                InsertCommand = new SqlCommand("INSERT dbo.books VALUES (@Name, @Author, @PublicationDate)", connection),
                DeleteCommand = new SqlCommand("DELETE dbo.books WHERE dbo.books.id = @Id", connection)
            };

            adapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, int.MaxValue, "id");

            ProvideAdapterWithInsertParameters(adapter).Update(changes, "Books");
        }

        private static SqlDataAdapter ProvideAdapterWithInsertParameters(SqlDataAdapter adapter)
            => AddInsertParameter("@Name", SqlDbType.NVarChar, 200, "name",
                AddInsertParameter("@Author", SqlDbType.NVarChar, 100, "author",
                    AddInsertParameter("@PublicationDate", SqlDbType.Date, int.MaxValue, "publication_date", adapter)));

        private static SqlDataAdapter AddInsertParameter(string parameterName, SqlDbType type, int size, string sourceColumn, SqlDataAdapter adapter)
        {
            adapter.InsertCommand.Parameters.Add(parameterName, type, size, sourceColumn);
            return adapter;
        }

        private static Book CreateBookObject(DataRow dataRow)
            => new Book()
            {
                Name = dataRow["name"] as string, Author = dataRow["author"] as string,
                PublicationDate = (DateTime)dataRow["publication_date"]
            };
    }
}
