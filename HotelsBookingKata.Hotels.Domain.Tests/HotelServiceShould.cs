using Moq;

namespace HotelsBookingKata.Hotels.Domain.Tests;

public class HotelServiceShould
{

    [Fact]
    public void AddAnHotel()
    {
        Mock<IHotelRepository> hotelRepository = new (); 
        var hotelService = new HotelService(hotelRepository.Object);
        
        hotelService.AddHotel("37750641M", "Hotel 1");
        
        hotelRepository.Verify(_ => _.AddHotel("37750641M", "Hotel 1"), Times.Once);
    }
}