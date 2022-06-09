using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicLibrary.API.Parameters
{
    public class BookParameters : PaginationParameters
    {
        public string Author { get; set; }

        public string Name { get; set; }
    }
}
