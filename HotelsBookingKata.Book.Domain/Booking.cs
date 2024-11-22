namespace HotelsBookingKata.Book.Domain;

public record Booking(string Id, string EmployeeId, string HotelId, string RoomType, DateTime CheckIn, DateTime CheckOut);