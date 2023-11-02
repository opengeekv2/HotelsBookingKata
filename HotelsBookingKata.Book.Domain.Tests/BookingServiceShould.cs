using Moq;

namespace HotelsBookingKata.Book.Domain.Tests;

using FluentAssertions;

public class BookingServiceShould
{
    [Fact]
    public void CreateBookingShouldContainAUniqueId()
    {
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        uniqueIdGenerator.Setup(_ => _.Generate()).Returns("efeb449a-793a-4b30-9291-1f89f571d4d6");
        BookingService bookingService = new BookingService();
        var booking = bookingService.Book("95080440G", "Double", "37750641M", "25-5-2024", "27-5-2024");
        Assert.Equal("efeb449a-793a-4b30-9291-1f89f571d4d6", booking.Id);
    }
}