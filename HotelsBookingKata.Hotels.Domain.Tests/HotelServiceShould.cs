using Moq;

namespace HotelsBookingKata.Hotels.Domain.Tests;

public class HotelServiceShould
{

    [Fact]
    public void AddAnHotel()
    {
        const string hotelId = "37750641M";
        const string hotelName = "Hotel 1";
        Mock<IHotelRepository> hotelRepository = new (); 
        var hotelService = new HotelService(hotelRepository.Object);
        hotelRepository.Setup(_ => _.AddHotel(It.Is<Hotel>(actualHotel =>
            actualHotel.Id == hotelId && actualHotel.Name == hotelName
        ))).Verifiable();
        
        hotelService.AddHotel(hotelId, hotelName);

        hotelRepository.Verify();
    }
}