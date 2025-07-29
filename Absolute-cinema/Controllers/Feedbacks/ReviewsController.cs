using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DataAccessObjects;
using Services.Interfaces;
using Common.DTOs.ReviewDTOs;

namespace Absolute_cinema.Controllers.Feedbacks;

public class ReviewsController : Controller
{
    private readonly IUserService _userService;
    private readonly IReviewService _reviewService;
    public ReviewsController(IUserService userService, IReviewService reviewService)
    {
        _userService = userService;
        _reviewService = reviewService;
    }

    [HttpGet]
    public IActionResult GetUserMovieFeedback(Guid movieId) // Đã thay đổi từ int sang Guid
    {
        var userId = GetCurrentUser().Id;

        var feedback = _reviewService.GetAll()
            .Where(r => r.MovieId == movieId && r.UserId == userId)
            .Select(r => new { r.Rating, r.Content, r.CreatedAt, r.UpdatedAt })
            .FirstOrDefault();

        if (feedback == null)
        {
            return NotFound(new { message = "Không tìm thấy feedback cho phim này." });
        }

        return Json(feedback);
    }

    [HttpPost]
    public IActionResult SubmitMovieFeedback([FromBody] CreateReviewDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        var userId = GetCurrentUser().Id;

        var existingFeedback = _reviewService.GetAll()
            .FirstOrDefault(r => r.MovieId == model.MovieId && r.UserId == userId);

        if (existingFeedback != null)
        {
            existingFeedback.Rating = model.Rating;
            existingFeedback.Content = model.Comment;
            existingFeedback.UpdatedAt = DateTime.UtcNow;
            //_context.Reviews.Update(existingFeedback);
            //await _context.SaveChangesAsync();

            _reviewService.Update(existingFeedback);

            return Json(new { success = true, message = "Feedback của bạn đã được cập nhật thành công!" });
        }
        else
        {
            var newFeedback = new Review
            {
                MovieId = model.MovieId,
                UserId = userId,
                Rating = model.Rating,
                Content = model.Comment,
                CreatedAt = DateTime.UtcNow
            };
            //_context.Reviews.Add(newFeedback);
            //await _context.SaveChangesAsync();

            _reviewService.Add(newFeedback);

            return Json(new { success = true, message = "Feedback của bạn đã được gửi thành công!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMovieFeedback([FromBody] DeleteReviewDTO model) // Sử dụng model riêng cho delete
    {
        var userId = GetCurrentUser().Id;

        var feedbackToDelete = _reviewService.GetAll()
            .FirstOrDefault(r => r.MovieId == model.MovieId && r.UserId == userId);

        if (feedbackToDelete == null)
        {
            return NotFound(new { message = "Không tìm thấy feedback để xóa." });
        }

        _reviewService.Delete(feedbackToDelete);

        return Json(new { success = true, message = "Feedback đã được xóa thành công!" });
    }

    private User GetCurrentUser()
    {
        var userNameString = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(userNameString))
        {
            return null; // or handle the case where the user is not logged in
        }
        var user = _userService.GetUserByUserName(userNameString);
        return user;
    }

}
