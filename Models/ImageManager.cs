// I decided to handle the saving of the image separately, also because the repository pattern didn't allow for an
// additional image file to be passed to the POST request the way it was setup initially.
namespace cheeseBackend.Models;

public class ImageManager : IImageRepository<IFormFile>
{
    private readonly string _imagePath;

    public ImageManager()
    {
        // Define where to store images, e.g., "public/images"
        _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "public/images");
    }
    
    public async Task<string> SaveImage(IFormFile image)
    {
        if (image.Length > 1024 * 1024 * 5) // 5MB limit
        {
            return ("Image too large");
        }
        
        if (image is null || image.Length <= 0)
            return ("No image data provided."); // Handle null or empty image files

        var extension = Path.GetExtension(image.FileName);
        
        if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
        {
            return ("Invalid file type");
        }
        
        if (!Directory.Exists(_imagePath))
        {
            Directory.CreateDirectory(_imagePath);
        }
        
        var filePath = Path.Combine(_imagePath, image.FileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        
        return ("File uploaded successfully");
    }
}