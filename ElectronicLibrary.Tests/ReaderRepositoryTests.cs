using System.CodeDom.Compiler;
using System.Collections.Generic;
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
                    City = "Minsk",
                    Address = "Middle st.",
                    Zip = "111222"
                });

                yield return new TestCaseData(new Reader()
                {
                    FirstName = "Nickolay",
                    LastName = "Andreev",
                    Email = "NickolayAndreev@mail.com",
                    Phone = "+375112233445",
                    City = "Minsk",
                    Address = "Right st.",
                    Zip = "111223"
                });

                yield return new TestCaseData(new Reader()
                {
                    FirstName = "Zedaph",
                    LastName = "Egorov",
                    Email = "ZedaphEgorov@mail.com",
                    Phone = "+375122334449",
                    City = "Gomel",
                    Address = "Left st.",
                    Zip = "121321"
                });
            }
        }

        public ReaderRepositoryTests()
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            this.library = new ElectronicLibraryRepository(configurationManager.Configuration["ConnectionStrings:LibraryConnectionString"]);
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
        public void ReaderRepositoryTests_FindReader()
        {
            int id = 1;
            foreach (var testData in Readers)
            {
                var expected = testData.Arguments[0] as Reader;
                var actual = this.library.ReaderRepository.FindReader(id);

                Assert.AreEqual(expected.FirstName, actual.FirstName);
                id++;
            }
        }
    }
}