namespace HotelsBookingKata.Hotels.Domain.Sepcs.Fakes;

public class HotelRepository : IHotelRepository
{
    private readonly List<Hotel> _hotels = new ();
    public void AddHotel(Hotel hotel)
    {
        _hotels.Add(hotel);
    }

    public Hotel? GetHotel(string id)
    {
        return _hotels.Find(hotel => hotel.Id == id);
    }

    public void SaveHotel(Hotel hotel)
    {
        _hotels.Remove(hotel);
        _hotels.Add(hotel);
    }
}