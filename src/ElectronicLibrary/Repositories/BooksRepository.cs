using System;
using System.Collections.Generic;
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
            using var connection = GetSqlConnection();
            return connection.GetAll<Book>();
        }

        public IEnumerable<Book> GetAll(int page, int size)
        {
            ValidatePaginationParameters(page, size);

            const string procedureName = "dbo.sp_books_read_all_paged";
            return InitializeAndQueryStoredProcedure<Book>(procedureName, new {Page = page, Size = size});
        }

        public Book Get(int id)
        {
            ValidateId(id);

            using var connection = GetSqlConnection();
            return connection.Get<Book>(id);
        }

        public void Insert(Book book)
        {
            const string procedureName = "dbo.sp_books_insert";
            InitializeAndExecuteStoredProcedure(procedureName, ProvideBookParameters(book));
        }

        public bool Delete(int id)
        {
            using var connection = GetSqlConnection();
            return connection.Delete(new Book() {Id = id});
        }

        public void Update(Book book)
        {
            const string procedureName = "dbo.sp_books_update";
            InitializeAndExecuteStoredProcedure(procedureName, ProvideBookParameters(book).AddIdParameter(book.Id));
        }

        public IEnumerable<Book> GetBooksBy(int page, int size, string name, string value)
        {
            var queryString = $"SELECT * FROM dbo.books WHERE { name } LIKE '%' + @{ name } + '%' ORDER BY (SELECT NULL) OFFSET((@Page - 1) * @Size) ROWS FETCH NEXT @Size ROWS ONLY";
            var dynamicParameters = new DynamicParameters()
                                        .AddParameter(name, value)
                                        .AddPaginationParameters(page, size);

            return ExecuteQuery<Book>(queryString, dynamicParameters);
        }

        // TODO: refactor method
        public IEnumerable<Book> GetBooksBy(int page, int size, params (string name, string value)[] parameters)
        {
            ValidateParameters(parameters);

            var dynamicParameters = new DynamicParameters();
            var queryString = new StringBuilder("SELECT * FROM dbo.books WHERE");

            for (int i = 0; i < parameters.Length - 1; i++)
            {
                queryString.Append($" {parameters[i].name} LIKE '%' + @{parameters[i].name}  + '%' AND");
                dynamicParameters.Add(parameters[i].name, parameters[i].value);
            }

            queryString.Append($" {parameters[^1].name} LIKE '%' + @{parameters[^1].name}  + '%'").AppendPagination();

            dynamicParameters.AddParameter(parameters[^1].name, parameters[^1].value)
                             .AddPaginationParameters(page, size);

            return ExecuteQuery<Book>(queryString.ToString(), dynamicParameters);
        }

        private static void ValidateParameters(params (string name, string value)[] parameters)
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
    }
}
