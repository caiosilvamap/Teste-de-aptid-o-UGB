using Microsoft.EntityFrameworkCore;
using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using System.Linq.Expressions;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DataContext _db;

        public Repository(DataContext dbContext)
        {
            _db = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public virtual async Task<int> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public virtual async Task EditAsync(T entity)
        {
            try
            {
                using (var context = new DataContext())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao editar a entidade: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> FindLastAsync()
        {
            return await _dbSet.OrderByDescending(e => EF.Property<int>(e, "Id")).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsyncNoTracking()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> FindIdNoTrackingAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

    }
}

