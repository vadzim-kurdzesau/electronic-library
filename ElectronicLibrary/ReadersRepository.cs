using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ElectronicLibrary.Models;
using ElectronicLibrary.Extensions;

namespace ElectronicLibrary
{
    public class ReadersRepository: BaseRepository
    {
        internal ReadersRepository(string connectionString) : base(connectionString)
        {
            // todo: it's better to have separate CitiesRepository and move this logic there 
            // todo: why array and not list? 
            this.Cities = new City[this.GetNumberOfCities()];
            this.FillCitiesArray();
        }

        private void FillCitiesArray()
        {
            const string queryString = "SELECT * FROM dbo.cities;";
            var index = 0;

            using (var sqlConnection = GetSqlConnection())
            {
                using (var sqlCommand = this.GetSqlCommand(queryString, sqlConnection))
                {
                    var citiesCollection = sqlCommand.GetCities();
                    foreach (var city in citiesCollection)
                    {
                        this.Cities[index] = city;
                        index++;
                    }
                }
            }
        }

        private int GetNumberOfCities()
        {
            // todo: change to using in all methods / files
            const string queryString = "SELECT COUNT(*) FROM dbo.cities;";
            using var reader = this.GetSqlCommand(queryString, GetSqlConnection()).ExecuteReader();
            reader.Read();

            return (int)reader[0];
        }

        public City[] Cities { get; private set; }

        public IEnumerable<Reader> GetAllReaders()
        {
            const string queryString = @"SELECT * FROM dbo.readers;";
            return this.GetSqlCommand(queryString, GetSqlConnection()).GetReaders();
        }

        public Reader GetReader(int id)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.id = @Id;";

            return this.GetSqlCommand(queryString, GetSqlConnection()).AddParameter("@Id", id).GetReaders().FirstOrDefault();
        }

        public IEnumerable<Reader> FindReadersByName(string firstName, string lastName)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.first_name = @FirstName 
                                            AND dbo.readers.last_name  = @LastName;";

            return this.GetSqlCommand(queryString, GetSqlConnection()).ProvideWithNameParameters(firstName, lastName).GetReaders();
        }

        public Reader FindReaderByPhone(string phone)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.phone = @Phone;";

            return GetSqlCommand(queryString, GetSqlConnection()).AddParameter("@Phone", phone).GetReaders().FirstOrDefault();
        }

        public Reader FindReaderByEmail(string email)
        {
            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.email = @Email;";

            return GetSqlCommand(queryString, GetSqlConnection()).AddParameter("@Email", email).GetReaders().FirstOrDefault();
        }

        public void InsertReader(Reader reader)
        {
            const string queryString = "dbo.sp_readers_insert";
            var command = this.GetSqlCommand(queryString, GetSqlConnection()).ProvideWithReaderParameters(reader);
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

            //todo: one line - one operation / whole solution
            this.GetSqlCommand(queryString, GetSqlConnection())
                .ProvideWithReaderParameters(reader)
                .AddParameter("@Id", reader.Id)
                .ExecuteNonQuery();
        }

        public void DeleteReader(int id)
        {
            const string queryString = @"DELETE dbo.readers 
                                          WHERE dbo.readers.id = @Id;";

            this.GetSqlCommand(queryString, GetSqlConnection()).AddParameter("@Id", id).ExecuteNonQuery();
        }

        private SqlCommand GetSqlCommand(string queryString, SqlConnection sqlConnection)
            => new SqlCommand(queryString, sqlConnection);
    }
}
