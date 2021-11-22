using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Extensions
{
    internal static class ReaderToSqlDataExtensions
    {
        internal static SqlCommand ProvideWithNameParameters(this SqlCommand sqlCommand, string firstName, string lastName)
        {
            sqlCommand
                .AddParameter("@FirstName", firstName)
                .AddParameter("@LastName", lastName);

            return sqlCommand;
        }

        internal static SqlCommand ProvideWithReaderParameters(this SqlCommand sqlCommand, Reader reader)
        {
            sqlCommand
                .AddParameter("@Id", reader.Id)
                .AddParameter("@FirstName", reader.FirstName)
                .AddParameter("@LastName", reader.LastName)
                .AddParameter("@Email", reader.Email)
                .AddParameter("@Phone", reader.Phone)
                .AddParameter("@CityId", reader.CityId)
                .AddParameter("@Address", reader.Address)
                .AddParameter("@Zip", reader.Zip);

            return sqlCommand;
        }
    }
}
