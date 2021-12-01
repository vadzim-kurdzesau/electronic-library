using System.Collections.Generic;
using NUnit.Framework;

namespace ElectronicLibrary.Tests.Extensions
{
    internal static class TestDataExtensions
    {
        internal static IEnumerable<T> ExtractData<T>(this IEnumerable<TestCaseData> testCaseData) where T : class
        {
            foreach (var data in testCaseData)
            {
                yield return data.Arguments[0] as T;
            }
        }
    }
}
