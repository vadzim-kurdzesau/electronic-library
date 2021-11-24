using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class BooksRepository : BaseRepository
    {
        static BooksRepository()
            => FluentMapper.Initialize(config =>
            {
                config.AddMap(new BookMap());
            });

        internal BooksRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using var connection = this.GetSqlConnection();
            return connection.GetAll<Book>();
        }

        public Book GetBook(int id)
        {
            using var connection = this.GetSqlConnection();
            return connection.Get<Book>(id);
        }

        public void InsertBook(Book book)
        {
            const string queryString = "dbo.sp_books_insert";
            using var connection = this.GetSqlConnection();
            connection.Execute(queryString, ProvideBookParameters(book), commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Book> FindBooksByName(string name)
        {
            const string queryString = @"SELECT *
                                           FROM dbo.books 
                                          WHERE dbo.books.name = @Name;";

            using var connection = this.GetSqlConnection();
            foreach (var book in connection.Query<Book>(queryString, new { Name = name }))
            {
                yield return book;
            }
        }

        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            const string queryString = @"SELECT *
                                           FROM dbo.books 
                                          WHERE dbo.books.author = @Author;";

            using var connection = this.GetSqlConnection();
            foreach (var book in connection.Query<Book>(queryString, new { Author = author }))
            {
                yield return book;
            }
        }

        public void DeleteBook(int id)
        {
            using var connection = this.GetSqlConnection();
            connection.Delete(new Book() {Id = id});
        }

        public void UpdateBook(Book book)
        {
            const string queryString = @"UPDATE dbo.books 
                                            SET 
                                                name             = @Name, 
                                                author           = @Author,
                                                publication_date = @PublicationDate
                                          WHERE 
                                                id = @Id";

            using var connection = this.GetSqlConnection();
            connection.Execute(queryString, ProvideBookParametersWithId(book));
        }

        private static object ProvideBookParameters(Book book)
            => new
            {
                Name = book.Name,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };

        private static object ProvideBookParametersWithId(Book book)
            => new
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            };
    }
}
