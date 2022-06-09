using Dapper.FluentMap;

namespace ElectronicLibrary.EntityMaps
{
    internal class FluentMapInitializer
    {
        internal FluentMapInitializer()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new BookMap());
            });
            
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new InventoryNumberMap());
            });

            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ReaderMap());
            });
        }
    }
}
