using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;

namespace Services;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile file, string folder = "movies");
    Task<DeletionResult> DeleteImageAsync(string publicId);
    string GetOptimizedUrl(string publicId, int? width = null, int? height = null, string crop = "fill");
}
