using E_exam.Models;

namespace E_exam.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class, new()
    {
        public E_examDBContext Db { get; }
        public GenericRepository(E_examDBContext db)
        {
            Db = db;
        }
        public ICollection<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }
        public TEntity? GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
        }
        public void Edit(TEntity entity)
        {
            Db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void Delete(int id)
        {
            TEntity? t = GetById(id);
            if (t != null)
            {
                Db.Set<TEntity>().Remove(t);
            }
        }
    }
}
