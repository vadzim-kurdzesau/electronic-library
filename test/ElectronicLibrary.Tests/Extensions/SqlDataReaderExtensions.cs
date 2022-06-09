using System;
using System.Data.SqlClient;

namespace ElectronicLibrary.Tests.Extensions
{
    internal static class SqlDataReaderExtensions
    {
        internal static DateTime? GetNullableDateTime(this SqlDataReader reader, string name)
        {
            var column = reader.GetOrdinal(name);
            return reader.IsDBNull(column) ? (DateTime?)null : (DateTime?)reader.GetDateTime(column);
        }
    }
}
