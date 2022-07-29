using System;

namespace ElectronicLibrary.Web.Parameters
{
    /// <summary>
    /// Contains parameters used for pagination.
    /// </summary>
    public class PaginationParameters
    {
        private const int MaxSize = 100;

        private int _page = 1;
        private int _size = 10;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int Page 
        {
            get => _page;
            set
            {
                if (value > 0)
                {
                    _page = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of elements on the page.
        /// </summary>
        public int Size
        {
            get => _size;
            set
            {
                if (value > 0)
                {
                    _size = Math.Min(value, MaxSize);
                }
            }
        }
    }
}
