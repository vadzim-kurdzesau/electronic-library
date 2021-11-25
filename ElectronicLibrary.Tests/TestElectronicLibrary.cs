namespace ElectronicLibrary.Tests
{
    public static class TestElectronicLibrary
    {
        public static ElectronicLibraryService LibraryService { get; }

        static TestElectronicLibrary()
        {
            LibraryService = new ElectronicLibraryService(ConfigurationManager.ConnectionString);
        }
    }
}
