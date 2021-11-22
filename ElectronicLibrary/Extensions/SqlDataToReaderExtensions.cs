using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Extensions
{
    internal static class SqlDataToReaderExtensions
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
    }
}
