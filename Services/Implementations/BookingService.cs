using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookingRepository.Get().ToList();
    }

    public Booking GetById(Guid id)
    {
        return _bookingRepository.GetByID(id);
    }

    public void Add(Booking booking)
    {
        _bookingRepository.Insert(booking);
        _bookingRepository.Save();
    }

    public void Update(Booking booking)
    {
        _bookingRepository.Update(booking);
        _bookingRepository.Save();
    }

    public void Delete(Guid id)
    {
        _bookingRepository.Delete(id);
        _bookingRepository.Save();
    }

    public void Delete(Booking booking)
    {
        _bookingRepository.Delete(booking);
        _bookingRepository.Save();
    }
} 