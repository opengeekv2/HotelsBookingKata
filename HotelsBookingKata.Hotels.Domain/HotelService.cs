namespace HotelsBookingKata.Hotels.Domain;

public interface IHotelService
{
    void AddHotel(string hotelId, string hotelName);
}

public class HotelService : IHotelService
{
    private readonly IHotelRepository hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        this.hotelRepository = hotelRepository;
    }

    public void AddHotel(string hotelId, string hotelName)
    {
        hotelRepository.AddHotel(hotelId, hotelName);
    }
}