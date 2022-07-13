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
        private static readonly string[] TableNames = new string[]
        {
            "books",
            "readers",
            "inventory_numbers",
            "borrow_history"
        };

        protected readonly ElectronicLibraryService Library;

        protected BaseTestElectronicLibrary()
        {
            Library = new ElectronicLibraryService(ConfigurationManager.ConnectionString);
        }

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            ClassCleanup();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassCleanup()
        {
            foreach (var tableName in TableNames)
            {
                ClearTable(tableName);
                ReseedTableIdentifiers(tableName);
            }
        }

        protected static void ClearTable(string tableName)
        {
            string queryString = $"DELETE dbo.{tableName};";

            using var connection = new SqlConnection(ConfigurationManager.ConnectionString);
            connection.Execute(queryString);
        }

        protected abstract void ClearTable();

        internal static void ReseedTableIdentifiers(string tableName)
        {
            using var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            sqlConnection.Open();

            new SqlCommand($"DBCC CHECKIDENT ('ElectronicLibrary.dbo.{tableName}', RESEED, 0);", sqlConnection).ExecuteNonQuery();
        }

        [TearDown]
        public void TearDown()
        {
            ClearTable();
        }

        [SetUp]
        public abstract void SetUp();

        protected static T GetElementFromTable<T>(int id) where T : class
        {
            using var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            return sqlConnection.Get<T>(id);
        }
    }
}
