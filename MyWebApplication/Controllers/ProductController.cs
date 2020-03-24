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
    public class ProductController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Info(ProductSortCriteria criteria = ProductSortCriteria.Default, int pageNumber = 1)
        {
            var sortedProducts = await (Task.Run(() => ProductService.SortProducts(ProductService.Products, criteria)));

            var pageSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PageSize"));

            var productViewModel = new ProductViewModel
            {
                PageInfo = new PageInfo(currentPageNumber: pageNumber, pageSize: pageSize, totalItems: sortedProducts.Count()),

                ProductsPerPage = PageInfo.GetItemsPerPage(sortedProducts, pageNumber, pageSize),

                Filters = new SelectList(Enum.GetValues(typeof(ProductSortCriteria)), criteria)
            };

            return PartialView(productViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (await Task.Run(() => ProductService.IsExisted(product.Name, product.Cost)))
                {
                    ModelState.AddModelError("", "Such product is already existing. Change name or cost of product.");

                    return View();
                }

                await Task.Run(() => ProductService.CreateProduct(product));

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View(product);

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            var product = await Task.Run(() => ProductService.GetProduct(id));

            if (product == null)
                return HttpNotFound();

            return PartialView(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => ProductService.DeleteProduct(id));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var product = await Task.Run(() => ProductService.GetProduct(id));

            if (product == null)
                return HttpNotFound();


            return PartialView(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (await Task.Run(() => ProductService.IsExisted(product.Name, product.Cost)))
                {
                    ModelState.AddModelError("", "Such product is already existing. Change name or cost of product.");

                    return View();
                }

                await Task.Run(() => ProductService.EditProduct(product));

                return View("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View(product);
        }
    }
}