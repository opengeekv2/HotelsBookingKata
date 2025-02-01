using Moq;

namespace HotelsBookingKata.Hotel.Domain.Tests;

public class HotelServiceShould
{

    [Fact]
    public void AddAnHotel()
    {
        const string hotelId = "37750641M";
        const string hotelName = "Hotel 1";
        Mock<IHotelRepository> hotelRepository = new (); 
        var hotelService = new HotelService(hotelRepository.Object);
        hotelRepository.Setup(_ => _.Add(It.Is<Hotel>(actualHotel =>
            actualHotel.Id == hotelId && actualHotel.Name == hotelName
        ))).Verifiable();
        
        hotelService.AddHotel(hotelId, hotelName);

        hotelRepository.Verify();
    }
    
    [Fact]
    public void AddARoom()
    {
        const string hotelId = "37750641M";
        const string hotelName = "Hotel 1";
        const int roomNumber = 101;
        const string roomType = "Double";
        Mock<IHotelRepository> hotelRepository = new (); 
        var hotelService = new HotelService(hotelRepository.Object);
        hotelRepository.Setup(_ => _.Get(hotelId)).Returns(new Hotel(hotelId, hotelName));
        hotelRepository.Setup(_ => _.Save(It.Is<Hotel>(actualHotel =>
            actualHotel.Rooms.Any() && actualHotel.Rooms[0].Number == roomNumber && actualHotel.Rooms[0].Type == roomType
        ))).Verifiable();
        
        hotelService.SetRoom(hotelId, roomNumber, roomType);

        hotelRepository.Verify();
    }
}