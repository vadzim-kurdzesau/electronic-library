using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace ElectronicLibrary.Tests
{
    [TestClass]
    [TestFixture]
    public abstract class BaseTestElectronicLibrary
    {
        protected readonly ElectronicLibraryService Library;

        protected BaseTestElectronicLibrary()
        {
            this.Library = new ElectronicLibraryService(ConfigurationManager.ConnectionString);
        }

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            ClassCleanup();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassCleanup()
        {
            ClearTable("books");
            ReseedTableIdentifiers("books");
            ClearTable("readers");
            ReseedTableIdentifiers("readers");
            ClearTable("inventory_numbers");
            ReseedTableIdentifiers("inventory_numbers");
            ClearTable("borrow_history");
            ReseedTableIdentifiers("borrow_history");
        }

        public static void ClearTable(string tableName)
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
