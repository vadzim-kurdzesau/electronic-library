using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap;
using ElectronicLibrary.EntityMaps;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class ReadersRepository: BaseRepository
    {
        internal ReadersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<Reader> GetAll()
        {
            using var connection = this.GetSqlConnection();
            return connection.GetAll<Reader>();
        }

        public Reader Get(int id)
        {
            ValidateId(id);
            using var connection = this.GetSqlConnection();
            return connection.Get<Reader>(id);
        }

        public IEnumerable<Reader> GetByName(string firstName, string lastName)
        {
            ValidateName(firstName, lastName);

            const string queryString = "dbo.sp_readers_read_by_name";
            return this.InitializeAndQueryStoredProcedure(queryString, new {FirstName = firstName, LastName = lastName});
        }

        private static void ValidateName(string firstName, string lastName)
        {
            ValidateString(firstName);
            ValidateString(lastName);
        }

        public Reader GetByPhone(string phone)
        {
            ValidateString(phone);

            const string queryString = "sp_readers_read_by_phone";
            return InitializeAndQueryStoredProcedure(queryString, new { Phone = phone }).FirstOrDefault();
        }

        private static void ValidateString(string stringToValidate)
        {
            if (string.IsNullOrWhiteSpace(stringToValidate))
            {
                throw new ArgumentNullException(nameof(stringToValidate),
                    "String can't be null, empty or a whitespace.");
            }
        }

        public Reader GetByEmail(string email)
        {
            ValidateString(email);

            const string queryString = "sp_readers_read_by_email";

            return InitializeAndQueryStoredProcedure(queryString, new {Email = email}).FirstOrDefault();
        }

        private IEnumerable<Reader> InitializeAndQueryStoredProcedure(string procedureName, object procedureParameters)
        {
            using var connection = this.GetSqlConnection();
            return connection.Query<Reader>(procedureName, procedureParameters, commandType: CommandType.StoredProcedure);
        }

        public void Insert(Reader reader)
        {
            const string queryString = "dbo.sp_readers_insert";
            this.InitializeAndExecuteStoredProcedure(queryString, ProvideReaderParameters(reader));
        }

        public void Update(Reader reader)
        {
            ValidateReader(reader);

            const string queryString = "dbo.sp_readers_update";
            this.InitializeAndExecuteStoredProcedure(queryString, ProvideReaderParametersWithId(reader));
        }

        private static void ValidateReader(Reader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
        }

        public void Delete(int id)
        {
            ValidateId(id);

            const string queryString = "dbo.sp_readers_delete";
            this.InitializeAndExecuteStoredProcedure(queryString, new { Id = id });
        }

        private static object ProvideReaderParameters(Reader reader)
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

        private static object ProvideReaderParametersWithId(Reader reader)
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
