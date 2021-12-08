﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicLibrary.API.Parameters
{
    public class PaginationParameters
    {
        private const int _maxSize = 100;

        private int _size = 15;

        private int _page = 1;

        public int Page
        {
            get => this._page;
            set
            {
                if (value > 0)
                {
                    this._page = value;
                }
            }
        }

        public int Size
        {
            get => this._size;

            set
            {
                if (value > 0)
                {
                    this._size = Math.Min(_maxSize, value);
                }
            }
        }
    }
}