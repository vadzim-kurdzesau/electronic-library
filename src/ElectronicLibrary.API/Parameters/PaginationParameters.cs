using System;

namespace ElectronicLibrary.API.Parameters
{
    public class PaginationParameters
    {
        private const int MaxSize = 100;

        private int _size = 15;

        private int _page = 1;

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

        public int Size
        {
            get => _size;

            set
            {
                if (value > 0)
                {
                    _size = Math.Min(MaxSize, value);
                }
            }
        }
    }
}
