using Moq;

namespace HotelsBookingKata.Book.Domain.Tests;

using FluentAssertions;

public class BookingServiceShould
{
    [Fact]
    public void CreateBookingShouldContainAllBookingInformationIncludingTheId()
    {
        var expectedBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        Mock<IHotelService> hotelService = new();
        hotelService.Setup(hotelService => hotelService.ExistsRoomTypeInHotel(expectedBookingDto.HotelId, expectedBookingDto.RoomType)).Returns(true);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        const string expectedId = "efeb449a-793a-4b30-9291-1f89f571d4d6";
        uniqueIdGenerator.Setup(uniqueIdGenerator => uniqueIdGenerator.Generate()).Returns(expectedId);
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        var bookingResultDto = bookingService.Book(expectedBookingDto.EmployeeId, expectedBookingDto.HotelId, expectedBookingDto.RoomType, expectedBookingDto.CheckIn, expectedBookingDto.CheckOut, out var bookingDto);
        
        uniqueIdGenerator.Verify(_ =>_.Generate(), Times.Once);
        bookingDto.Should().BeEquivalentTo(expectedBookingDto);
        bookingResultDto.Should().BeOfType<BookingSuccessfulDto>();
    }
    
    [Fact]
    public void FailToCreateBookingIfTheCheckOutDateIsNotLaterThanTheCheckInDate()
    {
        Mock<IHotelService> hotelService = new();
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 26));
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.Should().BeOfType<CheckOutDateIsNotLaterThanCheckInDto>();
        bookingDto.Should().BeNull();
    }
    
    [Fact]
    public void FailToCreateBookingIfTheHotelDoesNotHAveTheRoomType()
    {
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        Mock<IHotelService> hotelService = new();
        hotelService.Setup(hotelService => hotelService.ExistsRoomTypeInHotel(inputBookingDto.HotelId, inputBookingDto.RoomType)).Returns(false);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.Should().BeOfType<HotelDoesNotHaveRoomTypeDto>();
        bookingDto.Should().BeNull();
    }

    [Fact]
    public void FailToCreateBookingIfTheresNoFreeRoomOfTheTypeForTheDates()
    {
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        Mock<IHotelService> hotelService = new();
        hotelService.Setup(hotelService => hotelService.ExistsRoomTypeInHotel(inputBookingDto.HotelId, inputBookingDto.RoomType)).Returns(true);
        Mock<IPolicyService> policyService = new();
        policyService.Setup(policyService => policyService.IsBookingAllowed(inputBookingDto.EmployeeId, inputBookingDto.RoomType)).Returns(false);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.Should().BeOfType<NoFreeRoomDto>();
        bookingDto.Should().BeNull();
    }
}