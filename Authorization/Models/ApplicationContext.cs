﻿using Microsoft.AspNet.Identity.EntityFramework;


namespace Authorization.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("IdentityDb") 
        { 

        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}
