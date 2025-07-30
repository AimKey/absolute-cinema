using Common.DTOs.Gemini;
using Microsoft.AspNetCore.Mvc;
using Services.Gemini;

namespace Absolute_cinema.Controllers.Gemini;

public class AIAssistantController : Controller
{
    private readonly IAIAssistantService _aiAssistantService;

    public AIAssistantController(IAIAssistantService aiAssistantService)
    {
        _aiAssistantService = aiAssistantService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetSuggestions([FromBody] AISuggestionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserPrompt))
        {
            return Json(new AISuggestionResponse
            {
                Success = false,
                ErrorMessage = "Vui lòng nhập câu hỏi"
            });
        }

        var response = await _aiAssistantService.GetSuggestionsAsync(request);
        return Json(response);
    }

    [HttpPost]
    public IActionResult ExecuteAction([FromBody] ActionSuggestion suggestion)
    {
        // Xử lý các loại action khác nhau
        switch (suggestion.ActionType.ToLower())
        {
            case "movie_schedule":
                return Json(new { redirectUrl = "/Schedules" });

            case "movie_recommendation":
                return Json(new { redirectUrl = "/Movies" });

            case "search_movie":
                var searchQuery = suggestion.Parameters.ContainsKey("query") ? suggestion.Parameters["query"] : "";
                return Json(new { redirectUrl = $"/Movies/Search?query={searchQuery}" });

            case "movie_info":
                var movieId = suggestion.Parameters.ContainsKey("movieId") ? suggestion.Parameters["movieId"] : "";
                return Json(new { redirectUrl = $"/Movies/Details/{movieId}" });

            case "book_ticket":
                return Json(new { redirectUrl = "/Booking" });

            case "view_genre":
                var genre = suggestion.Parameters.ContainsKey("genre") ? suggestion.Parameters["genre"] : "";
                return Json(new { redirectUrl = $"/Movies/Genre/{genre}" });

            default:
                return Json(new { redirectUrl = suggestion.ActionUrl });
        }
    }

}
