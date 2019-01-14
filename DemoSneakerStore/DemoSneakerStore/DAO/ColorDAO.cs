using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;

namespace DemoSneakerStore.DAO
{
    public class ColorDAO: IReporsitory<Color, int>
    {
        private SneakerStoreContext context;

        public ColorDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Color>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(Color obj)
        {
            context.Color.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Color obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Color obj)
        {
            throw new NotImplementedException();
        }

        public Task<Color> GetObject(int obj)
        {
            throw new NotImplementedException();
        }
    }
}
