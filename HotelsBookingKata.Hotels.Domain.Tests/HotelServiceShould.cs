using NSubstitute;

namespace HotelsBookingKata.Hotel.Domain.Tests;

public class HotelServiceShould
{
    private readonly IHotelRepository _hotelRepository;
    private readonly HotelService _hotelService;

    public HotelServiceShould()
    {
        _hotelRepository = Substitute.For<IHotelRepository>();
        _hotelService = new HotelService(_hotelRepository);
    }

    [Fact]
    public void AddAnHotel()
    {
        const string hotelId = "37750641M";
        const string hotelName = "Hotel 1";

        _hotelService.AddHotel(hotelId, hotelName);

        _hotelRepository
            .Received()
            .Add(Arg.Is<Hotel>(hotel => hotel.Id == hotelId && hotel.Name == hotelName));
    }

    [Fact]
    public void AddARoom()
    {
        const string hotelId = "37750641M";
        const string hotelName = "Hotel 1";
        const int roomNumber = 101;
        const string roomType = "Double";
        _hotelRepository.Get(hotelId).Returns(new Hotel(hotelId, hotelName));

        _hotelService.SetRoom(hotelId, roomNumber, roomType);

        _hotelRepository
            .Received()
            .Save(
                Arg.Is<Hotel>(hotel =>
                    hotel.Rooms.Any()
                    && hotel.Rooms[0].Number == roomNumber
                    && hotel.Rooms[0].Type == roomType
                )
            );
    }
}
