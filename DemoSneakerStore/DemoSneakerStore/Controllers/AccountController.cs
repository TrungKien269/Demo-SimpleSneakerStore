using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSneakerStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SneakerStoreContext context;
        private AccountDAO accountDao;
        private CustomerDAO customerDao;

        public AccountController(SneakerStoreContext context)
        {
            this.context = context;
            accountDao = new AccountDAO(context);
            customerDao = new CustomerDAO(context);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await accountDao.GetList());
        }

        [HttpGet("GetAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await accountDao.GetObject(id));
        }

        public async Task<IActionResult> DeleteExecute(Account account)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            await accountDao.Delete(account);
            return RedirectToAction("Index", "Account");
        }

        [HttpGet("EditAccount/{id}")]
        public async Task<IActionResult> EditAccount(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await accountDao.GetObject(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditExecute(string username, string password, string type, string status)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            Account account = await accountDao.GetObject(username);
            account.Password = password;
            account.Status = int.Parse(status);
            account.Type = int.Parse(type);
            await accountDao.Update(account);
            return RedirectToAction("Index", "Account");
        }

        public IActionResult CreateAccount()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExecute(string username, string password, string type,
            string fullname, string mobilenumber)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            if (accountDao.GetObject(username).Result != null)
            {
                TempData["Message"] = "This username has been used!";
                return RedirectToAction("CreateAccount", "Account");
            }
            Account account = new Account
            {
                UserName = username,
                Password = password,
                Type = int.Parse(type)
            };
            await accountDao.Insert(account);
            Customer customer = new Customer();
            if (fullname != null && mobilenumber != null)
            {
                customer.Id = customerDao.GetNextID().Result + 1000;
                customer.FullName = fullname;
                customer.MobileNumber = mobilenumber;
                customer.Username = account.UserName;
                await customerDao.Insert(customer);
            }
            return RedirectToAction("CreateAccount", "Account");
        }
    }
}
