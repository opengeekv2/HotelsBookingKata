namespace HotelsBookingKata.Hotels.Domain;

public interface IHotelRepository
{
    void AddHotel(Hotel hotel);
    Hotel? GetHotel(string id);
    void SaveHotel(Hotel hotel);
}