namespace HotelsBookingKata.Book.Domain;

public interface IHotelRepository
{
    bool ExistsRoomTypeInHotel(string hotelId, string roomType);
}