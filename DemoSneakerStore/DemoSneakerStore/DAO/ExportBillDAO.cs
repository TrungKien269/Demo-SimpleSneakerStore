using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class ExportBillDAO: IReporsitory<Sell, int>
    {
        private SneakerStoreContext context;

        public ExportBillDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Sell>> GetList()
        {
            return context.Sell.Include(x => x.ExportBill).Include(x => x.UserNameNavigation)
                .ThenInclude(x => x.Customer).ToListAsync();
        }

        public Task<int> Insert(Sell obj)
        {
            context.Sell.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Sell obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Sell obj)
        {
            throw new NotImplementedException();
        }

        public Task<Sell> GetObject(int obj)
        {
            return context.Sell.Include(x => x.ExportBill).Include(x => x.UserNameNavigation)
                .FirstOrDefaultAsync(x => x.Serial == obj);
        }

        public Task<int> CountBill()
        {
            return context.Sell.CountAsync();
        }

        public Task<int> InsertListProduct(List<ExportBill> cart)
        {
            foreach (var item in cart)
            {
                context.ExportBill.Add(item);
            }
            return context.SaveChangesAsync();
        }

        public Task<List<Sell>> GetHistory(string username)
        {
            return context.Sell.Include(x => x.ExportBill).ThenInclude(x => x.Shoes)
                .Where(x => x.UserName.Equals(username)).ToListAsync();
        }

        public Task<List<ExportBill>> GetExportDetail(int serial)
        {
            return context.ExportBill.Where(x => x.ExSerial.Equals(serial)).Include(x => x.Shoes).ToListAsync();
        }
    }
}
