namespace HotelsBookingKata.Book.Domain;

public record BookingDto(string Id, string EmployeeId, string HotelId, string RoomType, DateTime CheckIn, DateTime CheckOut);