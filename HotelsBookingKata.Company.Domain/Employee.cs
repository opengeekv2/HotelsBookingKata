namespace HotelsBookingKata.Company.Domain;

public class Employee
{
    public string Id { get; }
    public Company Company { get; }

    public Employee(string id, Company company)
    {
        Id = id;
        Company = company;
    }
}