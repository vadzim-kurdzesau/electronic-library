using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    internal sealed class CitiesRepository : BaseRepository
    {
        private readonly List<City> _cities;

        internal CitiesRepository(string connectionString) : base(connectionString)
        {
            this._cities = new List<City>();
            this.FillCities();
        }

        public IEnumerable<City> Cities
        {
            get
            {
                if (this._cities.Count == 0)
                {
                    this.FillCities();
                }

                return this._cities;
            }
        }

        private void FillCities()
        {
            using var connection = GetSqlConnection();
            foreach (var city in connection.GetAll<City>())
            {
                this._cities.Add(city);
            }
        }
    }
}
