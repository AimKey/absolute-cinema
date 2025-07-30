using Common.DTOs.Gemini;

namespace Services.Gemini;

public interface IAIAssistantService
{
    Task<AISuggestionResponse> GetSuggestionsAsync(AISuggestionRequest request);
}
