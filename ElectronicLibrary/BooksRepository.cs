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

        private static Book CreateBookObject(DataRow dataRow)
            => new Book()
            {
                Name = dataRow["name"] as string, Author = dataRow["author"] as string,
                PublicationDate = (DateTime)dataRow["publication_date"]
            };
    }
}
