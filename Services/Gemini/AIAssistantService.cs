
using Common.DTOs.Gemini;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Services.Gemini;

public class AIAssistantService : IAIAssistantService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiAIConfig _config;

    public AIAssistantService(HttpClient httpClient, IOptions<GeminiAIConfig> config)
    {
        _httpClient = httpClient;
        _config = config.Value;
    }

    public async Task<AISuggestionResponse> GetSuggestionsAsync(AISuggestionRequest request)
    {
        try
        {
            var prompt = BuildContextualPrompt(request);
            var geminiRequest = new
            {
                contents = new[]
                {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    },
                generationConfig = new
                {
                    temperature = _config.Temperature,
                    topK = _config.TopK,
                    topP = _config.TopP,
                    maxOutputTokens = _config.MaxOutputTokens
                }
            };

            var json = JsonSerializer.Serialize(geminiRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                    $"{_config.BaseUrl}/models/{_config.Model}:generateContent?key={_config.ApiKey}",
                    content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return ParseGeminiResponse(responseContent);
            }
            else
            {
                return new AISuggestionResponse
                {
                    Success = false,
                    ErrorMessage = $"API call failed: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            return new AISuggestionResponse
            {
                Success = false,
                ErrorMessage = $"Error: {ex.Message}"
            };
        }
    }

    private string BuildContextualPrompt(AISuggestionRequest request)
    {
        var promptBuilder = new StringBuilder();

        promptBuilder.AppendLine("Bạn là AI assistant của website rạp chiếu phim 'Absolute Cinema'.");
        promptBuilder.AppendLine("Website có các chức năng: xem lịch chiếu phim, đặt vé, tìm kiếm phim, xem thông tin phim, đề xuất phim.");
        promptBuilder.AppendLine();
        promptBuilder.AppendLine($"Người dùng hỏi: {request.UserPrompt}");
        promptBuilder.AppendLine();
        promptBuilder.AppendLine("Hãy trả lời câu hỏi và đưa ra các gợi ý hành động cụ thể mà người dùng có thể thực hiện trên website.");
        promptBuilder.AppendLine();
        promptBuilder.AppendLine("Trả lời theo định dạng JSON sau:");
        promptBuilder.AppendLine(@"{
              ""responseText"": ""Câu trả lời chi tiết cho người dùng"",
              ""suggestions"": [
                {
                  ""id"": ""unique_id"",
                  ""title"": ""Tiêu đề gợi ý"",
                  ""description"": ""Mô tả ngắn"",
                  ""actionType"": ""movie_schedule|movie_recommendation|movie_info|book_ticket|search_movie|view_genre"",
                  ""actionUrl"": ""URL tương ứng"",
                  ""iconClass"": ""fas fa-icon-name"",
                  ""parameters"": {
                    ""key"": ""value""
                  }
                }
              ]
            }");

        promptBuilder.AppendLine();
        promptBuilder.AppendLine("Ví dụ các actionType và actionUrl:");
        promptBuilder.AppendLine("- movie_schedule: /Schedules");
        promptBuilder.AppendLine("- movie_recommendation: /MovieRecommendation");
        promptBuilder.AppendLine("- search_movie: /Movies/Search");
        promptBuilder.AppendLine("- movie_info: /Movies/Details/{id}");
        promptBuilder.AppendLine("- book_ticket: /Booking");
        promptBuilder.AppendLine("- view_genre: /Movies/Genre/{genre}");

        return promptBuilder.ToString();
    }

    private AISuggestionResponse ParseGeminiResponse(string responseContent)
    {
        try
        {
            using var document = JsonDocument.Parse(responseContent);
            var candidates = document.RootElement.GetProperty("candidates");

            if (candidates.GetArrayLength() > 0)
            {
                var firstCandidate = candidates[0];
                var content = firstCandidate.GetProperty("content");
                var parts = content.GetProperty("parts");

                if (parts.GetArrayLength() > 0)
                {
                    var text = parts[0].GetProperty("text").GetString();

                    // Tìm và parse JSON từ response
                    var jsonStart = text.IndexOf('{');
                    var jsonEnd = text.LastIndexOf('}') + 1;

                    if (jsonStart >= 0 && jsonEnd > jsonStart)
                    {
                        var jsonText = text.Substring(jsonStart, jsonEnd - jsonStart);
                        var suggestionData = JsonSerializer.Deserialize<JsonElement>(jsonText);

                        var response = new AISuggestionResponse { Success = true };

                        if (suggestionData.TryGetProperty("responseText", out var responseTextElement))
                        {
                            response.ResponseText = responseTextElement.GetString();
                        }

                        if (suggestionData.TryGetProperty("suggestions", out var suggestionsElement))
                        {
                            foreach (var suggestionElement in suggestionsElement.EnumerateArray())
                            {
                                var suggestion = new ActionSuggestion
                                {
                                    Id = suggestionElement.TryGetProperty("id", out var id) ? id.GetString() : Guid.NewGuid().ToString(),
                                    Title = suggestionElement.TryGetProperty("title", out var title) ? title.GetString() : "",
                                    Description = suggestionElement.TryGetProperty("description", out var desc) ? desc.GetString() : "",
                                    ActionType = suggestionElement.TryGetProperty("actionType", out var actionType) ? actionType.GetString() : "",
                                    ActionUrl = suggestionElement.TryGetProperty("actionUrl", out var actionUrl) ? actionUrl.GetString() : "",
                                    IconClass = suggestionElement.TryGetProperty("iconClass", out var iconClass) ? iconClass.GetString() : "fas fa-arrow-right"
                                };

                                if (suggestionElement.TryGetProperty("parameters", out var parametersElement))
                                {
                                    foreach (var param in parametersElement.EnumerateObject())
                                    {
                                        suggestion.Parameters[param.Name] = param.Value.GetString();
                                    }
                                }

                                response.Suggestions.Add(suggestion);
                            }
                        }

                        return response;
                    }
                }
            }

            return new AISuggestionResponse
            {
                Success = false,
                ErrorMessage = "Không thể parse response từ Gemini AI"
            };
        }
        catch (Exception ex)
        {
            return new AISuggestionResponse
            {
                Success = false,
                ErrorMessage = $"Error parsing response: {ex.Message}"
            };
        }
    }

}
