
using Absolute_cinema.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Absolute_cinema.Controllers.Movies;

    public class MoviesController : Controller
    {
        public IActionResult Index(string search = "", string[] tags = null, string country = "", string quality = "", int page = 1)
        {
            // Mock data - trong thực tế sẽ lấy từ database
            var movies = GetMockMovies();

            // Apply filters
            if (!string.IsNullOrEmpty(search))
            {
                movies = movies.Where(m => m.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         m.OriginalTitle.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

        //if (tags != null && tags.Any())
        //{
        //    movies = movies.Where(m => tags.Any(tag => m.MovieTags.Contains(tag))).ToList();
        //}

        if (!string.IsNullOrEmpty(country))
            {
                movies = movies.Where(m => m.Country == country).ToList();
            }

            if (!string.IsNullOrEmpty(quality))
            {
                movies = movies.Where(m => m.Quality == quality).ToList();
            }

            // Pagination
            int pageSize = 12;
            var totalMovies = movies.Count();
            var totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

            movies = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;
            ViewBag.SelectedTags = tags ?? new string[0];
            ViewBag.Country = country;
            ViewBag.Quality = quality;
            ViewBag.AllTags = GetAllTags();

            return View(movies);
        }

        private List<MovieVM> GetMockMovies()
        {
            var tags = GetAllTags();

            return new List<MovieVM>
            {
                new MovieVM
                {
                    Id = Guid.NewGuid(),
                    Title = "Avengers: Endgame",
                    OriginalTitle = "Avengers: Endgame",
                    Description = "Sau những sự kiện tàn khốc của Infinity War, vũ trụ đang trong tình trạng hỗn loạn...",
                    Duration = 181,
                    ReleaseDate = new DateTime(2019, 4, 26),
                    Country = "Mỹ",
                    PosterURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    BackgroundURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    TrailerURL = "https://youtube.com/watch?v=example",
                    ImdbRating = 8.4m,
                    AgeRestriction = 13,
                    Status = "Active",
                    Language = "Tiếng Anh",
                    Quality = "4K",
                    ViewCount = 2500000,
                    IsFeatured = true,
                    IsNewRelease = false,
                    Price = 50000,
                    IsFree = false,
                    MovieTags = new List<MovieTagVM>
                    {
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Hành động") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Khoa học viễn tưởng") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Phiêu lưu") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Siêu anh hùng") }
                    }
                },
                new MovieVM
                {
                    Id = Guid.NewGuid(),
                    Title = "Parasite",
                    OriginalTitle = "기생충",
                    Description = "Một gia đình nghèo khó lên kế hoạch xâm nhập vào cuộc sống của một gia đình giàu có...",
                    Duration = 132,
                    ReleaseDate = new DateTime(2019, 5, 30),
                    Country = "Hàn Quốc",
                    PosterURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    BackgroundURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    TrailerURL = "https://youtube.com/watch?v=example",
                    ImdbRating = 8.6m,
                    AgeRestriction = 16,
                    Status = "Active",
                    Language = "Tiếng Hàn",
                    Quality = "HD",
                    ViewCount = 1800000,
                    IsFeatured = true,
                    IsNewRelease = false,
                    Price = 40000,
                    IsFree = false,
                    MovieTags = new List<MovieTagVM>
                    {
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Tâm lý") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Chính kịch") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Kinh dị") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Hài đen") }
                    }
                },
                new MovieVM
                {
                    Id = Guid.NewGuid(),
                    Title = "Spirited Away",
                    OriginalTitle = "千と千尋の神隠し",
                    Description = "Chihiro, một cô bé 10 tuổi, phải làm việc trong thế giới thần linh để cứu cha mẹ...",
                    Duration = 125,
                    ReleaseDate = new DateTime(2001, 7, 20),
                    Country = "Nhật Bản",
                    PosterURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    BackgroundURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    TrailerURL = "https://youtube.com/watch?v=example",
                    ImdbRating = 9.2m,
                    AgeRestriction = 0,
                    Status = "Active",
                    Language = "Tiếng Nhật",
                    Quality = "HD",
                    ViewCount = 3200000,
                    IsFeatured = true,
                    IsNewRelease = false,
                    Price = 0,
                    IsFree = true,
                    MovieTags = new List<MovieTagVM>
                    {
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Hoạt hình") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Gia đình") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Phiêu lưu") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Kỳ ảo") }
                    }
                },
                new MovieVM
                {
                    Id = Guid.NewGuid(),
                    Title = "The Dark Knight",
                    OriginalTitle = "The Dark Knight",
                    Description = "Batman phải đối mặt với Joker, một tên tội phạm tâm thần nguy hiểm...",
                    Duration = 152,
                    ReleaseDate = new DateTime(2008, 7, 18),
                    Country = "Mỹ",
                    PosterURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    BackgroundURL = "https://static.nutscdn.com/vimg/300-0/10b33f7b9386bc265a085073c7aca6d5.jpg",
                    TrailerURL = "https://youtube.com/watch?v=example",
                    ImdbRating = 9.0m,
                    AgeRestriction = 13,
                    Status = "Active",
                    Language = "Tiếng Anh",
                    Quality = "4K",
                    ViewCount = 4200000,
                    IsFeatured = true,
                    IsNewRelease = false,
                    Price = 45000,
                    IsFree = false,
                    MovieTags = new List<MovieTagVM>
                    {
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Hành động") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Tội phạm") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Chính kịch") },
                        new MovieTagVM { Tag = tags.First(t => t.Name == "Siêu anh hùng") }
                    }
                }
            };
        }

        private List<TagVM> GetAllTags()
        {
            return new List<TagVM>
            {
                new TagVM { Id = Guid.NewGuid(), Name = "Hành động", Color = "#ef4444" },
                new TagVM { Id = Guid.NewGuid(), Name = "Tâm lý", Color = "#8b5cf6" },
                new TagVM { Id = Guid.NewGuid(), Name = "Hoạt hình", Color = "#06b6d4" },
                new TagVM { Id = Guid.NewGuid(), Name = "Kinh dị", Color = "#dc2626" },
                new TagVM { Id = Guid.NewGuid(), Name = "Tình cảm", Color = "#ec4899" },
                new TagVM { Id = Guid.NewGuid(), Name = "Hài hước", Color = "#f59e0b" },
                new TagVM { Id = Guid.NewGuid(), Name = "Khoa học viễn tưởng", Color = "#10b981" },
                new TagVM { Id = Guid.NewGuid(), Name = "Phiêu lưu", Color = "#f97316" },
                new TagVM { Id = Guid.NewGuid(), Name = "Chính kịch", Color = "#6b7280" },
                new TagVM { Id = Guid.NewGuid(), Name = "Tội phạm", Color = "#374151" },
                new TagVM { Id = Guid.NewGuid(), Name = "Gia đình", Color = "#84cc16" },
                new TagVM { Id = Guid.NewGuid(), Name = "Kỳ ảo", Color = "#a855f7" },
                new TagVM { Id = Guid.NewGuid(), Name = "Siêu anh hùng", Color = "#3b82f6" },
                new TagVM { Id = Guid.NewGuid(), Name = "Hài đen", Color = "#1f2937" },
                new TagVM { Id = Guid.NewGuid(), Name = "Lịch sử", Color = "#92400e" },
                new TagVM { Id = Guid.NewGuid(), Name = "Chiến tranh", Color = "#7c2d12" },
                new TagVM { Id = Guid.NewGuid(), Name = "Thể thao", Color = "#059669" },
                new TagVM { Id = Guid.NewGuid(), Name = "Âm nhạc", Color = "#db2777" }
            };
        }
    }

