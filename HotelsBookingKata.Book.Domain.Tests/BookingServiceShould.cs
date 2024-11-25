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
        hotelService.Setup(hotelService => hotelService.GetNumberOfRoomsByTypeAndHotel(expectedBookingDto.HotelId, expectedBookingDto.RoomType)).Returns(1);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        const string expectedId = "efeb449a-793a-4b30-9291-1f89f571d4d6";
        uniqueIdGenerator.Setup(uniqueIdGenerator => uniqueIdGenerator.Generate()).Returns(expectedId);
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        var bookingResultDto = bookingService.Book(expectedBookingDto.EmployeeId, expectedBookingDto.HotelId, expectedBookingDto.RoomType, expectedBookingDto.CheckIn, expectedBookingDto.CheckOut, out var bookingDto);
        
        uniqueIdGenerator.Verify(_ =>_.Generate(), Times.Once);
        bookingResultDto.Should().BeOfType<BookingSuccessfulDto>();
        bookingDto.Should().BeEquivalentTo(expectedBookingDto);
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
    public void FailToCreateBookingIfTheHotelDoesNotHaveTheRoomType()
    {
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        Mock<IHotelService> hotelService = new();
        hotelService.Setup(hotelService => hotelService.GetNumberOfRoomsByTypeAndHotel(inputBookingDto.HotelId, inputBookingDto.RoomType)).Returns(0);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        Mock<IBookingRepository> bookingRepository = new();
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.Should().BeOfType<HotelDoesNotHaveRoomTypeDto>();
        bookingDto.Should().BeNull();
    }

    /*
     * començar en la data d'inici i acabar en de la data de fi
       començar abans de la data d'inici i acabar passat la data d'inici
           començar abans la data de fi i acabar passat la data de fi
     */
    public static IEnumerable<object[]> Bookings => new List<object[]>
    {
        new object[] {
            new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double",
                new DateTime(2024, 5, 26), new DateTime(2024, 5, 29)) },
        new object[] {
            new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double",
        new DateTime(2024, 5, 27), new DateTime(2024, 5, 28)) },
        new object[] {
            new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double",
                new DateTime(2024, 5, 25), new DateTime(2024, 5, 27)) },
        new object[] {
            new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double",
                new DateTime(2024, 5, 28), new DateTime(2024, 5, 30))
        }
    };
    
    [Theory]
    [MemberData(nameof(Bookings))]
    public void FailToCreateBookingIfTheresNoFreeRoomOfTheTypeForTheDates(BookingDto inputBookingDto)
    {
        Mock<IHotelService> hotelService = new();
        hotelService.Setup(hotelService => hotelService.GetNumberOfRoomsByTypeAndHotel(inputBookingDto.HotelId, inputBookingDto.RoomType)).Returns(1);
        Mock<IPolicyService> policyService = new();
        policyService.Setup(policyService => policyService.IsBookingAllowed(inputBookingDto.EmployeeId, inputBookingDto.RoomType)).Returns(false);
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        Mock<IBookingRepository> bookingRepository = new();
        bookingRepository.Setup(bookingRepository => bookingRepository.GetAll()).Returns([
            new Booking("efeb449a-793a-4b30-9291-1f89f571d4d7", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 29))
        ]);
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object, hotelService.Object, bookingRepository.Object);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.Should().BeOfType<NoFreeRoomDto>();
        bookingDto.Should().BeNull();
    }
}