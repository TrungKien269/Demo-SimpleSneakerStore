using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Helpers;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSneakerStore.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        private readonly SneakerStoreContext context;
        private ShoesDAO shoesDao;
        private ExportBillDAO exportBillDao;

        public CartController(SneakerStoreContext context)
        {
            this.context = context;
            shoesDao = new ShoesDAO(context);
            exportBillDao = new ExportBillDAO(context);
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            List<ExportBill> cart = new List<ExportBill>();
            if (SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart") != null)
            {
                cart = SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart");
                foreach (var item in cart)
                {
                    item.Shoes = await shoesDao.GetObject(item.ShoesId);
                }
                ViewBag.Total = cart.Sum(item => item.Price * item.Quantity);
                ViewBag.Cart = cart;
            }
            else
            {
                List<Sell> list = await exportBillDao.GetList();
                ViewBag.Cart = cart;
                ViewBag.Total = 0;
            }
            return View();
        }

        [HttpGet("Choose/{id}")]
        public async Task<IActionResult> Select(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            if (HttpContext.Session.GetString("UserName") == null)
            {
                HttpContext.Session.SetString("ShoesID", id);
                LoginController.ShoesID = id;
                return RedirectToAction("Index", "Login");
            }
            else
            {
                Shoes shoes = await shoesDao.GetObject(int.Parse(id));
                if (SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart") == null)
                {
                    List<ExportBill> cart = new List<ExportBill>();
                    cart.Add(new ExportBill
                    {
                        //ExSerial = await exportBillDao.CountBill() + 1000,
                        ShoesId = int.Parse(id),
                        Price = shoes.Price[0].ShoesPrice,
                        Quantity = 1
                    });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
                }
                else
                {
                    List<ExportBill> cart = SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart");
                    int index = isExist(id);
                    if (!index.Equals(-1))
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new ExportBill
                        {
                            //ExSerial = await exportBillDao.CountBill() + 1000,
                            ShoesId = int.Parse(id),
                            Price = shoes.Price[0].ShoesPrice,
                            Quantity = 1
                        });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
                }
                return RedirectToAction("Index", "Shoes", shoes);
            }
        }

        [HttpGet("UnChoose/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            List<ExportBill> cart = SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        private int isExist(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            List<ExportBill> cart = SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ShoesId.Equals(int.Parse(id)))
                {
                    return i;
                }
            }
            return -1;
        }

        public async Task<IActionResult> Buy()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            List<ExportBill> cart = SessionHelper.GetObjectFromJson<List<ExportBill>>(HttpContext.Session, "Cart");
            try
            {
                if(cart.Count == 0)
                    throw new Exception();
                Sell sell = new Sell
                {
                    UserName = HttpContext.Session.GetString("UserName"),
                    Serial = await exportBillDao.CountBill() + 1000,
                    DateTime = DateTime.Now
                };
                cart.ForEach(x => x.ExSerial = sell.Serial);
                await exportBillDao.Insert(sell);
                await exportBillDao.InsertListProduct(cart);

                cart.Clear();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> History()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            string username = HttpContext.Session.GetString("UserName");
            return View(await exportBillDao.GetHistory(username));
        }
    }
}
