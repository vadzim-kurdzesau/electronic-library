using System;
using System.Collections.Generic;
using ElectronicLibrary.Models;

namespace ElectronicLibrary.Tests.Comparators
{
    internal class InventoryNumberComparator : IEqualityComparer<InventoryNumber>
    {
        public bool Equals(InventoryNumber x, InventoryNumber y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.BookId == y.BookId && x.Number == y.Number;
        }

        public int GetHashCode(InventoryNumber obj)
        {
            return HashCode.Combine(obj.Id, obj.BookId, obj.Number);
        }
    }
}
