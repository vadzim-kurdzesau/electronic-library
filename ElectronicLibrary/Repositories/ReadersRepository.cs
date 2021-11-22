using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ElectronicLibrary.Models;
using ElectronicLibrary.Extensions;

namespace ElectronicLibrary.Repositories
{
    public class ReadersRepository: BaseRepository
    {
        internal ReadersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            const string queryString = @"SELECT * FROM dbo.readers;";
            return this.GetSqlCommand(queryString, GetSqlConnection())
                       .GetReaders();
        }

        public Reader GetReader(int id)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.id = @Id;";

            return this.GetSqlCommand(queryString, GetSqlConnection())
                       .AddParameter("@Id", id)
                       .GetReaders()
                       .FirstOrDefault();
        }

        public IEnumerable<Reader> FindReadersByName(string firstName, string lastName)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.first_name = @FirstName 
                                            AND dbo.readers.last_name  = @LastName;";

            return this.GetSqlCommand(queryString, GetSqlConnection())
                       .ProvideWithNameParameters(firstName, lastName)
                       .GetReaders();
        }

        public Reader FindReaderByPhone(string phone)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.phone = @Phone;";

            return this.GetSqlCommand(queryString, GetSqlConnection())
                       .AddParameter("@Phone", phone)
                       .GetReaders()
                       .FirstOrDefault();
        }

        public Reader FindReaderByEmail(string email)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.email = @Email;";

            return this.GetSqlCommand(queryString, GetSqlConnection())
                       .AddParameter("@Email", email)
                       .GetReaders()
                       .FirstOrDefault();
        }

        public void InsertReader(Reader reader)
        {
            const string queryString = "dbo.sp_readers_insert";
            using var command = this.InitializeCommandAndProvideReaderParameter(queryString, reader);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }

        public void UpdateReader(Reader reader)
        {
            const string queryString = @"UPDATE dbo.readers 
                                            SET 
                                                first_name  =   @FirstName, 
                                                last_name   =   @LastName,
                                                email       =   @Email,
                                                phone       =   @Phone, 
                                                city_id     =   @CityId,
                                                address     =   @Address,
                                                zip         =   @Zip
                                          WHERE 
                                                id = @Id";

            using var command = this.InitializeCommandAndProvideReaderParameter(queryString, reader);
            command.ExecuteNonQuery();
        }

        private SqlCommand InitializeCommandAndProvideReaderParameter(string queryString, Reader reader)
        {
            var connection = this.GetSqlConnection();
            return this.GetSqlCommand(queryString, connection)
                       .ProvideWithReaderParameters(reader);
        }

        public void DeleteReader(int id)
        {
            const string queryString = @"DELETE dbo.readers 
                                          WHERE dbo.readers.id = @Id;";

            using var command = this.GetSqlCommand(queryString, GetSqlConnection())
                                        .AddParameter("@Id", id);
            command.ExecuteNonQuery();
        }

        internal SqlCommand GetSqlCommand(string queryString, SqlConnection sqlConnection)
            => new SqlCommand(queryString, sqlConnection);
    }
}
