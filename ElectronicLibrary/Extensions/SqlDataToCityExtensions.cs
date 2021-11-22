using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Extensions
{
    internal static class SqlDataToCityExtensions
    {
        internal static IEnumerable<City> GetCities(this SqlCommand command)
        {
            using var sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                // TODO: why yield return?
                yield return CreateCityObject(sqlDataReader);
            }
        }

        internal static City CreateCityObject(this SqlDataReader sqlDataReader)
            => new City()
            {
                Id = (int)sqlDataReader["id"],
                Name = sqlDataReader["name"] as string
            };
    }
}
