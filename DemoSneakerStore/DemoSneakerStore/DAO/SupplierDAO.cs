using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class SupplierDAO: IReporsitory<Supplier, int>
    {
        private SneakerStoreContext context;

        public SupplierDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Supplier>> GetList()
        {
            return context.Supplier.ToListAsync();
        }

        public Task<int> Insert(Supplier obj)
        {
            context.Supplier.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Supplier obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Supplier obj)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> GetObject(int obj)
        {
            throw new NotImplementedException();
        }
    }
}
