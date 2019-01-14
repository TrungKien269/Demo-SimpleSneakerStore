using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class ImportBillDAO: IReporsitory<WareHouse, int>
    {
        private SneakerStoreContext context;

        public ImportBillDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<WareHouse>> GetList()
        {
            return context.WareHouse.Include(x => x.ImportBill).Include(x => x.Supplier).ToListAsync();
        }

        public Task<int> Insert(WareHouse obj)
        {
            context.WareHouse.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(WareHouse obj)
        {
            context.WareHouse.Remove(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Update(WareHouse obj)
        {
            context.WareHouse.Update(obj);
            return context.SaveChangesAsync();
        }

        public Task<WareHouse> GetObject(int obj)
        {
            return context.WareHouse.Include(x => x.ImportBill).Include(x => x.Supplier)
                .FirstOrDefaultAsync(x => x.Serial.Equals(obj));
        }

        public Task<List<ImportBill>> GetBillList(int serial)
        {
            return context.ImportBill.Include(x => x.Shoes).Where(x => x.ImSerial.Equals(serial)).ToListAsync();
        }

        public Task<int> CountBill()
        {
            return context.WareHouse.CountAsync();
        }

        public Task<int> InsertDetail(ImportBill importBill)
        {
            context.ImportBill.Add(importBill);
            return context.SaveChangesAsync();
        }

        public Task<int> UpdateDetail(ImportBill importBill)
        {
            context.ImportBill.Update(importBill);
            return context.SaveChangesAsync();
        }
    }
}
