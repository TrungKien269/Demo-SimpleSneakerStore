using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;

namespace DemoSneakerStore.DAO
{
    public class PriceDAO: IReporsitory<Price, float>
    {
        private SneakerStoreContext context;

        public PriceDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Price>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Price obj)
        {
            context.Price.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Price obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Price obj)
        {
            throw new NotImplementedException();
        }

        public Task<Price> GetObject(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
