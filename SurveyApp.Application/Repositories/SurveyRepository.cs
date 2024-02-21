using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Abstraction.IRepositories;

namespace SurveyApp.Dal.Repositories
{
    public class SurveyRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDatabaseContext _context;
        private readonly DbSet<T> _table;


        public SurveyRepository(AppDatabaseContext context)
        {
            _context = context;
            _table = context.Set<T>();

        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public void Add(T entity)
        {

            if (entity != null)
            {
                _table.Add(entity);
            }
        }

        public void Delete(T entity)
        {

            if (entity != null)
            {
                _table.Remove(entity);
            }
        }


        //public T GetByPredicate(Func<T, bool> predicate)
        //{
        //    return _table.FirstOrDefault(predicate);
        //}


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        
    }
}
