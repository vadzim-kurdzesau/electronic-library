using System;
using System.Collections.Generic;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Tests.Comparators
{
    internal class ReaderComparator : IEqualityComparer<Reader>
    {
        public bool Equals(Reader x, Reader y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Id == y.Id
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Email == y.Email
                && x.Phone == y.Phone
                && x.CityId == y.CityId
                && x.Address == y.Address
                && x.Zip == y.Zip;
        }

        public int GetHashCode(Reader obj)
            => HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.Email, obj.Phone, obj.CityId, obj.Address,
                obj.Zip);
    }
}
