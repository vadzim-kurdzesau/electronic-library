using Dapper.FluentMap.Mapping;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.EntityMaps
{
    internal class BookMap : EntityMap<Book>
    {
        internal BookMap()
            => Map(b => b.PublicationDate).ToColumn("publication_date");
    }
}
