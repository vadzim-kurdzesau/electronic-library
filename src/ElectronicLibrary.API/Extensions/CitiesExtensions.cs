using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicLibrary.API.Extensions
{
    internal static class CitiesExtensions
    {
        internal static IEnumerable<SelectListItem> ConvertToSelectListItems(this IEnumerable<City> cities)
            => cities.Select(c => new SelectListItem() { Text = c.Name });
    }
}
