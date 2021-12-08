using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;
using ElectronicLibrary.Extensions;
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

        private IEnumerable<Book> InitializeAndQueryStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = this.GetSqlConnection();
            return connection.Query<Book>(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
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

        public IEnumerable<Book> GetBooksBy(int page, int size, string name, string value)
        {
            var queryString = $"SELECT * FROM dbo.books WHERE { name } = @{ name } ORDER BY (SELECT NULL) OFFSET((@Page - 1) * @Size) ROWS FETCH NEXT @Size ROWS ONLY";
            var dynamicParameters = new DynamicParameters()
                                        .AddParameter(name, value)
                                        .AddPaginationParameters(page, size);

            return this.ExecuteQuery(queryString, dynamicParameters);
        }

        private IEnumerable<Book> ExecuteQuery(string queryString, DynamicParameters parameters)
        {
            var connection = this.GetSqlConnection();
            return connection.Query<Book>(queryString, parameters);
        }

        public IEnumerable<Book> GetBooksBy(int page, int size, params (string name, string value)[] parameters)
        {
            ValidateParams(parameters);

            var dynamicParameters = new DynamicParameters();
            var queryString = new StringBuilder("SELECT * FROM dbo.books WHERE");

            for (int i = 0; i < parameters.Length - 1; i++)
            {
                queryString.Append($" {parameters[i].name} = @{parameters[i].name} AND");
                dynamicParameters.Add(parameters[i].name, parameters[i].value);
            }

            queryString.Append($" {parameters[^1].name} = @{parameters[^1].name}").AppendPagination();

            dynamicParameters.AddParameter(parameters[^1].name, parameters[^1].value)
                             .AddPaginationParameters(page, size);

            return this.ExecuteQuery(queryString.ToString(), dynamicParameters);
        }

        private static void ValidateParams(params (string name, string value)[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Number of parameters can't equal zero.");
            }
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
