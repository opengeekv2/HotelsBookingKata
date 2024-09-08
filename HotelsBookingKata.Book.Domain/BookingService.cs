using Jane;

namespace HotelsBookingKata.Book.Domain;

public class BookingService(IUniqueIdGenerator uniqueIdGenerator, IHotelService hotelService, IPolicyService policyService)
{

    public BookingOperationResultDto Book(string employeeId, string hotelId, string roomType, DateTime checkIn, DateTime checkOut, out BookingDto? bookingDto)
    {
        bookingDto = null;
        if (checkIn >= checkOut) return new CheckOutDateIsNotLaterThanCheckInDto();
        if (!hotelService.ExistsRoomTypeInHotel(hotelId, roomType)) return new HotelDoesNotHaveRoomTypeDto();
        bookingDto = new BookingDto(uniqueIdGenerator.Generate(), employeeId, hotelId, roomType, checkIn, checkOut);
        return new BookingSuccessfulDto();
    }
}

public abstract class BookingOperationResultDto(string Message);

public class BookingSuccessfulDto() : BookingOperationResultDto("Booking was sucessful");

public class CheckOutDateIsNotLaterThanCheckInDto() : BookingOperationResultDto("Booking unsuccessful because checkout date is later than check in");

public class HotelDoesNotHaveRoomTypeDto() : BookingOperationResultDto("Hotel does not have room type");