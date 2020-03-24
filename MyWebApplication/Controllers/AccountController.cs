using Authorization.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyWebApplication.Controllers
{

    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            /*var roleAdmin = new ApplicationRole { Name = "admin", Description = "AdminRole" };

            var roleUser = new ApplicationRole { Name = "user", Description = "Just a user" };

            await RoleManager.CreateAsync(roleAdmin);

            await RoleManager.CreateAsync(roleUser);

            var admin = new ApplicationUser { UserName = "maxim@mail.ru", Email = "maxim@mail.ru" };

            string password = "admin2601";

            await UserManager.CreateAsync(admin, password);

            await UserManager.AddToRoleAsync(maxim.Id, "admin");*/

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var result1 = await UserManager.CreateAsync(user, model.Password);

                if (result1.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "user");

                    return RedirectToAction("Login", "Account");
                }

                else
                {
                    foreach (string error in result1.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                else
                {
                    var claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);

                    AuthenticationManager.SignOut();

                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, claim);

                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        ViewBag.CurrentAccount = model.Email;
                        return RedirectToAction("Index", "Home");
                    }

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Login");
        }
    }

}