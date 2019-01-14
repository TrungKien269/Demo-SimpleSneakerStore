using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoSneakerStore.Controllers
{
    public class ShoesManageController : Controller
    {
        private readonly SneakerStoreContext context;
        private ShoesDAO shoesDao;
        private ImageDAO imageDao;
        private ShoesSizeDAO shoesSizeDao;
        private ShoesColorDAO shoesColorDao;
        private PriceDAO priceDao;
        private IHostingEnvironment _env;

        public ShoesManageController(SneakerStoreContext context, IHostingEnvironment env)
        {
            this.context = context;
            this._env = env;
            shoesDao = new ShoesDAO(context);
            shoesColorDao = new ShoesColorDAO(context);
            shoesSizeDao = new ShoesSizeDAO(context);
            priceDao = new PriceDAO(context);
            imageDao = new ImageDAO(context);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(Shoes shoes)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await shoesDao.GetObject(shoes.Id));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> CreateShoes()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            List<Category> cateList = await shoesDao.GetCategoryList();
            List<Origin> originList = await shoesDao.GetOriginList();
            List<Maker> makerList = await shoesDao.GetMakerList();
            List<Color> colorList = await shoesDao.GetColorList();
            List<Size> sizeList = await shoesDao.GetSizeList();
            ViewBag.cateList = cateList;
            ViewBag.originList = originList;
            ViewBag.makerList = makerList;
            ViewBag.colorList = colorList;
            ViewBag.sizeList = sizeList;
            ViewBag.ShoesID = shoesDao.CountShoes().Result + 111;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExecute(Shoes shoes, string maker, string category, string origin,
            string size, string color, string price)
        {
            var files = HttpContext.Request.Form.Files;
            var file = files[0];
            string filename = shoes.Id + ".jpeg";
            await SaveImage(shoes.Id, file);
            shoes.MakerId = int.Parse(maker);
            shoes.CountryId = int.Parse(origin);
            shoes.CateId = int.Parse(category);
            Image image = new Image
            {
                ShoesId = shoes.Id,
                ImageId = 0,
                Link = filename
            };
            Price shoesprice = new Price
            {
                ShoesId = shoes.Id,
                ShoesPrice = float.Parse(price)
            };
            ShoesColor shoesColor = new ShoesColor
            {
                ColorId = int.Parse(color),
                ShoesId = shoes.Id
            };
            ShoesSize shoesSize = new ShoesSize
            {
                ShoesId = shoes.Id,
                Vnsize = float.Parse(size)
            };
            await shoesDao.Insert(shoes);
            await imageDao.Insert(image);
            await priceDao.Insert(shoesprice);
            await shoesColorDao.Insert(shoesColor);
            await shoesSizeDao.Insert(shoesSize);
            
            return RedirectToAction("CreateShoes", "ShoesManage");
        }

        public Task SaveImage(int ShoesID, IFormFile file)
        {
            string webRoot = _env.WebRootPath;
            var uploads = Path.Combine(webRoot, "images\\shoes");
            var filename = ShoesID + ".jpeg";
            FileStream fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
            return file.CopyToAsync(fileStream);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> EditShoes(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            await SettingVariable();
            return View(await shoesDao.GetObject(int.Parse(id)));
        }

        public async Task<IActionResult> InsertShoesSize(string id, string vnsize)
        {
            ShoesSize shoesSize = new ShoesSize
            {
                ShoesId = int.Parse(id),
                Vnsize = float.Parse(vnsize)
            };
            Shoes shoes = await shoesDao.GetObject(int.Parse(id));
            try
            {
                await shoesSizeDao.Insert(shoesSize);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            await SettingVariable();
            return View("EditShoes", shoes);
        }

        public async Task<IActionResult> InsertShoesPrice(string id, string price)
        {
            Shoes shoes = await shoesDao.GetObject(int.Parse(id));
            Price shoesprice = new Price
            {
                ShoesId = int.Parse(id),
                ShoesPrice = float.Parse(price)
            };

            try
            {
                await priceDao.Insert(shoesprice);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
                
            await SettingVariable();
            return View("EditShoes", shoes);
        }

        public async Task<IActionResult> InsertShoesColor(string id, string color)
        {
            ShoesColor shoesColor = new ShoesColor
            {
                ShoesId = int.Parse(id),
                ColorId = int.Parse(color)
            };
            Shoes shoes = await shoesDao.GetObject(int.Parse(id));
            try
            {
                await shoesColorDao.Insert(shoesColor);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            await SettingVariable();
            return View("EditShoes", shoes);
        }

        public async Task<IActionResult> EditExecute(Shoes shoes, string maker, string category, string origin)
        {
            shoes.CateId = int.Parse(category);
            shoes.CountryId = int.Parse(origin);
            shoes.MakerId = int.Parse(maker);
            await shoesDao.Update(shoes);
            await SettingVariable();
            return View("EditShoes", shoes);
        }

        public async Task SettingVariable()
        {
            List<Category> cateList = await shoesDao.GetCategoryList();
            List<Origin> originList = await shoesDao.GetOriginList();
            List<Maker> makerList = await shoesDao.GetMakerList();
            List<Color> colorList = await shoesDao.GetColorList();
            List<Size> sizeList = await shoesDao.GetSizeList();
            ViewBag.cateList = cateList;
            ViewBag.originList = originList;
            ViewBag.makerList = makerList;
            ViewBag.colorList = colorList;
            ViewBag.sizeList = sizeList;
            ViewBag.ShoesID = shoesDao.CountShoes().Result + 111;
        }
    }
}
