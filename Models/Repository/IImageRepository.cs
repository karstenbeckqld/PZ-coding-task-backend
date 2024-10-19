namespace cheeseBackend.Models;

// The IImageRepository defines the method for the ImageManager and subsequently the ImageController. As with the
// ICheeseRepository, the repository design pattern gets used here.
public interface IImageRepository<TEntity> where TEntity : class
{
    Task<string> SaveImage(TEntity image);
}