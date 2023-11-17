using Microsoft.EntityFrameworkCore;
using react.api.DBContext;
using System.Linq;
using System.Threading.Tasks;

namespace react.api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApiDdContext _context;
        private DbSet<T> _dbSet;
        public Repository(ApiDdContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Query => _context.Set<T>().AsNoTracking().AsQueryable();


        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
