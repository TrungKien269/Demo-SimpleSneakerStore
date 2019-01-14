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
    public class ShoesController : Controller
    {
        private readonly SneakerStoreContext context;
        private ShoesDAO shoesDao;

        public ShoesController(SneakerStoreContext context)
        {
            this.context = context;
            shoesDao = new ShoesDAO(context);
        }

        //public async Task<IActionResult> Index(Shoes shoes)
        //{
        //    return View(await shoesDao.GetObject(Int32.Parse(id)));
        //}

        public async Task<IActionResult> Index(Shoes shoes)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await shoesDao.GetObject(shoes.Id));
        }
    }
}
