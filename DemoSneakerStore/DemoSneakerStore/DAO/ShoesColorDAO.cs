using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;

namespace DemoSneakerStore.DAO
{
    public class ShoesColorDAO: IReporsitory<ShoesColor, float>
    {
        private SneakerStoreContext context;

        public ShoesColorDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<ShoesColor>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(ShoesColor obj)
        {
            context.ShoesColor.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(ShoesColor obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ShoesColor obj)
        {
            throw new NotImplementedException();
        }

        public Task<ShoesColor> GetObject(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
