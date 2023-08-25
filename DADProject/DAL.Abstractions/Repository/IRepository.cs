namespace DAL.Abstractions.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(object id);
    Task InsertAsync(T obj);
    Task InsertRangeAsync(IEnumerable<T> obj);
    Task UpdateAsync(T obj);
    Task DeleteAsync(object id);
    Task DeleteAllAsync();
    Task SaveAsync();
}