using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyWebApplication.Models
{
    public class SaleViewModel
    {
        public IEnumerable<Sale> SalesPerPage { get; set; }

        public PageInfo PageInfo { get; set; }

        public SelectList SortingFilters { get; set; }

        public SelectList Managers { get; set; }

        public SelectList Customers { get; set; }

    }
}