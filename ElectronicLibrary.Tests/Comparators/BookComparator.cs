using System;
using System.Collections.Generic;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Tests.Comparators
{
    internal class BookComparator : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Id == y.Id 
                && x.Name == y.Name
                && x.Author == y.Author
                && x.PublicationDate.Equals(y.PublicationDate);
        }

        public int GetHashCode(Book obj)
            => HashCode.Combine(obj.Id, obj.Name, obj.Author, obj.PublicationDate);
    }
}
