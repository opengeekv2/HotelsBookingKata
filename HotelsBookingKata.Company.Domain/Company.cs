namespace HotelsBookingKata.Company.Domain;

public class Company
{
    public string CompanyId { get; }

    public Company(string companyId)
    {
        CompanyId = companyId;
    }
}