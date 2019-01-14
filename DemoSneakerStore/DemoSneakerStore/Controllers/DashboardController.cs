using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSneakerStore.Controllers
{
    public class DashboardController : Controller
    {

        private readonly SneakerStoreContext context;
        private ShoesDAO shoesDao;
        private CookieCollection CookieList;
        private Account account;

        public DashboardController(SneakerStoreContext context)
        {
            this.context = context;
            shoesDao = new ShoesDAO(context);
        }

        // GET: Shoes
        public async Task<IActionResult> Index(Account acc)
        {
            this.account = acc;
            if (account.UserName == null)
                return View(await shoesDao.GetList());
            else
            {
                string name = Request.Cookies[account.UserName + account.Password];
                HttpContext.Session.SetString("Account", account.UserName + account.Password);
                HttpContext.Session.SetString("UserName", name);
                ViewBag.Account = HttpContext.Session.GetString("UserName");
                return View(await shoesDao.GetList());
            }
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> ShoesDetail(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            if (id == null)
                return NotFound();
            return RedirectToAction("Index", "ShoesManage", await shoesDao.GetObject(int.Parse(id)));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Account()
        {
            return RedirectToAction("Index", "Account");
        }
    }
}
