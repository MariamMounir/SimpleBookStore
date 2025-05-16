using Microsoft.EntityFrameworkCore;
using SimpleVookStore.Data;

namespace SimpleVookStore.Repo
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        public ApplicationDbContext db { get; }
        public GenericRepo(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }
        public TEntity GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }
        public void Edit(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                db.Set<TEntity>().Remove(entity);
            }
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
