using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class ImageDAO: IReporsitory<Image, float>
    {
        private SneakerStoreContext context;

        public ImageDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Image>> GetList()
        {
            return context.Image.Include(x => x.Shoes).ToListAsync();
        }

        public Task<int> Insert(Image obj)
        {
            context.Image.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Image obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Image obj)
        {
            throw new NotImplementedException();
        }

        public Task<Image> GetObject(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
