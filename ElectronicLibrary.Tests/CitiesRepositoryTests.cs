using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class CitiesRepositoryTests
    {
        private readonly ElectronicLibraryService _libraryService;

        public CitiesRepositoryTests()
        {
            this._libraryService = TestElectronicLibrary.LibraryService;
        }

        [Test]
        public void CitiesRepositoryTests_FillCities()
        {
            var expected = this.GetNumberOfEntriesInCitiesTable();
            Assert.AreEqual(expected, _libraryService.Cities.Count());
        }

        private int GetNumberOfEntriesInCitiesTable()
        {
            const string queryString = "SELECT COUNT(*) FROM dbo.cities;";

            using var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionString);
            sqlConnection.Open();
            var command = new SqlCommand(queryString, sqlConnection);
            var reader = command.ExecuteReader();
            reader.Read();
            return (int) reader[0];
        }
    }
}
