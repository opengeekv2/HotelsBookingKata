namespace HotelsBookingKata.Book.Domain;

public interface IHotelRepository
{
    bool ExistsRoomTypeInHotel(string hotelId, string roomType);

    int GetNumberOfRoomsByTypeAndHotel(string hotelId, string roomType);
}
