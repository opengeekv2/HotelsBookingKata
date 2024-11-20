namespace HotelsBookingKata.Book.Domain;

public interface IBookingRepository
{
    public IEnumerable<Booking> GetAll();
}