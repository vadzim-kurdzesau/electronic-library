using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Extensions
{
    internal static class ReadersRepositoryExtensions
    {
        internal static IEnumerable<Reader> GetReaders(this SqlCommand command)
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return CreateReaderObject(reader);
            }
        }

        internal static Reader CreateReaderObject(this SqlDataReader reader)
            => new Reader()
            {
                Id = (int)reader["id"],
                FirstName = reader["first_name"] as string,
                LastName = reader["last_name"] as string,
                Email = reader["email"] as string,
                Phone = reader["phone"] as string,
                CityId = (int)reader["city_id"],
                Address = reader["address"] as string,
                Zip = reader["zip"] as string
            };

        internal static SqlCommand AddParameter(this SqlCommand sqlCommand, string parameterName, object value)
        {
            sqlCommand.Parameters.AddWithValue(parameterName, value);
            return sqlCommand;
        }

        internal static SqlCommand ProvideWithNameParameters(this SqlCommand sqlCommand, string firstName, string lastName)
            => sqlCommand.AddParameter("@FirstName", firstName).AddParameter("@LastName", lastName);

        internal static SqlCommand ProvideWithReaderParameters(this SqlCommand sqlCommand, Reader reader)
        {
            sqlCommand.AddParameter("@FirstName", reader.FirstName)
                .AddParameter("@LastName", reader.LastName).AddParameter("@Email", reader.Email)
                .AddParameter("@Phone", reader.Phone).AddParameter("@CityId", reader.CityId)
                .AddParameter("@Address", reader.Address).AddParameter("@Zip", reader.Zip);

            return sqlCommand;
        }
    }
}
