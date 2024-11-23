namespace HotelsBookingKata.Book.Domain;

public interface IHotelService
{
    bool ExistsRoomTypeInHotel(string hotelId, string roomType);

    public int GetNumberOfRoomsByTypeAndHotel(string hotelId, string roomType);
}

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public bool ExistsRoomTypeInHotel(string hotelId, string roomType)
    {
        return hotelRepository.ExistsRoomTypeInHotel(hotelId, roomType);
    }
    
    public int GetNumberOfRoomsByTypeAndHotel(string hotelId, string roomType)
    {
        throw new NotImplementedException();
    }
}