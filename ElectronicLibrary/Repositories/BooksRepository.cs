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
            this.InitializeAndExecuteStoredProcedure(queryString, ProvideBookParameters(book));
        }

        public IEnumerable<Book> FindBooksByName(string name)
        {
            const string queryString = "dbo.sp_books_read_by_name";

            foreach (var book in this.InitializeAndQueryStoredProcedure(queryString, new { Name = name }))
            {
                yield return book;
            }
        }

        private IEnumerable<Book> InitializeAndQueryStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = this.GetSqlConnection();
            return connection.Query<Book>(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Book> FindBooksByAuthor(string author)
        {
            const string queryString = "dbo.sp_books_read_by_author";

            foreach (var book in this.InitializeAndQueryStoredProcedure(queryString, new { Author = author }))
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
            const string queryString = "dbo.sp_books_update";

            this.InitializeAndExecuteStoredProcedure(queryString, ProvideBookParametersWithId(book));
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
