using HotelsBookingKata.Company.Domain;

namespace HotelsBookingKata.Hotels.Domain.Specs.Fakes;
public class CompanyRepository : ICompanyRepository
{
    private readonly List<Company.Domain.Company> _companies = new ();

    public void Add(Company.Domain.Company company)
    {
        _companies.Add(company);
    }

    public Company.Domain.Company? GetCompany(string id)
    {
        return _companies.Find(company => company.Id == id);
    }
}