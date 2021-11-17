using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Demo;
using ElectronicLibrary.Models;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class ReaderRepositoryTests
    {
        private readonly ElectronicLibraryRepository library;

        public static IEnumerable<TestCaseData> Readers
        {
            get
            {
                yield return new TestCaseData(new Reader()
                {
                    FirstName = "Vadzim",
                    LastName = "Kurdzesau",
                    Email = "VadzimKurdzesau@mail.com",
                    Phone = "+375112223344",
                    CityId = 1,
                    Address = "Middle st.",
                    Zip = "111222"
                });

                yield return new TestCaseData(new Reader()
                {
                    FirstName = "Nickolay",
                    LastName = "Andreev",
                    Email = "NickolayAndreev@mail.com",
                    Phone = "+375112233445",
                    CityId = 1,
                    Address = "Right st.",
                    Zip = "111223"
                });

                yield return new TestCaseData(new Reader()
                {
                    FirstName = "Zedaph",
                    LastName = "Egorov",
                    Email = "ZedaphEgorov@mail.com",
                    Phone = "+375122334449",
                    CityId = 2,
                    Address = "Left st.",
                    Zip = "121321"
                });
            }
        }

        public ReaderRepositoryTests()
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            ReseedReadersIdentifiers(configurationManager.Configuration["ConnectionStrings:LibraryConnectionString"]);
            this.library = new ElectronicLibraryRepository(configurationManager.Configuration["ConnectionStrings:LibraryConnectionString"]);
        }

        private static void ReseedReadersIdentifiers(string connectionString)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            new SqlCommand("DBCC CHECKIDENT ('ElectronicLibrary.dbo.readers', RESEED, 0);", sqlConnection).ExecuteNonQuery();
        }

        [Order(0)]
        [TestCaseSource(nameof(Readers))]
        public void ReaderRepositoryTests_InsertReader(Reader reader)
        {
            this.library.ReaderRepository.InsertReader(reader);
            Assert.Pass();
        }

        [Order(1)]
        [Test]
        public void ReaderRepositoryTests_GetReader()
        {
            int id = 1;
            foreach (var testData in Readers)
            {
                var expected = testData.Arguments[0] as Reader;
                var actual = this.library.ReaderRepository.GetReader(id);

                Assert.AreEqual(expected.FirstName, actual.FirstName);
                id++;
            }
        }

        [Order(2)]
        [TestCaseSource(nameof(Readers))]
        public void ReaderRepositoryTests_FindReadersByName(Reader reader)
        {
            var actual = this.library.ReaderRepository.FindReadersByName(reader.FirstName, reader.LastName).First()
                .FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [TestCaseSource(nameof(Readers))]
        public void ReaderRepositoryTests_FindReaderByPhone(Reader reader)
        {
            var actual = this.library.ReaderRepository.FindReaderByPhone(reader.Phone).FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [TestCaseSource(nameof(Readers))]
        public void ReaderRepositoryTests_FindReaderByEmail(Reader reader)
        {
            var actual = this.library.ReaderRepository.FindReaderByEmail(reader.Email).FirstName;
            Assert.AreEqual(actual, reader.FirstName);
        }

        [Order(2)]
        [Test]
        public void ReaderRepositoryTests_GetAllReaders()
        {
            int index = 0;
            var expected = GetExpectedReaders().ToArray();
            foreach (var reader in this.library.ReaderRepository.GetAllReaders())
            {
                Assert.AreEqual(expected[index].FirstName, reader.FirstName);
                index++;
            }
        }

        //[Order(3)]
        //[Test]
        //public void ReaderRepositoryTests_UpdateReader()
        //{
        //    var expected = Readers.First().Arguments[0] as Reader;
        //    expected.FirstName = "Vadim";

        //    this.library.ReaderRepository.UpdateReader(expected);
        //    Assert.AreEqual(expected.FirstName, this.library.ReaderRepository.GetReader(1));
        //}

        [Order(5)]
        [Test]
        public void ReaderRepositoryTests_DeleteReaders()
        {
            for (int i = 1; i <= Readers.Count(); i++)
            {
                this.library.ReaderRepository.DeleteReader(i);
            }

            Assert.IsEmpty(library.ReaderRepository.GetAllReaders());
        }

        private static IEnumerable<Reader> GetExpectedReaders()
        {
            List<Reader> expected = new List<Reader>();
            foreach (var testData in Readers.ToArray())
            {
                expected.Add(testData.Arguments[0] as Reader);
            }

            return expected;
        }
    }
}