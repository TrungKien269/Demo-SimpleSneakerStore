using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class ShoesDAO:IReporsitory<Shoes, int>
    {
        private SneakerStoreContext context;

        public ShoesDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Shoes>> GetList()
        {
            return context.Shoes.Include(x => x.Image).Include(x => x.Price).Include(x => x.ShoesSize).Include(x => x.Cate)
                .Include(x => x.ShoesColor).Include(x => x.Maker).Include(x => x.Country).ToListAsync();
        }

        public Task<int> Insert(Shoes obj)
        {
            context.Shoes.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Shoes obj)
        {
            context.Shoes.Remove(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Update(Shoes obj)
        {
            context.Shoes.Update(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> CountShoes()
        {
            return context.Shoes.CountAsync();
        }

        //public Task<int> CountImage(int id)
        //{
        //    return context.I
        //}

        public Task<Shoes> GetObject(int obj)
        {
            return context.Shoes.Where(x => x.Id.Equals(obj))
                .Include(x => x.Image).Include(x => x.Price).Include(x => x.ShoesSize)
                .Include(x => x.Cate)
                .Include(x => x.ShoesColor).Include(x => x.Maker).Include(x => x.Country)
                .FirstOrDefaultAsync();
        }

        public Task<Shoes> GetShoesByName(string name)
        {
            return context.Shoes.Include(x => x.Image).Include(x => x.Price).Include(x => x.ShoesSize).Include(x => x.Cate)
                .Include(x => x.ShoesColor).Include(x => x.Maker).Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public Task<List<Maker>> GetMakerList()
        {
            return context.Maker.Include(x => x.Shoes).ToListAsync();
        }

        public Task<List<Category>> GetCategoryList()
        {
            return context.Category.Include(x => x.Shoes).ToListAsync();
        }

        public Task<List<Origin>> GetOriginList()
        {
            return context.Origin.Include(x => x.Shoes).ToListAsync();
        }

        public Task<List<Color>> GetColorList()
        {
            return context.Color.Include(x => x.ShoesColor).ToListAsync();
        }

        public Task<List<Size>> GetSizeList()
        {
            return context.Size.Include(x => x.ShoesSize).ToListAsync();
        }
    }
}
