namespace Common.Filters.Movies;

public class MovieFilterCriteria
{
    public string Search { get; set; } = "";
    public string[] Tags { get; set; } = Array.Empty<string>();
    public string Country { get; set; } = "";
    public string Quality { get; set; } = "";
    public string Year { get; set; } = "";
    public string Rating { get; set; } = "";
    public string Price { get; set; } = "";
    public bool? Featured { get; set; }
    public bool? NewRelease { get; set; }
    public string Sort { get; set; } = "newest";
    public int Page { get; set; } = 1;
}
