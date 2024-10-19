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
        if (image is null || image.Length <= 0)
            return ("No image data provided."); // Handle null or empty image files

        // Set the image file path, e.g., "public/images/imageName.jpg"
        var imageFilePath = Path.Combine(_imagePath, image.FileName);

        // Save the file asynchronously
        await using (var stream = new FileStream(imageFilePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        // Return the image file name.
        return image.FileName;
    }
}