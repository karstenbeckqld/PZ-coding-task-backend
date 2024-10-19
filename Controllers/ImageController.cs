using System.Net.Mime;
using cheeseBackend.Models;
using Microsoft.AspNetCore.Mvc;
using ImageMagick;

namespace cheeseBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController(): ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile imageFile)
    {
        if (imageFile.Length > 1024 * 1024 * 5) // 5MB limit
        {
            return BadRequest("File too large");
        }

        if (imageFile is not null && imageFile.Length > 0)
        {
            if (imageFile.ContentType.Contains("image"))
            {
                var image = await pic(imageFile);
            }
        }
        
        var extension = Path.GetExtension(imageFile.FileName);
        
        if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
        {
            return BadRequest("Invalid file type");
        }
        
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "public", "images");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        var filePath = Path.Combine(folderPath, imageFile.FileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }
        
        return Ok(new {message = "File uploaded successfully"});
        
        
        
        // if (imageFile == null || imageFile.Length == 0)
        // {
        //     return BadRequest("No image file provided");
        // }
        //
        // // Generate a unique file name (optional)
        // var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) 
        //                + "_" + Guid.NewGuid() 
        //                + Path.GetExtension(imageFile.FileName);
        //
        // // Define the path to save the file (e.g., "wwwroot/images/")
        // var filePath = Path.Combine("wwwroot/images", fileName);
        //
        // // Ensure the directory exists
        // Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        //
        // // Save the file to the specified path
        // await using (var stream = new FileStream(filePath, FileMode.Create))
        // {
        //     await imageFile.CopyToAsync(stream);
        // }
        //
        // // Return the file name or path for further use (e.g., storing in database)
        // return Ok(new { fileName });
    }
    
    private async Task<byte[]> pic(IFormFile ProfilePicture)
    {
        // FullPath is the new file's path.

        using var stream = new MemoryStream();
        await ProfilePicture.CopyToAsync(stream);
        var tmpImg = stream.ToArray();

        using var collection = new MagickImageCollection(tmpImg);
        using var image = new MagickImage(tmpImg);

        image.Resize(800, 800);
        return image.ToByteArray();
    }

    
    // [HttpPost("upload")]
    // public async Task<ActionResult> UploadImage(IFormFile file)
    // {
    //     if (file == null)
    //     {
    //         return BadRequest("File is required.");
    //     }
    //
    //     // Use the repository to save the image and get the filename
    //     var imageFileName = await repo.SaveImage(file);
    //
    //     if (string.IsNullOrEmpty(imageFileName))
    //     {
    //         return BadRequest("Could not save the image.");
    //     }
    //
    //     return Ok(new { FileName = imageFileName });
    // }
}