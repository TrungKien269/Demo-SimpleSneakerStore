using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSneakerStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly SneakerStoreContext context;

        public static string ShoesID = "";
        private AccountDAO accountDao;
        private CustomerDAO customerDao;
        private ShoesDAO shoesDao;

        public LoginController(SneakerStoreContext context)
        {
            this.context = context;
            accountDao = new AccountDAO(context);
            customerDao = new CustomerDAO(context);
            shoesDao = new ShoesDAO(context);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(String username, String password)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                Account account =
                    await context.Account.SingleOrDefaultAsync(x =>
                        x.UserName.Equals(username) && x.Password.Equals(password));
                if (account == null)
                {
                    TempData["Message"] = "Username or Password is not correct!";
                    return View("Index");
                }
                else
                {
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, username)
                    //};
                    //var userIdentity = new ClaimsIdentity(claims, "login");
                    //ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddHours(1);
                    if (account.Type.Equals(1))
                    {
                        //await HttpContext.SignInAsync(principal);
                        Response.Cookies.Append(account.UserName + account.Password, account.UserName, cookie);
                        return RedirectToAction("Index", "Dashboard", account);
                    }
                    else
                    {
                        if (ShoesID == "")
                        {
                            Response.Cookies.Append(account.UserName + account.Password, account.UserName, cookie);
                            return RedirectToAction("Index", "Main", account);
                        }
                        else
                        {
                            Response.Cookies.Append(account.UserName + account.Password, account.UserName, cookie);
                            HttpContext.Session.SetString("Account", account.UserName + account.Password);
                            HttpContext.Session.SetString("UserName", account.UserName);
                            return RedirectToAction("Index", "Shoes", await shoesDao.GetObject(int.Parse(ShoesID)));
                        }
                    }
                }
            }
            else
            {
                string user = HttpContext.Session.GetString("UserName");
                Account account = await accountDao.GetObject(user);
                if (account.Type.Equals(1))
                {
                    return RedirectToAction("Index", "Dashboard", account);
                }
                else
                {
                    if (ShoesID == "")
                    {
                        return RedirectToAction("Index", "Main", account);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Shoes", await shoesDao.GetObject(int.Parse(ShoesID)));
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            string name = HttpContext.Session.GetString("Account");
            HttpContext.Session.Remove("UserName");
            Response.Cookies.Delete(name);
            HttpContext.Session.Remove("Account");
            //await HttpContext.SignOutAsync()
            ShoesID = "";
            return RedirectToAction("Index", "Login");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomer(string username, string password, string cfpassword, string fullname,
            string phonenumber)
        {
            try
            {
                if(password != cfpassword)
                    throw new Exception("Password need being confirmed!");
                Account account = new Account
                {
                    UserName = username,
                    Password = password,
                    Type = 0,
                    Status = 1
                };
                int nextID = customerDao.GetNextID().Result + 1000;
                Customer customer = new Customer
                {
                    Id = nextID,
                    FullName = fullname,
                    MobileNumber = phonenumber,
                    Username = username
                };
                await accountDao.Insert(account);
                await customerDao.Insert(customer);
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append(account.UserName + account.Password, account.UserName, cookie);
                return RedirectToAction("Index", "Main", account);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return View("SignUp");
            }
        }
    }
}
