using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DemoSneakerStore.Controllers
{
    public class MainController : Controller
    {
        private readonly SneakerStoreContext context;
        private ShoesDAO shoesDao;
        private CookieCollection CookieList;
        private Account account;
        private CustomerDAO customerDao;
        private AccountDAO accountDao;

        public MainController(SneakerStoreContext context)
        {
            this.context = context;
            shoesDao = new ShoesDAO(context);
            customerDao = new CustomerDAO(context);
            accountDao = new AccountDAO(context);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(Account acc)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
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

        [HttpGet("Info/{id}")]
        public async Task<IActionResult> ShoesInfo(string id)
        {
            return RedirectToAction("Index", "Shoes", await shoesDao.GetObject(int.Parse(id)));
        }

        public async Task<IActionResult> EditProfile()
        {
            string username = HttpContext.Session.GetString("UserName");
            return View(await customerDao.GetCustomerByUserName(username));
        }

        public async Task<IActionResult> UpdateProfile(string username, string password, string fullname, string phonenumber)
        {
            Customer customer = await customerDao.GetCustomerByUserName(username);
            customer.FullName = fullname;
            customer.MobileNumber = phonenumber;

            Account account = await accountDao.GetObject(username);
            account.Password = password;
            await accountDao.Update(account);
            await customerDao.Update(customer);
            return RedirectToAction("EditProfile");
        }
    }
}
