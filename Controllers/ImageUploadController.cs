using Microsoft.AspNetCore.Mvc;

namespace inventoryEyeBack;
[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile photo)
    {
        if (photo == null || photo.Length == 0) {
            return BadRequest("No file uploaded.");
        }
        //<current directory>\wwwroot\uploads
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        var filePath = Path.Combine(uploads, photo.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{photo.FileName}";
        return Ok(new { fileUrl });
    }
}
