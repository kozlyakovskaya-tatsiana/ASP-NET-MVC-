using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Pagination.Models
{
    public class PageInfo
    {
        public int CurrentPageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItems { get; private set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPreviousPage => CurrentPageNumber > 1;

        public bool HasNextPage => CurrentPageNumber < TotalPages;

        public PageInfo(int currentPageNumber, int pageSize, int totalItems)
        {
            CurrentPageNumber = currentPageNumber;

            PageSize = pageSize;

            TotalItems = totalItems;
        }

        public static IEnumerable<T> GetItemsPerPage<T>(IEnumerable<T> allItems, int pageNumber, int pageSize)
        {
            return allItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
        }
    }
}



