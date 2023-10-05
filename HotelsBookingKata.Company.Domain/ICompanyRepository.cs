namespace HotelsBookingKata.Company.Domain;

public interface ICompanyRepository
{
    public void AddCompany(Company company);
    Company GetCompany(string companyId);
}