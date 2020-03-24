using BusinessLogicLayer.ControllersServices;
using DataAccessLayer.EntityModels;
using System.Linq;
using System.Web.Mvc;

namespace MyWebApplication.Models
{
    public class SaleCreateViewModel
    {
        public SelectList Managers { get; set; }

        public SelectList Customers { get; set; }

        public SelectList Products { get; set; }

        public Sale Sale { get; set; }

        public SaleCreateViewModel()
        {

        }

        public static SaleCreateViewModel Create()
        {
            var managers = ManagerService.Managers.ToList();

            managers.Insert(0, new Manager { Id = 0, Name = "Choose the manager" });

            var managersSelectList = new SelectList(managers, "Id", "Name");


            var customers = CustomerService.Customers.ToList();

            customers.Insert(0, new Customer { Id = 0, FullName = "Choose the customer" });

            var customersSelectList = new SelectList(customers, "Id", "FullName");


            var products = ProductService.Products.ToList();

            products.Insert(0, new Product { Id = 0, Name = "Choose the product" });

            var productsSelectList = new SelectList(products, "Id", "Name");

            return new SaleCreateViewModel
            {
                Managers = managersSelectList,

                Customers = customersSelectList,

                Products = productsSelectList
            };
        }
    }
}