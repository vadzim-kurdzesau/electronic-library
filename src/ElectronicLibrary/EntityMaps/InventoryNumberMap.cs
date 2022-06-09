using Dapper.FluentMap.Mapping;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.EntityMaps
{
    internal class InventoryNumberMap : EntityMap<InventoryNumber>
    {
        internal InventoryNumberMap()
            => Map(n => n.BookId).ToColumn("book_id");
    }
}
