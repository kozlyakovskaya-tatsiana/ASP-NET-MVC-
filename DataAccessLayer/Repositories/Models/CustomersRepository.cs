using DataAccessLayer.ContextModels;
using DataAccessLayer.EntityModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories.Models
{
    public class CustomersRepository : IRepository<Customer>, IDisposable
    {
        private SalesDBContext _db;

        public CustomersRepository(SalesDBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(Customer item)
        {
            if (item != null)
                _db.Customers.Add(item);
        }

        public IQueryable<Customer> GetAll()
        {
            return _db.Customers;
        }

        public Customer Get(int id)
        {
            return _db.Customers.Find(id);
        }

        public void Remove(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer != null)
                _db.Customers.Remove(customer);
        }

        public void Update(Customer item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
