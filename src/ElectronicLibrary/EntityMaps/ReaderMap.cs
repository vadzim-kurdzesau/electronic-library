using Dapper.FluentMap.Mapping;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.EntityMaps
{
    internal class ReaderMap: EntityMap<Reader>
    {
        public ReaderMap()
        { 
            Map(r => r.FirstName).ToColumn("first_name");
            Map(r => r.LastName).ToColumn("last_name");
            Map(r => r.CityId).ToColumn("city_id");
        }
    }
}
