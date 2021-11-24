using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class ReadersRepository: BaseRepository
    {
        static ReadersRepository()
            => FluentMapper.Initialize(config =>
            {
                config.AddMap(new ReaderMap());
            });

        internal ReadersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Reader> GetAllReaders()
        {
            using var connection = this.GetSqlConnection();
            return connection.GetAll<Reader>();
        }

        public Reader GetReader(int id)
        {
            ValidateId(id);
            using var connection = this.GetSqlConnection();
            return connection.Get<Reader>(id);
        }

        public IEnumerable<Reader> FindReadersByName(string firstName, string lastName)
        {
            ValidateName(firstName, lastName);

            const string queryString = @"SELECT *
                                           FROM dbo.readers 
                                          WHERE dbo.readers.first_name = @FirstName 
                                            AND dbo.readers.last_name  = @LastName;";

            using var connection = this.GetSqlConnection();
            return connection.Query<Reader>(queryString, new
            {
                FirstName = firstName,
                LastName = lastName
            });
        }

        private static void ValidateName(string firstName, string lastName)
        {
            ValidateString(firstName);
            ValidateString(lastName);
        }

        public Reader FindReaderByPhone(string phone)
        {
            ValidateString(phone);

            const string queryString = @"SELECT *
                                           FROM dbo.readers 
                                          WHERE dbo.readers.phone = @Phone;";

            using var connection = this.GetSqlConnection();
            return connection.QueryFirstOrDefault<Reader>(queryString, new { Phone = phone });
        }

        private static void ValidateString(string stringToValidate)
        {
            if (string.IsNullOrWhiteSpace(stringToValidate))
            {
                throw new ArgumentNullException(nameof(stringToValidate),
                    "String can't be null, empty or a whitespace.");
            }
        }

        public Reader FindReaderByEmail(string email)
        {
            ValidateString(email);

            const string queryString = @"SELECT * 
                                           FROM dbo.readers 
                                          WHERE dbo.readers.email = @Email;";

            using var connection = this.GetSqlConnection();
            return connection.QueryFirstOrDefault<Reader>(queryString, new { Email = email });
        }

        public void InsertReader(Reader reader)
        {
            const string queryString = "dbo.sp_readers_insert";
            using var connection = this.GetSqlConnection();
            connection.Execute(queryString, ProvideReaderParameters(reader), commandType: CommandType.StoredProcedure);
        }

        public void UpdateReader(Reader reader)
        {
            ValidateReader(reader);

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

            using var connection = this.GetSqlConnection();
            connection.Execute(queryString, ProvideReaderParametersWithId(reader));
        }

        private static void ValidateReader(Reader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
        }

        public void DeleteReader(int id)
        {
            ValidateId(id);

            //const string queryString = @"DELETE dbo.readers 
            //                              WHERE dbo.readers.id = @Id;";

            using var connection = this.GetSqlConnection();
            connection.Delete(new Reader() {Id = id});
            //connection.Execute(queryString, new { Id = id });
        }

        internal static object ProvideReaderParameters(Reader reader)
            => new
            {
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                CityId = reader.CityId,
                Address = reader.Address,
                Zip = reader.Zip
            };

        internal static object ProvideReaderParametersWithId(Reader reader)
            => new
            {
                Id = reader.Id,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                CityId = reader.CityId,
                Address = reader.Address,
                Zip = reader.Zip
            };
    }
}
