using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class AccountDAO: IReporsitory<Account, string>
    {
        private SneakerStoreContext context;

        public AccountDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Account>> GetList()
        {
            return context.Account.Include(x => x.Customer).Include(x => x.TypeNavigation).ToListAsync();
        }

        public Task<int> Insert(Account obj)
        {
            context.Account.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Account obj)
        {
            context.Account.Remove(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Update(Account obj)
        {
            context.Account.Update(obj);
            return context.SaveChangesAsync();
        }

        public Task<Account> GetObject(string obj)
        {
            return context.Account.Include(x => x.Customer).Include(x => x.TypeNavigation)
                .FirstOrDefaultAsync(x => x.UserName.Equals(obj));
        }
    }
}
