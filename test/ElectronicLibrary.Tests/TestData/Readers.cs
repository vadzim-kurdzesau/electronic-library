using System.Collections.Generic;
using ElectronicLibrary.Models;
using NUnit.Framework;

namespace ElectronicLibrary.Tests.TestData
{
    internal class Readers
    {
        public static IEnumerable<TestCaseData> GetList()
        {
            yield return new TestCaseData(new Reader()
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
}
