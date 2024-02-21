
namespace SurveyApp.Application.Abstraction.IRepositories
{
    public interface IBaseRepository <T> where T : class
    {
        Task <List<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        //T GetByPredicate(Func<T, bool> predicate);
       void Delete(T entity);
        Task SaveChangesAsync();
       // Task<T> CreateAsync(T entity);
    }
}
