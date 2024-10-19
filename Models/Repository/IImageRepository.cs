namespace cheeseBackend.Models;

public interface IImageRepository<TEntity> where TEntity : class
{
    Task<string> SaveImage(TEntity image);
}