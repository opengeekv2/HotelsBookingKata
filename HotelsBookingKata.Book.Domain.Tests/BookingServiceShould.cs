namespace HotelsBookingKata.Book.Domain.Tests;

using NSubstitute;
using Shouldly;

public class BookingServiceShould
{
    private readonly IUniqueIdGenerator _uniqueIdGenerator;
    private readonly IHotelService _hotelService;

    private readonly IBookingRepository _bookingRepository;
    
    private readonly BookingService _bookingService;
    public BookingServiceShould() {
        _uniqueIdGenerator = Substitute.For<IUniqueIdGenerator>();
        _hotelService = Substitute.For<IHotelService>();
        _bookingRepository = Substitute.For<IBookingRepository>();
        _bookingService = new BookingService(_uniqueIdGenerator, _hotelService, _bookingRepository);
    }

    [Fact]
    public void CreateBookingShouldContainAllBookingInformationIncludingTheId()
    {
        var expectedBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        _hotelService.GetNumberOfRoomsByTypeAndHotel(expectedBookingDto.HotelId, expectedBookingDto.RoomType).Returns(1);
        const string expectedId = "efeb449a-793a-4b30-9291-1f89f571d4d6";
        _uniqueIdGenerator.Generate().Returns(expectedId);
        
        var bookingResultDto = _bookingService.Book(expectedBookingDto.EmployeeId, expectedBookingDto.HotelId, expectedBookingDto.RoomType, expectedBookingDto.CheckIn, expectedBookingDto.CheckOut, out var bookingDto);
        
        _uniqueIdGenerator.Received(1).Generate();
        bookingResultDto.ShouldBeOfType<BookingSuccessfulDto>();
        bookingDto.ShouldBe(expectedBookingDto);
    }
    
    [Fact]
    public void FailToCreateBookingIfTheCheckOutDateIsNotLaterThanTheCheckInDate()
    {
        var hotelService = Substitute.For<IHotelService>();
        var uniqueIdGenerator = Substitute.For<IUniqueIdGenerator>();
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 26));
        var bookingRepository = Substitute.For<IBookingRepository>();
        var bookingService = new BookingService(uniqueIdGenerator, hotelService, bookingRepository);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.ShouldBeOfType<CheckOutDateIsNotLaterThanCheckInDto>();
        bookingDto.ShouldBeNull();
    }
    
    [Fact]
    public void FailToCreateBookingIfTheHotelDoesNotHaveTheRoomType()
    {
        var inputBookingDto = new BookingDto("efeb449a-793a-4b30-9291-1f89f571d4d6", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 27));
        var hotelService = Substitute.For<IHotelService>();
        hotelService.GetNumberOfRoomsByTypeAndHotel(inputBookingDto.HotelId, inputBookingDto.RoomType).Returns(0);
        var uniqueIdGenerator = Substitute.For<IUniqueIdGenerator>();
        var bookingRepository = Substitute.For<IBookingRepository>();
        BookingService bookingService = new BookingService(uniqueIdGenerator, hotelService, bookingRepository);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.ShouldBeOfType<HotelDoesNotHaveRoomTypeDto>();
        bookingDto.ShouldBeNull();
    }

    
    /* començar en la data d'inici i acabar en de la data de fi
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
        _hotelService.GetNumberOfRoomsByTypeAndHotel(inputBookingDto.HotelId, inputBookingDto.RoomType).Returns(1);    
        _bookingRepository.GetAll().Returns([
            new Booking("efeb449a-793a-4b30-9291-1f89f571d4d7", "95080440G", "37750641M", "Double", new DateTime(2024, 5, 26), new DateTime(2024, 5, 29))
        ]);
        BookingService bookingService = new BookingService(_uniqueIdGenerator, _hotelService, _bookingRepository);
        
        var bookingResultDto = bookingService.Book(inputBookingDto.EmployeeId, inputBookingDto.HotelId, inputBookingDto.RoomType, inputBookingDto.CheckIn, inputBookingDto.CheckOut, out var bookingDto);
        bookingResultDto.ShouldBeOfType<NoFreeRoomDto>();
        bookingDto.ShouldBeNull();
    }
}