using Moq;

namespace HotelsBookingKata.Book.Domain.Tests;

using FluentAssertions;

public class BookingServiceShould
{
    [Fact]
    public void CreateBookingShouldContainAUniqueId()
    {
        Mock<IUniqueIdGenerator> uniqueIdGenerator = new();
        const string expectedId = "efeb449a-793a-4b30-9291-1f89f571d4d6";
        uniqueIdGenerator.Setup(_ => _.Generate()).Returns(expectedId);
        BookingService bookingService = new BookingService(uniqueIdGenerator.Object);
        
        var booking = bookingService.Book("95080440G", "Double", "37750641M", "25-5-2024", "27-5-2024");
        
        uniqueIdGenerator.Verify(_ =>_.Generate(), Times.Once);
        Assert.Equal(expectedId, booking.Id);
    }
    
}