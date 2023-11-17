using System.Linq;
using System.Threading.Tasks;

namespace react.api.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Query { get; }

        Task Add(T entity);
    }
}
