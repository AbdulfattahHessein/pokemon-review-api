using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Bases;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;
using System.Linq.Expressions;

namespace PokemonReviewApp.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity<int>
    {
        protected AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id) ?? throw new Exception($"{nameof(T)} not Found");
        }
        public T Find(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria) ?? throw new Exception($"{nameof(T)} not Found");
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }

        public bool IsExist(int id)
        {

            var entity = _context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);

            return entity != null;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
