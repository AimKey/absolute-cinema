
namespace Common.DTOs.Gemini;

public class AISuggestionRequest
{
    public string UserPrompt { get; set; }
    public string Context { get; set; } // Thêm context về website cinema
}
