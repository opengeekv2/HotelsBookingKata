namespace HotelsBookingKata.Hotels.Domain;

public interface IHotelRepository
{
    void AddHotel(string hotelId, string hotelName);
}