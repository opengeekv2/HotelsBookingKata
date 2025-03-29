namespace HotelsBookingKata.Company.Domain;

public interface ICompanyRepository
{
    public void Add(Company company);
    Company? GetCompany(string companyId);
}
