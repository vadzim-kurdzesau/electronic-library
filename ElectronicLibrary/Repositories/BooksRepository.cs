using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class BooksRepository : BaseRepository
    {
        internal BooksRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Book> GetAll()
        {
            using var connection = this.GetSqlConnection();
            return connection.GetAll<Book>();
        }

        public IEnumerable<Book> GetAll(int page, int size)
        {
            if (page <= 0 || size < 0)
            {
                throw new ArgumentException("Invalid size or page argument.");
            }

            const string queryString = "dbo.sp_books_read_all_paged";
            return this.InitializeAndQueryStoredProcedure(queryString, new {Page = page, Size = size});
        }

        public Book Get(int id)
        {
            using var connection = this.GetSqlConnection();
            return connection.Get<Book>(id);
        }

        public void Insert(Book book)
        {
            const string queryString = "dbo.sp_books_insert";
            this.InitializeAndExecuteStoredProcedure(queryString, ProvideBookParameters(book));
        }

        public IEnumerable<Book> GetByName(string name)
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

        public IEnumerable<Book> GetByAuthor(string author)
        {
            const string queryString = "dbo.sp_books_read_by_author";

            foreach (var book in this.InitializeAndQueryStoredProcedure(queryString, new { Author = author }))
            {
                yield return book;
            }
        }

        public bool Delete(int id)
        {
            using var connection = this.GetSqlConnection();
            return connection.Delete(new Book() {Id = id});
        }

        public void Update(Book book)
        {
            const string queryString = "dbo.sp_books_update";

            this.InitializeAndExecuteStoredProcedure(queryString, ProvideBookParametersWithId(book));
        }

        private static DynamicParameters ProvideBookParameters(Book book)
            => new DynamicParameters(new
            {
                Name = book.Name,
                Author = book.Author,
                PublicationDate = book.PublicationDate
            });


        private static DynamicParameters ProvideBookParametersWithId(Book book)
        {
            var parameters = ProvideBookParameters(book);
            parameters.Add("Id", book.Id, DbType.Int32);
            return parameters;
        }
    }
}
