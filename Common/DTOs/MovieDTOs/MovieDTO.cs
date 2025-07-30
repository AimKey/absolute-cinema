
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.MovieDTOs;

public class MovieDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
    [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
    public string Title { get; set; }

    [StringLength(200, ErrorMessage = "Tiêu đề gốc không được vượt quá 200 ký tự")]
    public string OriginalTitle { get; set; }

    [Required(ErrorMessage = "Mô tả là bắt buộc")]
    [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Thời lượng là bắt buộc")]
    [Range(1, 1000, ErrorMessage = "Thời lượng phải từ 1 đến 1000 phút")]
    public int Duration { get; set; }

    [Required(ErrorMessage = "Ngày phát hành là bắt buộc")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Quốc gia là bắt buộc")]
    [StringLength(100, ErrorMessage = "Quốc gia không được vượt quá 100 ký tự")]
    public string Country { get; set; }

    [Required(ErrorMessage = "URL poster là bắt buộc")]
    [Url(ErrorMessage = "URL poster không hợp lệ")]
    public string PosterURL { get; set; }

    [Url(ErrorMessage = "URL background không hợp lệ")]
    public string BackgroundURL { get; set; }

    [Url(ErrorMessage = "URL trailer không hợp lệ")]
    public string TrailerURL { get; set; }

    [Range(0, 10, ErrorMessage = "Đánh giá IMDB phải từ 0 đến 10")]
    public decimal ImdbRating { get; set; }

    [StringLength(10, ErrorMessage = "Độ tuổi không được vượt quá 10 ký tự")]
    public string AgeRestriction { get; set; }

    [Required(ErrorMessage = "Trạng thái là bắt buộc")]
    public string Status { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }

    // Danh sách tags được chọn (dùng cho form)
    public List<string> SelectedTags { get; set; } = new List<string>();
    public List<Guid> SelectedDirectorIds { get; set; } = new List<Guid>();
    public List<Guid> SelectedActorIds { get; set; } = new List<Guid>();

    // Danh sách tags hiển thị (chỉ đọc)
    public List<string> Tags { get; set; } = new List<string>();
}

