using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.DAO;
using DemoSneakerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.Controllers
{
    public class BillController : Controller
    {
        private readonly SneakerStoreContext context;
        private ImportBillDAO importBillDao;
        private ExportBillDAO exportBillDao;
        private SupplierDAO supplierDao;
        private ShoesDAO shoesDao;

        public BillController(SneakerStoreContext context)
        {
            this.context = context;
            importBillDao = new ImportBillDAO(context);
            exportBillDao = new ExportBillDAO(context);
            supplierDao = new SupplierDAO(context);
            shoesDao = new ShoesDAO(context);
        }
        
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await importBillDao.GetList());
        }

        [HttpGet("Import/{id}")]
        public async Task<IActionResult> ImportBillDetail(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            ViewBag.Serial = id;
            return View(await importBillDao.GetBillList(int.Parse(id)));
        }

        public async Task<IActionResult> InsertImportBill()
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            ViewBag.Serial = await importBillDao.CountBill() + 1000;
            ViewBag.supList = await supplierDao.GetList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportBill(WareHouse wareHouse)
        {
            try
            {
                ViewBag.Account = HttpContext.Session.GetString("UserName");
                await importBillDao.Insert(wareHouse);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Index", "Bill");
        }

        [HttpGet("InsertImport/{id}")]
        public async Task<IActionResult> InsertImportDetail(string id)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            ViewBag.shoesList = await shoesDao.GetList();
            ViewBag.Serial = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportDetail(ImportBill importBill)
        {
            try
            {
                ViewBag.Account = HttpContext.Session.GetString("UserName");
                await importBillDao.InsertDetail(importBill);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            ViewBag.Serial = importBill.ImSerial;
            return View("ImportBillDetail", await importBillDao.GetBillList(importBill.ImSerial));
        }

        [HttpGet("EditImport/{id}/{shoesid}")]
        public async Task<IActionResult> EditImportDetail(string id, string shoesid)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            return View(await context.ImportBill.Include(x => x.Shoes)
                .FirstOrDefaultAsync(x => x.ImSerial.Equals(int.Parse(id)) && x.ShoesId.Equals(int.Parse(shoesid))));
        }

        [HttpPost]
        public async Task<IActionResult> EditImportDetailExecute(ImportBill importBill)
        {
            ViewBag.Account = HttpContext.Session.GetString("UserName");
            await importBillDao.UpdateDetail(importBill);
            ViewBag.Serial = importBill.ImSerial;
            return View("ImportBillDetail", await importBillDao.GetBillList(importBill.ImSerial));
        }

        public async Task<IActionResult> ExportBill()
        {
            return View(await exportBillDao.GetList());
        }

        [HttpGet("DetailExport/{id}")]
        public async Task<IActionResult> ExportBillDetail(string id)
        {
            return View(await exportBillDao.GetExportDetail(int.Parse(id)));
        }
    }
}
