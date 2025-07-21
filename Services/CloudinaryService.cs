
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Common.DTOs.CloudinaryDTOs;

namespace Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinarySettings _settings;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        _settings = settings.Value;

        var account = new Account(
            _settings.CloudName,
            _settings.ApiKey,
            _settings.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file, string folder = "movies")
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is required", nameof(file));
        }

        // Validate file type
        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
        if (!allowedTypes.Contains(file.ContentType.ToLower()))
        {
            throw new ArgumentException("Only JPEG, PNG, and WebP images are allowed");
        }

        // Validate file size (max 10MB)
        if (file.Length > 10 * 1024 * 1024)
        {
            throw new ArgumentException("File size must be less than 10MB");
        }

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folder,
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
        {
            throw new Exception($"Cloudinary upload failed: {result.Error.Message}");
        }

        return result;
    }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
        {
            throw new ArgumentException("Public ID is required", nameof(publicId));
        }

        var deleteParams = new DeletionParams(publicId);
        return await _cloudinary.DestroyAsync(deleteParams);
    }

    public string GetOptimizedUrl(string publicId, int? width = null, int? height = null, string crop = "fill")
    {
        if (string.IsNullOrEmpty(publicId))
        {
            return string.Empty;
        }

        var transformation = new Transformation();

        if (width.HasValue)
        {
            transformation = transformation.Width(width.Value);
        }

        if (height.HasValue)
        {
            transformation = transformation.Height(height.Value);
        }

        if (!string.IsNullOrEmpty(crop))
        {
            transformation = transformation.Crop(crop);
        }

        // Always optimize
        transformation = transformation.Quality("auto:good").FetchFormat("auto");

        return _cloudinary.Api.UrlImgUp.Transform(transformation).BuildUrl(publicId);
    }

}

