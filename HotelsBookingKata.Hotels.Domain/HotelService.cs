namespace HotelsBookingKata.Hotel.Domain;

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
        var hotel = new Hotel(hotelId, hotelName);
        hotelRepository.Add(hotel);
    }

    public void SetRoom(string hotelId, int roomNumber, string roomType)
    {
        var hotel = hotelRepository.Get(hotelId);
        hotel.Rooms.Add(new Room(roomNumber, roomType));
        hotelRepository.Save(hotel);
    }
}
