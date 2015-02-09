using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting.Parsers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevAssignment.MVC.Models;

namespace DevAssignment.MVC.Controllers
{
    public class UserController : AsyncController
    {
        AccountDbContext context = new AccountDbContext(ConfigurationManager.ConnectionStrings["AccountDbContext"].ConnectionString);
        //
        // GET: /User/
        public async Task<ActionResult> Index()
        {
            return View(await context.User.ToListAsync());
        }

        //
        // GET: /User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await context.User.FirstOrDefaultAsync(u => u.Id == id));
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = context.Entry(user);
                    entity.State = EntityState.Added;
                    await context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, User user)
        {
            try
            {
                var entity = context.Entry(user);
                entity.State = EntityState.Deleted;
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> IsUserAvailable(string Login)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Login == Login);
            if (user == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Format("User {0} exists. Please try other login.", user.Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
