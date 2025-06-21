using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public IEnumerable<Review> GetAll()
    {
        return _reviewRepository.Get().ToList();
    }

    public Review GetById(Guid id)
    {
        return _reviewRepository.GetByID(id);
    }

    public void Add(Review review)
    {
        _reviewRepository.Insert(review);
        _reviewRepository.Save();
    }

    public void Update(Review review)
    {
        _reviewRepository.Update(review);
        _reviewRepository.Save();
    }

    public void Delete(Guid id)
    {
        _reviewRepository.Delete(id);
        _reviewRepository.Save();
    }

    public void Delete(Review review)
    {
        _reviewRepository.Delete(review);
        _reviewRepository.Save();
    }
} 