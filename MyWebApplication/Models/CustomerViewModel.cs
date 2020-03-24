using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyWebApplication.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<Customer> CustomersPerPage { get; set; }

        public PageInfo PageInfo { get; set; }

        public SelectList Filters { get; set; }
    }
}