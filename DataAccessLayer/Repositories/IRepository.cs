using System.Linq;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Create(TEntity item);

        IQueryable<TEntity> GetAll();

        TEntity Get(int id);

        void Update(TEntity item);

        void Remove(int id);

    }
}
