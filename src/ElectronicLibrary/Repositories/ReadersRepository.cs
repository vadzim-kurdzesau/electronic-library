using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using ElectronicLibrary.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class ReadersRepository: BaseRepository
    {
        internal ReadersRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Reader> GetAll()
        {
            using var connection = GetSqlConnection();
            return connection.GetAll<Reader>();
        }

        public IEnumerable<Reader> GetAll(int page, int size)
        {
            ValidatePaginationParameters(page, size);

            const string procedureName = "dbo.sp_readers_read_all_paged";
            return InitializeAndQueryStoredProcedure<Reader>(procedureName, new { Page = page, Size = size });
        }

        public Reader Get(int id)
        {
            ValidateId(id);

            using var connection = GetSqlConnection();
            return connection.Get<Reader>(id);
        }

        public IEnumerable<Reader> GetByName(string firstName, string lastName)
        {
            ValidateName(firstName, lastName);

            const string procedureName = "dbo.sp_readers_read_by_name";
            return InitializeAndQueryStoredProcedure<Reader>(procedureName, new {FirstName = firstName, LastName = lastName});
        }

        public Reader GetByPhone(string phone)
        {
            ValidateString(phone);

            const string procedureName = "dbo.sp_readers_read_by_phone";
            return InitializeAndQueryStoredProcedure<Reader>(procedureName, new { Phone = phone }).FirstOrDefault();
        }

        public Reader GetByEmail(string email)
        {
            ValidateString(email);

            const string procedureName = "dbo.sp_readers_read_by_email";
            return InitializeAndQueryStoredProcedure<Reader>(procedureName, new {Email = email}).FirstOrDefault();
        }

        public void Insert(Reader reader)
        {
            ValidateReader(reader);

            const string procedureName = "dbo.sp_readers_insert";
            InitializeAndExecuteStoredProcedure(procedureName, ProvideReaderParameters(reader));
        }

        public void Update(Reader reader)
        {
            ValidateReader(reader);

            const string procedureName = "dbo.sp_readers_update";
            InitializeAndExecuteStoredProcedure(procedureName, ProvideReaderParameters(reader).AddIdParameter(reader.Id));
        }

        public void Delete(int id)
        {
            ValidateId(id);

            const string procedureName = "dbo.sp_readers_delete";
            InitializeAndExecuteStoredProcedure(procedureName, new DynamicParameters( new { Id = id }));
        }

        private static void ValidateName(string firstName, string lastName)
        {
            ValidateString(firstName);
            ValidateString(lastName);
        }

        private static void ValidateReader(Reader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
        }

        private static DynamicParameters ProvideReaderParameters(Reader reader)
            => new DynamicParameters(new 
            {
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                Email = reader.Email,
                Phone = reader.Phone,
                CityId = reader.CityId,
                Address = reader.Address,
                Zip = reader.Zip
            });
    }
}
