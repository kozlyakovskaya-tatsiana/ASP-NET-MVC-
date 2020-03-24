using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyWebApplication.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> ProductsPerPage { get; set; }

        public PageInfo PageInfo { get; set; }

        public SelectList Filters { get; set; }
    }
}