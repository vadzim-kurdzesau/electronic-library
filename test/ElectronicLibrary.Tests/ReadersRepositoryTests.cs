using System.Linq;
using ElectronicLibrary.Models;
using ElectronicLibrary.Tests.Comparators;
using ElectronicLibrary.Tests.Extensions;
using ElectronicLibrary.Tests.TestData;
using NUnit.Framework;

namespace ElectronicLibrary.Tests
{
    [TestFixture]
    public class ReadersRepositoryTests : BaseTestElectronicLibrary
    {
        protected override void ClearTable()
            => ClearTable("readers");

        public override void SetUp()
            => ReseedTableIdentifiers("readers");

        [Test]
        public void ReadersRepositoryTests_InsertReader()
        {
            foreach (var reader in Readers.GetList().ExtractData<Reader>())
            {
                // Act
                Library.InsertReader(reader);
                var actual = GetElementFromTable<Reader>(reader.Id);

                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(reader, actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_GetReader()
        {
            foreach (var expected in Readers.GetList().ExtractData<Reader>())
            {
                // Arrange
                Library.InsertReader(expected);

                // Act
                var actual = Library.GetReader(expected.Id);

                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_GetReadersByName()
        {
            // Arrange
            foreach (var expected in Readers.GetList().ExtractData<Reader>())
            {
                Library.InsertReader(expected);

                // Act
                var actual = Library.GetReaderByName(expected.FirstName, expected.LastName).First();

                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_GetReaderByPhone()
        {
            // Arrange
            foreach (var expected in Readers.GetList().ExtractData<Reader>())
            {
                Library.InsertReader(expected);

                // Act
                var actual = Library.GetReaderByPhone(expected.Phone);

                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_GetReaderByEmail()
        {
            // Arrange
            foreach (var expected in Readers.GetList().ExtractData<Reader>())
            {
                Library.InsertReader(expected);

                // Act
                var actual = Library.GetReaderByEmail(expected.Email);

                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(expected, actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_GetAllReaders()
        {
            // Arrange
            foreach (var reader in Readers.GetList().ExtractData<Reader>())
            {
                Library.InsertReader(reader);
            }

            int index = 0;
            var expected = Readers.GetList().ExtractData<Reader>().ToArray();

            // Act
            foreach (var actual in Library.GetAllReaders())
            {
                // Assert
                Assert.IsTrue(new ReaderComparator().Equals(expected[index++], actual));
            }
        }

        [Test]
        public void ReadersRepositoryTests_UpdateReader()
        {
            // Arrange
            var expected = Readers.GetList().ExtractData<Reader>().First();
            Library.InsertReader(expected);
            expected.FirstName = "Test";

            // Act
            Library.UpdateReader(expected);

            // Assert
            Assert.IsTrue(new ReaderComparator().Equals(expected, Library.GetReader(expected.Id)));
        }

        [Test]
        public void ReadersRepositoryTests_DeleteReaders()
        {
            foreach (var reader in Readers.GetList().ExtractData<Reader>())
            {
                // Arrange
                Library.InsertReader(reader);

                // Act
                Library.DeleteReader(reader.Id);

                // Assert
                Assert.IsNull(GetElementFromTable<Reader>(reader.Id));
            }
        }
    }
}
