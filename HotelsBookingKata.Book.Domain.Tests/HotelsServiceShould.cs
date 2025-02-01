using NSubstitute;
using Shouldly;

namespace HotelsBookingKata.Book.Domain.Tests;

public class HotelServiceShould
{
    private readonly HotelService _hotelService;
    
    private readonly IHotelRepository _hotelRepository;

    public HotelServiceShould() {
        _hotelRepository = Substitute.For<IHotelRepository>();
        _hotelService = new HotelService(_hotelRepository);
    }
    
    [Fact]
    public void ReturnTrueIfExistsRoomTypeInHotel()
    {
        _hotelRepository.ExistsRoomTypeInHotel("hotelid", "Double").Returns(true);
        var hotelService = new HotelService(_hotelRepository);
        hotelService.ExistsRoomTypeInHotel("hotelid", "Double").ShouldBeTrue();
    }
    
    [Fact]
    public void ReturnFalseIfDoesNotExistRoomTypeInHotel()
    {
        _hotelRepository.ExistsRoomTypeInHotel("hotelid", "Double").Returns(false);
        _hotelService.ExistsRoomTypeInHotel("hotelid", "Double").ShouldBeFalse();
    }
    
    [Fact]
    public void ReturnTheNumberOfExistingRoomOfTheTypeInHotel()
    {
        _hotelRepository.GetNumberOfRoomsByTypeAndHotel("hotelid", "Double").Returns(1);
        _hotelService.GetNumberOfRoomsByTypeAndHotel("hotelid", "Double").ShouldBe(1);
    }

}