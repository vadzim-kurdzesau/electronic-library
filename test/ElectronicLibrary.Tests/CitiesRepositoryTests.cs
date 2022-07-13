using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class CitiesRepositoryTests
    {
        private readonly ElectronicLibraryService _library;

        public CitiesRepositoryTests()
        {
            _library = new ElectronicLibraryService(ConfigurationManager.ConnectionString);
        }

        [Test]
        public void CitiesRepositoryTests_FillCities()
        {
            var expected = GetNumberOfEntriesInCitiesTable();
            Assert.AreEqual(expected, _library.GetAllCities().Count());
        }

        private static int GetNumberOfEntriesInCitiesTable()
        {
            const string queryString = "SELECT COUNT(*) FROM dbo.cities;";

            using var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            sqlConnection.Open();

            var command = new SqlCommand(queryString, sqlConnection);
            var reader = command.ExecuteReader();
            reader.Read();

            return (int)reader[0];
        }
    }
}
