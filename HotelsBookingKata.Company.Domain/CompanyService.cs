namespace HotelsBookingKata.Company.Domain;

public class CompanyService
{
    private readonly ICompanyRepository companyRepository;
    private readonly IEmployeeRepository employeeRepository;


    public CompanyService(ICompanyRepository companyRepositoryObject, IEmployeeRepository employeeRepositoryObject)
    {
        companyRepository = companyRepositoryObject;
        employeeRepository = employeeRepositoryObject;
    }
    
    public void AddEmployee(string companyId, string employeeId)
    {
        var company = companyRepository.GetCompany(companyId);
        var employee = new Employee(employeeId, company);
        employeeRepository.Add(employee);
    }

    public void AddCompany(string companyId)
    {
        var company = new Company(companyId);
        companyRepository.Add(company);
    }
}