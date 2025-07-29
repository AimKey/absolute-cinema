
namespace Common.DTOs.Gemini;

public class ActionSuggestion
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ActionType { get; set; } // "movie_schedule", "movie_recommendation", "movie_info", etc.
    public string ActionUrl { get; set; }
    public string IconClass { get; set; }
    public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
}
