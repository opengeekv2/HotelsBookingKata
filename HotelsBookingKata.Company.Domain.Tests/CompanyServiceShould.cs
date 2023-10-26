using Moq;

namespace HotelsBookingKata.Company.Domain.Tests;

public class CompanyServiceShould
{
    [Fact]
    public void CreateACompany()
    {
        Mock<ICompanyRepository> companyRepository = new();
        Mock<IEmployeeRepository> employeeRepository = new();
        var companyService = new CompanyService(companyRepository.Object, employeeRepository.Object);
        var companyId = "59500657W";
        var expectedCompany = new Company(companyId);
        companyRepository.Setup(m =>m.Add(It.Is<Company>(company =>
            company.Id == expectedCompany.Id))).Verifiable();
        
        companyService.AddCompany(companyId);
        
        companyRepository.Verify();
    }
    
    [Fact]
    public void CreateAnEmployee()
    {
        Mock<ICompanyRepository> companyRepository = new();
        Mock<IEmployeeRepository> employeeRepository = new();
        var companyService = new CompanyService(companyRepository.Object, employeeRepository.Object);
        var companyId = "59500657W";
        var employeeId = "95080440G";
        var company = new Company("59500657W");
        var expectedEmployee = new Employee(employeeId, company);
        
        companyRepository.Setup(_ => _.GetCompany("59500657W")).Returns(new Company("59500657W"));
        employeeRepository.Setup(m =>m.Add(It.Is<Employee>(employee =>
            employee.EmployeeId == expectedEmployee.EmployeeId && employee.Company.Id == expectedEmployee.Company.Id))).Verifiable();
        
        companyService.AddEmployee(companyId, employeeId);
        
        employeeRepository.Verify();
    }
}