using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.TestData;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class ReadersRepositoryTests
    {
        // TODO: implement Reader comparator

        private readonly ElectronicLibraryService libraryService;

        public ReadersRepositoryTests()
        {
            ReseedReadersIdentifiers(Constants.ConnectionString);
            this.libraryService = new ElectronicLibraryService(Constants.ConnectionString);
        }

        private static void ReseedReadersIdentifiers(string connectionString)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            new SqlCommand("DBCC CHECKIDENT ('ElectronicLibrary.dbo.readers', RESEED, 0);", sqlConnection).ExecuteNonQuery();
        }

        [Order(0)]
        [Test]
        public void ReaderRepositoryTests_FillCities()
        {
            //todo: does it test tests fill cities? 
            Assert.AreEqual(6, libraryService.CitiesRepository.Cities.Count);
        }

        [Order(0)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_InsertReader(Reader reader)
        {
            this.libraryService.ReaderRepository.InsertReader(reader);
            Assert.Pass();
        }

        [Order(1)]
        [Test]
        public void ReaderRepositoryTests_GetReader()
        {
            int id = 1;
            foreach (var testData in Readers.GetList())
            {
                var expected = (Reader) testData.Arguments[0];
                var actual = this.libraryService.ReaderRepository.GetReader(id);

                Assert.AreEqual(expected.FirstName, actual.FirstName);
                id++;
            }
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReadersByName(Reader reader)
        {
            var actual = this.libraryService.ReaderRepository.FindReadersByName(reader.FirstName, reader.LastName).First()
                .FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReaderByPhone(Reader reader)
        {
            var actual = this.libraryService.ReaderRepository.FindReaderByPhone(reader.Phone).FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReaderByEmail(Reader reader)
        {
            var actual = this.libraryService.ReaderRepository.FindReaderByEmail(reader.Email).FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [Test]
        public void ReaderRepositoryTests_GetAllReaders()
        {
            int index = 0;
            var expected = GetExpectedReaders().ToArray();
            foreach (var reader in this.libraryService.ReaderRepository.GetAllReaders())
            {
                Assert.AreEqual(expected[index].FirstName, reader.FirstName);
                index++;
            }
        }

        [Order(3)]
        [Test]
        public void ReaderRepositoryTests_UpdateReader()
        {
            var expected = Readers.GetList().First().Arguments[0] as Reader;
            expected.FirstName = "Vadim";

            this.libraryService.ReaderRepository.UpdateReader(expected);
            Assert.AreEqual(expected.FirstName, this.libraryService.ReaderRepository.GetReader(expected.Id).FirstName);
        }

        [Order(4)]
        [Test]
        public void ReaderRepositoryTests_DeleteReaders()
        {
            for (int i = 1; i <= Readers.GetList().Count(); i++)
            {
                this.libraryService.ReaderRepository.DeleteReader(i);
            }

            Assert.IsEmpty(libraryService.ReaderRepository.GetAllReaders());
        }

        private static IEnumerable<Reader> GetExpectedReaders()
        {
            List<Reader> expected = new List<Reader>();
            foreach (var testData in Readers.GetList().ToArray())
            {
                expected.Add(testData.Arguments[0] as Reader);
            }

            return expected;
        }
    }
}