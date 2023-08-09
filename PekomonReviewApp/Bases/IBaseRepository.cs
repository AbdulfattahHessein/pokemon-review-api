using System.Linq.Expressions;

namespace PokemonReviewApp.Bases
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool IsExist(int id);
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
