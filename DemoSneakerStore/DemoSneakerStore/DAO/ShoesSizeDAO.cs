using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;

namespace DemoSneakerStore.DAO
{
    public class ShoesSizeDAO: IReporsitory<ShoesSize, float>
    {
        private SneakerStoreContext context;

        public ShoesSizeDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<ShoesSize>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(ShoesSize obj)
        {
            context.ShoesSize.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(ShoesSize obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ShoesSize obj)
        {
            throw new NotImplementedException();
        }

        public Task<ShoesSize> GetObject(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
