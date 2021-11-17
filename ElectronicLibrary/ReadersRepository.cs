using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ElectronicLibrary.Models;
using ElectronicLibrary.Extensions;

namespace ElectronicLibrary
{
    public class ReadersRepository
    {
        private readonly SqlConnection sqlConnection;

        internal ReadersRepository(SqlConnection sqlConnection)
            => this.sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection), "SqlConnection is null.");

        public IEnumerable<Reader> GetAllReaders()
        {
            const string queryString = @"SELECT * FROM dbo.readers;";
            return this.InitializeCommand(queryString).GetReaders();
        }

        public Reader GetReader(int id)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.id = @Id;";

            return this.InitializeCommand(queryString).AddParameter("@Id", id).GetReaders().FirstOrDefault();
        }

        public IEnumerable<Reader> FindReadersByName(string firstName, string lastName)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.first_name = @FirstName 
                                            AND dbo.readers.last_name  = @LastName;";

            return this.InitializeCommand(queryString).ProvideWithNameParameters(firstName, lastName).GetReaders();
        }

        public Reader FindReaderByPhone(string phone)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.phone = @Phone;";

            return InitializeCommand(queryString).AddParameter("@Phone", phone).GetReaders().FirstOrDefault();
        }

        public Reader FindReaderByEmail(string email)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.email = @Email;";

            return InitializeCommand(queryString).AddParameter("@Email", email).GetReaders().FirstOrDefault();
        }

        public void InsertReader(Reader reader)
        {
            const string queryString = "I_InsertReader";
            var command = this.InitializeCommand(queryString).ProvideWithReaderParameters(reader);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }

        public void UpdateReader(Reader reader)
        {
            const string queryString = @"UPDATE dbo.readers 
                                            SET 
                                                dbo.readers.first_name  =   @FirstName, 
                                                dbo.readers.last_name   =   @LastName,
                                                dbo.readers.email       =   @Email,
                                                dbo.readers.phone       =   @Phone, 
                                                dbo.readers.city_id     =   @CityId,
                                                dbo.readers.address     =   @Address,
                                                dbo.readers.zip         =   @Zip
                                          WHERE 
                                                dbo.readers.id = @Id;";

                this.InitializeCommand(queryString)
                    .ProvideWithReaderParameters(reader).AddParameter("@Id", reader.Id)
                        .ExecuteNonQuery();
        }

        public void DeleteReader(int id)
        {
            const string queryString = @"DELETE dbo.readers
                                          WHERE dbo.readers.id = @Id;";

            this.InitializeCommand(queryString).AddParameter("@Id", id).ExecuteNonQuery();
        }

        private SqlCommand InitializeCommand(string queryString)
            => new SqlCommand(queryString, this.sqlConnection);
    }
}
