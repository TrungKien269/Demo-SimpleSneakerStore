using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSneakerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSneakerStore.DAO
{
    public class CustomerDAO: IReporsitory<Customer, int>
    {
        private SneakerStoreContext context;

        public CustomerDAO(SneakerStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Customer>> GetList()
        {
            return context.Customer.Include(x => x.UsernameNavigation).ToListAsync();
        }

        public Task<int> Insert(Customer obj)
        {
            context.Customer.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Customer obj)
        {
            context.Customer.Remove(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Update(Customer obj)
        {
            context.Customer.Update(obj);
            return context.SaveChangesAsync();
        }

        public Task<Customer> GetObject(int obj)
        {
            return context.Customer.Include(x => x.UsernameNavigation).FirstOrDefaultAsync(x => x.Id.Equals(obj));
        }

        public Task<int> GetNextID()
        {
            return context.Customer.CountAsync();
        }

        public Task<Customer> GetCustomerByUserName(string username)
        {
            return context.Customer.Include(x => x.UsernameNavigation).FirstOrDefaultAsync(x => x.Username.Equals(username));
        }
    }
}
