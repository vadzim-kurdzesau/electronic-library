using System;
using System.Collections.Generic;
using ElectronicLibrary.Models;
using NUnit.Framework;

namespace ElectronicLibrary.Tests.TestData
{
    class Books
    {
        public static IEnumerable<TestCaseData> GetList()
        {
            yield return new TestCaseData(new Book()
            {
                Id = 1,
                Name = "Pride and Prejudice",
                Author = "Jane Ousten",
                PublicationDate = new DateTime(1813, 1, 28)
            });

            yield return new TestCaseData(new Book()
            {
                Id = 2,
                Name = "War and Peace",
                Author = "Leo Tolstoy",
                PublicationDate = new DateTime(1869, 1, 1)
            });

            yield return new TestCaseData(new Book()
            {
                Id = 3,
                Name = "Crime and Punishment",
                Author = "Fyodor Dostoevsky",
                PublicationDate = new DateTime(1866, 1, 1)
            });
        }
    }
}
