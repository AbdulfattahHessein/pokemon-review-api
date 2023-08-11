using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Bases;
using PokemonReviewApp.Data;
using System.Linq.Expressions;

namespace PokemonReviewApp.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity<int>
    {
        protected AppDbContext _context;
        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id) ?? throw new Exception($"{nameof(T)} not Found");
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = DbSet;

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.FirstOrDefault(criteria) ?? throw new Exception($"{nameof(T)} not Found");
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = DbSet;

            query = query.Where(criteria);

            query = includes?.Aggregate(query, (current, include) => current.Include(include)) ?? query;

            //if (includes != null)
            //    query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        public bool IsExist(int id)
        {

            var entity = DbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);

            return entity != null;
        }
        public bool GetAny(Expression<Func<T, bool>> filter)
        {
            return DbSet.Any(filter);
        }

        public virtual bool Insert(T entity)
        {
            var entityEntry = _context.Entry(entity);
            if (entityEntry.State != EntityState.Detached)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
            return true;
        }
        public virtual void InsertList(List<T> entityList)
        {
            foreach (var entity in entityList)
            {
                DbSet.Add(entity);
            }
        }
        public void Update(T entity)
        {
            var entityEntry = _context.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            entityEntry.State = EntityState.Modified;
        }
        public virtual void UpdateList(List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                Update(entity);
            }
        }
        public virtual void Delete(T entity)
        {
            var entityEntry = _context.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }
        public virtual void DeleteList(List<T> entityList)
        {
            foreach (T entity in entityList)
            {
                Delete(entity);
            }
        }
        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }


    }
}
