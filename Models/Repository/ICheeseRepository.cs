namespace cheeseBackend.Models;

// The ICheeseRepository defines the methods for the CheeseManager and subsequently for the CheeseController. I use the
// repository design pattern here for separation of concerns.
public interface ICheeseRepository<TEntity, TKey> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(string id);
    Task CreateAsync(TEntity item);
    Task UpdateAsync(TKey id, TEntity item);
    Task DeleteAsync(TKey id);
}