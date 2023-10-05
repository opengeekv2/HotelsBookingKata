namespace HotelsBookingKata.Company.Domain;

public class Employee
{
    public string EmployeeId { get; }
    public Company Company { get; }

    public Employee(string employeeId, Company company)
    {
        EmployeeId = employeeId;
        Company = company;
    }
}