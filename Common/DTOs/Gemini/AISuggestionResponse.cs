
namespace Common.DTOs.Gemini;

public class AISuggestionResponse
{
    public string ResponseText { get; set; }
    public List<ActionSuggestion> Suggestions { get; set; } = new List<ActionSuggestion>();
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
