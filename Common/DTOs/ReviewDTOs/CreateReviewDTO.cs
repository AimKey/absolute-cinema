using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.ReviewDTOs;

public class CreateReviewDTO
{
    [Required] // Đảm bảo MovieId luôn có giá trị
    public Guid MovieId { get; set; } // Đã thay đổi từ int sang Guid

    [Required(ErrorMessage = "Đánh giá là bắt buộc.")]
    [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao.")]
    public int Rating { get; set; }

    [StringLength(500, ErrorMessage = "Bình luận không được vượt quá 500 ký tự.")]
    public string Comment { get; set; }
}
