using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public abstract class BaseTestElectronicLibrary
    {
        protected readonly ElectronicLibraryService Library;

        protected BaseTestElectronicLibrary()
        {
            this.Library = new ElectronicLibraryService(ConfigurationManager.ConnectionString);
        }

        protected void ClearTable(string tableName)
        {
            string queryString = $"DELETE dbo.{tableName};";

            using var connection = new SqlConnection(ConfigurationManager.ConnectionString);
            connection.Execute(queryString);
        }

        protected abstract void ClearTable();

        internal static void ReseedTableIdentifiers(string tableName)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            sqlConnection.Open();

            new SqlCommand($"DBCC CHECKIDENT ('ElectronicLibrary.dbo.{tableName}', RESEED, 0);", sqlConnection).ExecuteNonQuery();
        }

        [TearDown]
        public void TearDown()
        {
            this.ClearTable();
        }

        [SetUp]
        public abstract void SetUp();

        protected T GetElementFromTable<T>(int id) where T : class
        {
            using var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            return sqlConnection.Get<T>(id);
        }
    }
}
