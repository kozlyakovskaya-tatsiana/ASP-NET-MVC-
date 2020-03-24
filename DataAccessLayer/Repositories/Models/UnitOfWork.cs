using DataAccessLayer.ContextModels;
using System;

namespace DataAccessLayer.Repositories.Models
{
    public class UnitOfWork:IDisposable
    {
        private SalesDBContext _db = new SalesDBContext();

        private CustomersRepository _customersRepository;

        private ManagersRepository _managersRepository;

        private ProductsRepository _productsRepository;

        private SalesRepository _salesRepository;

        public CustomersRepository CustomersRepository
        {
            get
            {
                if (_customersRepository == null)
                    _customersRepository = new CustomersRepository(_db);

                return _customersRepository;
            }
        }

        public ManagersRepository ManagersRepository
        {
            get
            {
                if (_managersRepository == null)
                    _managersRepository = new ManagersRepository(_db);

                return _managersRepository;
            }
        }

        public ProductsRepository ProductsRepository
        {
            get
            {
                if (_productsRepository == null)
                    _productsRepository = new ProductsRepository(_db);

                return _productsRepository;
            }
        }

        public SalesRepository SalesRepository
        {
            get
            {
                if (_salesRepository == null)
                    _salesRepository = new SalesRepository(_db);

                return _salesRepository;
            }
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
