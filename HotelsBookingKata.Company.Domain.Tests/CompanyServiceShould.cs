using Moq;

namespace HotelsBookingKata.Company.Domain.Tests;

public class CompanyServiceShould
{
    [Fact]
    public void CreateACompany()
    {
        Mock<ICompanyRepository> companyRepository = new();
        var companyService = new CompanyService(companyRepository.Object);
        var companyId = "59500657W";
        var expectedCompany = new Company(companyId);
        companyRepository.Setup(m =>m.AddCompany(It.Is<Company>(company =>
            company.CompanyId == expectedCompany.CompanyId))).Verifiable();
        
        companyService.AddCompany(companyId);
        
        companyRepository.Verify();
    }
}