using BusinessLogicLayer.ControllersServices;
using BusinessLogicLayer.Filters;
using BusinessLogicLayer.Pagination.Models;
using DataAccessLayer.EntityModels;
using MyWebApplication.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MyWebApplication.Controllers
{
    public class ManagerController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Info(ManagerSortCriteria criteria = ManagerSortCriteria.Default, int pageNumber = 1)
        {
            var sortedManagers = await (Task.Run(() => ManagerService.SortManagers(ManagerService.Managers, criteria)));

            var pageSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("PageSize"));

            var managerViewModel = new ManagerViewModel
            {
                PageInfo = new PageInfo(currentPageNumber: pageNumber, pageSize: pageSize, totalItems: sortedManagers.Count()),

                ManagersPerPage = PageInfo.GetItemsPerPage(sortedManagers, pageNumber, pageSize),

                Filters = new SelectList(Enum.GetValues(typeof(ManagerSortCriteria)), criteria)
            };

            return PartialView(managerViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(Manager manager)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => ManagerService.CreateManager(manager));

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View("Create");

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            var manager = await Task.Run(() => ManagerService.GetManager(id));

            if (manager == null)
                return HttpNotFound();

            return PartialView(manager);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => ManagerService.DeleteManager(id));

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var manager = await Task.Run(() => ManagerService.GetManager(id));

            if (manager == null)
                return HttpNotFound();

            return PartialView(manager);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(Manager manager)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => ManagerService.EditManager(manager));

                return View("Index");
            }

            ModelState.AddModelError("", "Invalid data");

            return View(manager);
        }

        [HttpGet]
        public JsonResult CheckExisting(string Name)
        {
            var result = !ManagerService.IsExisted(Name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
