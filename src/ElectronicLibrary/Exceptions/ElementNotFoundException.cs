using System;

namespace ElectronicLibrary.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
        {
        }

        public ElementNotFoundException(string name, int id) : base(
            $"There is no element '{name}' in database with specified id: {id}")
        {
        }
    }
}
