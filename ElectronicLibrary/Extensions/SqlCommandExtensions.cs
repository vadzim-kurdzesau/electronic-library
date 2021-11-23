using System.Data.SqlClient;

namespace ElectronicLibrary.Extensions
{
    internal static class SqlCommandExtensions
    {
        internal static SqlCommand AddParameter(this SqlCommand sqlCommand, string parameterName, object value)
        {
            sqlCommand.Parameters.AddWithValue(parameterName, value);
            return sqlCommand;
        }
    }
}
