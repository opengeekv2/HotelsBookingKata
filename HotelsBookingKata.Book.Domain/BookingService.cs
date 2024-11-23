namespace HotelsBookingKata.Book.Domain;

public class BookingService(IUniqueIdGenerator uniqueIdGenerator, IHotelService hotelService, IBookingRepository bookingRepository)
{

    public BookingOperationResultDto Book(string employeeId, string hotelId, string roomType, DateTime checkIn, DateTime checkOut, out BookingDto? bookingDto)
    {
        bookingDto = null;
        if (checkIn >= checkOut) return new CheckOutDateIsNotLaterThanCheckInDto();
        var roomsInHotel = hotelService.GetNumberOfRoomsByTypeAndHotel(hotelId, roomType);
        if (roomsInHotel == 0) return new HotelDoesNotHaveRoomTypeDto();
        if (bookingRepository.GetAll().Count(booking =>
            {
                return booking.CheckIn <= checkIn && booking.CheckOut >= checkOut
                    || booking.CheckIn > checkIn && booking.CheckIn < checkOut
                    || booking.CheckOut > checkIn && booking.CheckOut <= checkOut;
            }) == roomsInHotel) return new NoFreeRoomDto();
        bookingDto = new BookingDto(uniqueIdGenerator.Generate(), employeeId, hotelId, roomType, checkIn, checkOut);
        return new BookingSuccessfulDto();
    }
}

public abstract class BookingOperationResultDto(string Message);

public class BookingSuccessfulDto() : BookingOperationResultDto("Booking was sucessful");

public class CheckOutDateIsNotLaterThanCheckInDto() : BookingOperationResultDto("Booking unsuccessful because checkout date is later than check in");

public class HotelDoesNotHaveRoomTypeDto() : BookingOperationResultDto("Hotel does not have room type");

public class RoomTypeIsNotAllowedDto() : BookingOperationResultDto("Room type is not allowed");

public class NoFreeRoomDto() : BookingOperationResultDto("Room type is booked for the hotel for the selected dates");


