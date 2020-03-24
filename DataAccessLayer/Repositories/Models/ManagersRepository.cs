using DataAccessLayer.ContextModels;
using DataAccessLayer.EntityModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories.Models
{
    public class ManagersRepository : IRepository<Manager>, IDisposable
    {
        private SalesDBContext _db;

        public ManagersRepository(SalesDBContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(Manager item)
        {
            if (item != null)
                _db.Managers.Add(item);
        }

        public IQueryable<Manager> GetAll()
        {
            return _db.Managers;
        }

        public Manager Get(int id)
        {
            return _db.Managers.Find(id);
        }

        public void Remove(int id)
        {
            var manager = _db.Managers.Find(id);

            if (manager != null)
                _db.Managers.Remove(manager);
        }

        public void Update(Manager item)
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
