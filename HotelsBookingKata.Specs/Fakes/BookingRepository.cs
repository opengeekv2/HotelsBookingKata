using HotelsBookingKata.Book.Domain;

namespace HotelsBookingKata.Hotels.Domain.Specs.Steps;

public class BookingRepository : IBookingRepository
{
    private IEnumerable<Booking> _bookings = [];

    public IEnumerable<Booking> GetAll() => _bookings;
}
