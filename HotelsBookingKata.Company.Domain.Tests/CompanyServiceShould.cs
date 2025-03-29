using NSubstitute;

namespace HotelsBookingKata.Company.Domain.Tests;

public class CompanyServiceShould
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyService _companyService;

    public CompanyServiceShould()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _companyService = new CompanyService(_companyRepository, _employeeRepository);
    }

    [Fact]
    public void CreateACompany()
    {
        var companyService = new CompanyService(_companyRepository, _employeeRepository);
        var companyId = "59500657W";
        var expectedCompany = new Company(companyId);

        companyService.AddCompany(companyId);

        _companyRepository.Received().Add(Arg.Is<Company>(company => company.Id == companyId));
    }

    [Fact]
    public void CreateAnEmployee()
    {
        var companyService = new CompanyService(_companyRepository, _employeeRepository);
        var companyId = "59500657W";
        var employeeId = "95080440G";
        var company = new Company(companyId);
        var expectedEmployee = new Employee(employeeId, company);

        _companyRepository.GetCompany(companyId).Returns(new Company("59500657W"));

        companyService.AddEmployee(companyId, employeeId);

        _employeeRepository.Received().Add(Arg.Is<Employee>(employee => employee.Id == employeeId));
    }
}
