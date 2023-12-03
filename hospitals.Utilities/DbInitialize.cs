//using Hospital.Repositories;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace hospitals.Utilities
//{
//    public class DbInitialize : IDbInitializer
//    {
//        private UserManager<IdentityUser> userManager;
//        private UserManager<IdentityUser> roleManager;
//        private ApplicationDbContext _context;

//        public DbInitialize(UserManager<IdentityUser> userManager,
//            UserManager<IdentityUser> roleManager, 
//            ApplicationDbContext context)
//        {
//            this.userManager = userManager;
//            this.roleManager = roleManager;
//            _context = context;
//        }

//        public void Initialize()
//        {
//            try
//            {
//                if (_context.Database.GetPendingMigrations().Count()>0)
//                {
//                    _context.Database.Migrate();

//                }

//            }
//            catch
//            {
//                throw;

//            }
//            if (!roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult()
//            {
//                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
//                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
//                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();

//            }



//        }
//    }
//}
using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospitals.Utilities
{
    public class DbInitialize : IDbInitializer
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext _context;

        public DbInitialize(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

            }
            catch (Exception)
            {
                throw;
            }

            if (!roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();

                userManager.CreateAsync(new ApplicationUser
                {
                    UserName="ahmed",
                    Email= "ahmed@gmail.com"
                },"ahmed@123").GetAwaiter().GetResult();
                var Appuser= _context.ApplicationUsers.FirstOrDefault(x=>x.Email== "ahmed@gmail.com");
                if(Appuser!=null)
                {
                    userManager.AddToRoleAsync(Appuser, WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult();   
                }
            }
        }
    }
}
