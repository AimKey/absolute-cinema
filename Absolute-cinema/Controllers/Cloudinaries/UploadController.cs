using Common.DTOs.CloudinaryDTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Absolute_cinema.Controllers.Cloudinaries;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<UploadController> _logger;

    public UploadController(ICloudinaryService cloudinaryService, ILogger<UploadController> logger)
    {
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    [HttpPost("image")]
    public async Task<ActionResult<CloudinaryUploadResponse>> UploadImage(
        IFormFile file,
        [FromQuery] string folder = "movies",
        [FromQuery] string type = "general")
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new CloudinaryUploadResponse
                {
                    Success = false,
                    Error = "No file provided"
                });
            }

            // Determine folder based on type
            var uploadFolder = type.ToLower() switch
            {
                "poster" => "movies/posters",
                "background" => "movies/backgrounds",
                "actor" => "actors",
                "director" => "directors",
                _ => folder
            };

            var result = await _cloudinaryService.UploadImageAsync(file, uploadFolder);

            return Ok(new CloudinaryUploadResponse
            {
                Success = true,
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId,
                FileSize = result.Bytes,
                Width = result.Width,
                Height = result.Height,
                Format = result.Format
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid file upload attempt");
            return BadRequest(new CloudinaryUploadResponse
            {
                Success = false,
                Error = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading image to Cloudinary");
            return StatusCode(500, new CloudinaryUploadResponse
            {
                Success = false,
                Error = "Internal server error occurred during upload"
            });
        }
    }

    [HttpDelete("image/{publicId}")]
    public async Task<ActionResult<CloudinaryUploadResponse>> DeleteImage(string publicId)
    {
        try
        {
            // Decode the publicId (it might be URL encoded)
            publicId = Uri.UnescapeDataString(publicId);

            var result = await _cloudinaryService.DeleteImageAsync(publicId);

            if (result.Result == "ok")
            {
                return Ok(new CloudinaryUploadResponse
                {
                    Success = true
                });
            }
            else
            {
                return BadRequest(new CloudinaryUploadResponse
                {
                    Success = false,
                    Error = $"Failed to delete image: {result.Result}"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting image from Cloudinary");
            return StatusCode(500, new CloudinaryUploadResponse
            {
                Success = false,
                Error = "Internal server error occurred during deletion"
            });
        }
    }

    [HttpGet("optimize")]
    public ActionResult<string> GetOptimizedUrl(
        [FromQuery] string publicId,
        [FromQuery] int? width = null,
        [FromQuery] int? height = null,
        [FromQuery] string crop = "fill")
    {
        try
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return BadRequest("Public ID is required");
            }

            var optimizedUrl = _cloudinaryService.GetOptimizedUrl(publicId, width, height, crop);
            return Ok(optimizedUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating optimized URL");
            return StatusCode(500, "Internal server error occurred");
        }
    }

}
