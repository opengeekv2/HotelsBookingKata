using FluentAssertions;
using Moq;

namespace HotelsBookingKata.Book.Domain.Tests;

public class HotelsServiceShould
{
    [Fact]
    public void ReturnTrueIfExistsRoomTypeInHotel()
    {
        Mock<IHotelRepository> hotelRepository = new();
        hotelRepository.Setup(hotelRepository => hotelRepository.ExistsRoomTypeInHotel("hotelid", "Double")).Returns(true);
        var hotelService = new HotelService(hotelRepository.Object);
        hotelService.ExistsRoomTypeInHotel("hotelid", "Double").Should().BeTrue();
    }
    
    [Fact]
    public void ReturnFalseIfDoesNotExistRoomTypeInHotel()
    {
        Mock<IHotelRepository> hotelRepository = new();
        hotelRepository.Setup(hotelRepository => hotelRepository.ExistsRoomTypeInHotel("hotelid", "Double")).Returns(false);
        var hotelService = new HotelService(hotelRepository.Object);
        hotelService.ExistsRoomTypeInHotel("hotelid", "Double").Should().BeFalse();
    }
    
    [Fact]
    public void ReturnTheNumberOfExistingRoomOfTheTypeInHotel()
    {
        Mock<IHotelRepository> hotelRepository = new();
        hotelRepository.Setup(hotelRepository => hotelRepository.GetNumberOfRoomsByTypeAndHotel("hotelid", "Double")).Returns(1);
        var hotelService = new HotelService(hotelRepository.Object);
        hotelService.GetNumberOfRoomsByTypeAndHotel("hotelid", "Double").Should().Be(1);
    }

}