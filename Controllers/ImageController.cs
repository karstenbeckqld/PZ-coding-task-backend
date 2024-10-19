using cheeseBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace cheeseBackend.Controllers;

// The Image controller handles the upload and saving of an image. I've decided for a separate controller for this
// functionality, also to separate concerns. Not every cheese addition will have an image attached. The Image controller
// injects the ImageManager which itself implements the IImageRepository interface.  
[ApiController]
[Route("api/[controller]")]
public class ImageController: ControllerBase
{
    
    private readonly ImageManager _imageManager;
    public ImageController(ImageManager imageManager)
    {
        _imageManager = imageManager;
    }
    
    [HttpPost("upload")] // Upload an image and save it to the public/images directory.
    public async Task<IActionResult> UploadImage(IFormFile imageFile)
    {
        
        var response = await _imageManager.SaveImage(imageFile);
        
        return Ok(response);
        
    }
}