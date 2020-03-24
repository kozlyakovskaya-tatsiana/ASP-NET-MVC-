using BusinessLogicLayer.ControllersServices;
using BusinessLogicLayer.Filters;
using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using MyWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWebApplication.Controllers
{
    public class SaleController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Info(int? manager, int? customer, SaleSortCriteria criteria = SaleSortCriteria.Default, int pageNumber = 1)
        {
            IEnumerable<Sale> sales = await Task.Run(() => SaleService.Sales.ToList());


            var managerId = manager ?? 0;

            if (managerId != 0)
            {
                sales = SaleService.FilterByManager(sales, managerId);
            }

            var customerId = customer ?? 0;

            if (customerId != 0)
            {
                sales = SaleService.FilterByCustomer(sales, customerId);
            }

            sales = await Task.Run(() => SaleService.SortSales(sales, criteria));


            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PageSize"));

            var saleViewModel = new SaleViewModel
            {
                PageInfo = new PageInfo(currentPageNumber: pageNumber, pageSize: pageSize, totalItems: sales.Count()),

                SalesPerPage = PageInfo.GetItemsPerPage(sales, pageNumber, pageSize),

                SortingFilters = new SelectList(Enum.GetValues(typeof(SaleSortCriteria)), criteria),

                Managers = await CreateManagerSelectList(managerId),

                Customers = await CreateCustomerSelectList(customerId)
            };

            return PartialView(saleViewModel);

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create()
        {
            var viewModel = new SaleCreateViewModel
            {
                Managers = await CreateManagerSelectList(),

                Customers = await CreateCustomerSelectList(),

                Products = await CreateProductSelectList()
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Sale sale)
        {
            if (sale.ManagerId == 0 || sale.ProductId == 0 || sale.CustomerId == 0)
            {
                ModelState.AddModelError("", "All fields must be filled");

                return View("Create", SaleCreateViewModel.Create());
            }

            await Task.Run(() => SaleService.CreateSale(sale));

            return View("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            var sale = await Task.Run(() => SaleService.GetSale(id));

            if (sale == null)
                return HttpNotFound();

            return PartialView(sale);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => SaleService.DeleteSale(id));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var viewModel = new SaleCreateViewModel
            {
                Managers = await CreateManagerSelectList(),

                Customers = await CreateCustomerSelectList(),

                Products = await CreateProductSelectList(),

                Sale = await Task.Run(() => SaleService.GetSale(id))
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Sale sale)
        {
            if (sale.ManagerId == 0 || sale.ProductId == 0 || sale.CustomerId == 0)
            {
                ModelState.AddModelError("", "All fields must be filled");

                var model = SaleCreateViewModel.Create();

                model.Sale = sale;

                return View(model);
            }

            await Task.Run(() => SaleService.EditSale(sale));

            return RedirectToAction("Index");
        }

        private async Task<SelectList> CreateManagerSelectList(int selectedManagerId = 0)
        {
            var managers = await Task.Run(() => ManagerService.Managers.ToList());

            managers.Insert(0, new Manager { Id = 0, Name = "Choose manager" });

            return new SelectList(managers, "Id", "Name", selectedManagerId);
        }

        private async Task<SelectList> CreateCustomerSelectList(int selectedCustomerId = 0)
        {
            var customers = await Task.Run(() => CustomerService.Customers.ToList());

            customers.Insert(0, new Customer { Id = 0, FullName = "Choose customer" });

            return new SelectList(customers, "Id", "FullName", selectedCustomerId);
        }

        private async Task<SelectList> CreateProductSelectList(int selectedProductId = 0)
        {
            var products = await Task.Run(() => ProductService.Products.ToList());

            products.Insert(0, new Product { Id = 0, Name = "Choose the product" });

            return new SelectList(products, "Id", "Name", selectedProductId);

        }
    }
}