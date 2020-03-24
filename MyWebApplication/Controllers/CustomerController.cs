using BusinessLogicLayer.ControllersServices;
using BusinessLogicLayer.Filters;
using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using MyWebApplication.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyWebApplication.Controllers
{

    public class CustomerController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var authorized = User.Identity.IsAuthenticated;
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Info(CustomerSortCriteria criteria = CustomerSortCriteria.Default, int pageNumber = 1)
        {
            var sortedCustomers = await (Task.Run(() => CustomerService.SortCustomers(CustomerService.Customers, criteria)));

            var pageSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PageSize"));

            var customerViewModel = new CustomerViewModel
            {
                PageInfo = new PageInfo(currentPageNumber: pageNumber, pageSize: pageSize, totalItems: sortedCustomers.Count()),

                CustomersPerPage = PageInfo.GetItemsPerPage(sortedCustomers, pageNumber, pageSize),

                Filters = new SelectList(Enum.GetValues(typeof(CustomerSortCriteria)), criteria)
            };

            return PartialView(customerViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => CustomerService.CreateCustomer(customer));

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View();

        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            var customer = await Task.Run(() => CustomerService.GetCustomer(id));

            if (customer == null)
                return HttpNotFound();

            return PartialView(customer);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => CustomerService.DeleteCustomer(id));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var customer = await Task.Run(() => CustomerService.GetCustomer(id));

            if (customer == null)
                return HttpNotFound();

            return PartialView(customer);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => CustomerService.EditCustomer(customer));

                return View("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View(customer);
        }

        [HttpGet]
        public JsonResult CheckExisting(string FullName)
        {
            var result = !CustomerService.IsExisted(FullName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}