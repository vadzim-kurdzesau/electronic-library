using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.Comparators;
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
            foreach (var testData in Readers.GetList())
            {
                var expected = (Reader) testData.Arguments[0];
                var actual = this.libraryService.ReaderRepository.GetReader(expected.Id);

                Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
            }
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReadersByName(Reader expected)
        {
            var actual = this.libraryService.ReaderRepository.FindReadersByName(expected.FirstName, expected.LastName).First();
            Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReaderByPhone(Reader expected)
        {
            var actual = this.libraryService.ReaderRepository.FindReaderByPhone(expected.Phone);
            Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
        }

        [Order(2)]
        [TestCaseSource(typeof(Readers), nameof(Readers.GetList))]
        public void ReaderRepositoryTests_FindReaderByEmail(Reader expected)
        {
            var actual = this.libraryService.ReaderRepository.FindReaderByEmail(expected.Email);
            Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
        }

        [Order(2)]
        [Test]
        public void ReaderRepositoryTests_GetAllReaders()
        {
            int index = 0;
            var expected = GetExpectedReaders().ToArray();
            foreach (var actual in this.libraryService.ReaderRepository.GetAllReaders())
            {
                Assert.IsTrue(new ReaderComparator().Equals(expected[index], actual));
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
            Assert.IsTrue(new ReaderComparator().Equals(expected, this.libraryService.ReaderRepository.GetReader(expected.Id)));
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