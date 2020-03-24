using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Authorization.Models;

[assembly: OwinStartup(typeof(MyWebApplication.App_Start.Startup))]

namespace MyWebApplication.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure context and managers
            app.CreatePerOwinContext <ApplicationContext>(ApplicationContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,

                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}