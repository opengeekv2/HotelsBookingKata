namespace HotelsBookingKata.Hotels.Domain.Specs.Fakes;

public class HotelRepository : IHotelRepository, Book.Domain.IHotelRepository
{
    private readonly List<Hotel> _hotels = new ();
    public void Add(Hotel hotel)
    {
        _hotels.Add(hotel);
    }

    public Hotel? Get(string id)
    {
        return _hotels.Find(hotel => hotel.Id == id);
    }

    public void Save(Hotel hotel)
    {
        _hotels.Remove(hotel);
        _hotels.Add(hotel);
    }

    public bool ExistsRoomTypeInHotel(string hotelId, string roomType)
    {
        return true;
    }
}