namespace HotelsBookingKata.Book.Domain;

public interface IPolicyService
{
    bool IsBookingAllowed(string employeeId, string roomType);
}

public class PolicyService: IPolicyService
{
    public bool IsBookingAllowed(string employeeId, string roomType)
    {
        throw new NotImplementedException();
    }
}