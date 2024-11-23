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
        throw new NotImplementedException();
    }

}