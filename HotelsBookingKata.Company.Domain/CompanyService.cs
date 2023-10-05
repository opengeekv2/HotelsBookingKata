namespace HotelsBookingKata.Company.Domain;

public class CompanyService
{
    private readonly ICompanyRepository companyRepository;

    public CompanyService(ICompanyRepository companyRepositoryObject)
    {
        companyRepository = companyRepositoryObject;
    }
    
    public void AddEmployee(string companyId, string employeeId)
    {
        throw new NotImplementedException();
    }

    public void AddCompany(string companyId)
    {
        var company = new Company(companyId);
        companyRepository.AddCompany(company);
    }
}