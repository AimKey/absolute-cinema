
namespace Common.DTOs.CloudinaryDTOs;

public class CloudinaryUploadResponse
{
    public bool Success { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public string? Error { get; set; }
    public long? FileSize { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Format { get; set; }
}
