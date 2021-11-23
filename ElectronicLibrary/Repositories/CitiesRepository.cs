﻿using System.Collections.Generic;
using Dapper;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Repositories
{
    public class CitiesRepository : BaseRepository
    {
        private readonly List<City> _cities;

        internal CitiesRepository(string connectionString) : base(connectionString)
        {
            this._cities = new List<City>();
            this.FillCities();
        }

        public ICollection<City> Cities
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
            const string queryString = "SELECT * FROM dbo.cities;";

            using (var sqlConnection = GetSqlConnection())
            {
                var cities = sqlConnection.Query<City>(queryString);
                foreach (var city in cities)
                {
                    this._cities.Add(city);
                }
            }
        }
    }
}
