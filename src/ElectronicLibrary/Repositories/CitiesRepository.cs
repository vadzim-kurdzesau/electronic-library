using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class CitiesRepository : BaseRepository
    {
        private readonly List<City> _cities;

        internal CitiesRepository(string connectionString)
            : base(connectionString)
        {
            _cities = new List<City>();
        }

        public IEnumerable<City> Cities
        {
            get
            {
                if (_cities.Count == 0)
                {
                    FillCities();
                }

                return _cities;
            }
        }

        private void FillCities()
        {
            using var connection = GetSqlConnection();
            foreach (var city in connection.GetAll<City>())
            {
                _cities.Add(city);
            }
        }
    }
}
