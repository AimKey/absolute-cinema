using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IReviewService
{
    IEnumerable<Review> GetAll();
    Review GetById(Guid id);
    void Add(Review review);
    void Update(Review review);
    void Delete(Guid id);
    void Delete(Review review);
} 