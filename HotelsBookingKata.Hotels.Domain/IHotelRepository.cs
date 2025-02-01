namespace HotelsBookingKata.Hotel.Domain;

public interface IHotelRepository
{
    void Add(Hotel hotel);
    Hotel? Get(string id);
    void Save(Hotel hotel);
}