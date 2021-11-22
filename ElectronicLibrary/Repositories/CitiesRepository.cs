using System.Collections.Generic;
using System.Data.SqlClient;
using ElectronicLibrary.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class CitiesRepository : BaseRepository
    {
        internal CitiesRepository(string connectionString) : base(connectionString)
        {
            this.Cities = new List<City>();
            this.FillCities();
        }

        public ICollection<City> Cities { get; private set; }

        private void FillCities()
        {
            const string queryString = "SELECT * FROM dbo.cities;";

            using (var sqlConnection = GetSqlConnection())
            {
                using (var sqlCommand = new SqlCommand(queryString, sqlConnection))
                {
                    var citiesCollection = sqlCommand.GetCities();
                    foreach (var city in citiesCollection)
                    {
                        this.Cities.Add(city);
                    }
                }
            }
        }
    }
}
