namespace cheeseBackend.Models;

public interface ICheeseRepository<TEntity, TKey> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(string id);
    Task CreateAsync(TEntity item);
    Task UpdateAsync(TKey id, TEntity item);
    Task DeleteAsync(TKey id);
}