
namespace Common.DTOs.Gemini;

public class GeminiAIConfig
{
    public const string SectionName = "GeminiAI";

    public string ApiKey { get; set; }
    public string BaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta";
    public string Model { get; set; } = "gemini-2.5-flash";
    public double Temperature { get; set; } = 0.7;
    public int TopK { get; set; } = 40;
    public double TopP { get; set; } = 0.95;
    public int MaxOutputTokens { get; set; } = 2048;
}
