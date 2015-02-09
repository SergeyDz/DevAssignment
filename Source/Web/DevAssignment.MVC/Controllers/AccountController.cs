using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevAssignment.MVC.Models;

namespace DevAssignment.MVC.Controllers
{
    public class AccountController : AsyncController
    {
        //
        // GET: /Account/
        public async Task<ActionResult> Index()
        {
            AccountDbContext context = new AccountDbContext(ConfigurationManager.ConnectionStrings["AccountDbContext"].ConnectionString);
            return View(await context.Account.ToListAsync());
        }
	}
}