using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
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
            const string queryString = "SELECT * FROM dbo.readers JOIN dbo.cities ON dbo.readers.city_id = dbo.cities.id;";
            return GetReaders(this.InitializeCommand(queryString));
        }

        public Reader FindReader(int id)
        {
            const string queryString = "SELECT * FROM dbo.readers JOIN dbo.cities ON dbo.readers.city_id = dbo.cities.id WHERE @Id = dbo.readers.id;";
            return GetReaders(AddParameter("@Id", id, this.InitializeCommand(queryString))).FirstOrDefault();
        }

        public void InsertReader(Reader reader)
        {
            const string queryString = "I_AddReader";
            var command = ProvideWithReaderParameters(this.InitializeCommand(queryString), reader);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }

        public void UpdateReader(int id, Reader reader)
        {
            const string queryString = "UPDATE dbo.readers SET dbo.readers.first_name = @FirstName," +
                                            " dbo.readers.last_name = @LastName, dbo.readers.email = @Email, dbo.readers.phone = @Phone," +
                                                " dbo.readers.city_id = (SELECT city FROM dbo.cities WHERE dbo.cities.id = @CityId)," +
                                                    " dbo.readers.address = @Address, dbo.readers.zip = @Zip;";

            ProvideWithReaderParameters(AddParameter("@Id", id, this.InitializeCommand(queryString)), reader)
                .ExecuteNonQuery();
        }

        public void DeleteReader(int id)
        {
            const string queryString = "DELETE dbo.readers WHERE @Id = dbo.readers.id;";
            AddParameter("@Id", id, this.InitializeCommand(queryString)).ExecuteNonQuery();
        }

        private static IEnumerable<Reader> GetReaders(SqlCommand command)
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return CreateReaderObject(reader);
            }
        }

        private static SqlCommand ProvideWithReaderParameters(SqlCommand command, Reader reader)
        {
            AddParameter("@Zip", reader.Zip,
                AddParameter("@Address", reader.Address,
                    AddParameter("@City", reader.City,
                        AddParameter("@Phone", reader.Phone,
                            AddParameter("@Email", reader.Email,
                                AddParameter("@LastName", reader.LastName,
                                    AddParameter("@FirstName", reader.FirstName, command)))))));

            return command;
        }

        private static SqlCommand AddParameter(string parameterName, object value, SqlCommand sqlCommand)
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
