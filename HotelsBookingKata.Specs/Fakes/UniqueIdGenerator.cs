using HotelsBookingKata.Book.Domain;

namespace HotelsBookingKata.Hotels.Domain.Specs.Fakes;

public class UniqueIdGenerator : IUniqueIdGenerator
{
    public string Generate()
    {
        return "efeb449a-793a-4b30-9291-1f89f571d4d6";
    }
}