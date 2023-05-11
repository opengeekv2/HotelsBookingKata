using FluentAssertions;

namespace HotelsBookingKata.Hotels.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var result = Class1.Hallo();
        result.Should().BeTrue();
    }
}