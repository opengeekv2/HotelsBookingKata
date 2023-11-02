namespace HotelsBookingKata.Book.Domain;

public class BookingService
{
    private readonly IUniqueIdGenerator uniqueIdGenerator;

    public BookingService(IUniqueIdGenerator uniqueIdGenerator)
    {
        this.uniqueIdGenerator = uniqueIdGenerator;
    }

    public Booking Book(string employeeId, string hotelId, string roomType, string checkIn, string checkOut)
    {
        return new Booking(uniqueIdGenerator.Generate());
    }
}