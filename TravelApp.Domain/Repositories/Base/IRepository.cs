using TravelApp.Domain.Model;

namespace TravelApp.Domain.Repositories.Base
{
    public interface IRepository<T> where T : class, new()
    {
        Result<List<T>> GetlAll();
        Result<T> GetById(int i);
        Result<T> Add(T entity);
        Result Delete(T entity);
        Result Update(T entity);
    }
}
