using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ElectronicLibrary.Models;

namespace ElectronicLibrary
{
    public class ReaderRepository
    {
        private readonly SqlConnection sqlConnection;

        internal ReaderRepository(SqlConnection sqlConnection)
            => this.sqlConnection = sqlConnection 
                                        ?? throw new ArgumentNullException(nameof(sqlConnection), "SqlConnection is null.");

        public IEnumerable<Reader> GetAllReaders()
        {
            const string queryString = "SELECT * FROM dbo.readers JOIN dbo.cities ON dbo.readers.city_id = dbo.cities.id";
            return this.GetReaders(this.InitializeCommand(queryString));
        }

        public Reader FindReader(int id)
        {
            const string queryString = "SELECT * FROM dbo.readers JOIN dbo.cities ON dbo.readers.city_id = dbo.cities.id WHERE @Id = dbo.readers.id";
            return this.GetReaders(AddParameter(this.InitializeCommand(queryString), "@Id", id)).FirstOrDefault();
        }

        private IEnumerable<Reader> GetReaders(SqlCommand command)
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return CreateReaderObject(reader);
            }
        }

        private SqlCommand AddParameter(SqlCommand sqlCommand, string parameterName, object value)
        {
            sqlCommand.Parameters.AddWithValue(parameterName, value);
            return sqlCommand;
        }

        private SqlCommand InitializeCommand(string queryString)
            => new SqlCommand(queryString, this.sqlConnection);

        private static Reader CreateReaderObject(SqlDataReader reader)
            => new Reader()
            {
                FirstName = reader["first_name"] as string,
                LastName = reader["last_name"] as string,
                Email = reader["email"] as string,
                Phone = reader["phone"] as string,
                City = reader["city"] as string,
                Address = reader["address"] as string,
                Zip = reader["zip"] as string
            };
    }
}
