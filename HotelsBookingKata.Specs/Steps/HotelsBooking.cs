using HotelsBookingKata.Book.Domain;
using HotelsBookingKata.Company.Domain;
using HotelsBookingKata.Hotels.Domain.Specs.Fakes;

namespace HotelsBookingKata.Hotels.Domain.Specs.Steps;

[Binding]
public sealed class HotelsBooking
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
    private readonly ScenarioContext _scenarioContext;

    private readonly IHotelRepository _hotelRepository;
    
    private readonly ICompanyRepository _companyRepository;
    
    private readonly IEmployeeRepository _employeeRepository;

    public HotelsBooking(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _hotelRepository = new HotelRepository();
        _companyRepository = new CompanyRepository();
        _employeeRepository = new EmployeeRepository();
    }


    [Given(@"an hotel with id ""(.*)"" and name ""(.*)""")]
    public void GivenAnHotelWithIdAndName(string hotelId, string hotelName)
    {
        var hotelService = new HotelService(_hotelRepository);
        hotelService.AddHotel(hotelId, hotelName);
    }

    [Given(@"a room for hotel with id ""(.*)"", number (.*) and room type ""(.*)""")]
    public void GivenARoomForHotelWithNumberAndRoomType(string hotelId, int roomNumber, string roomType)
    {
        var hotelService = new HotelService(_hotelRepository);
        hotelService.SetRoom(hotelId, roomNumber, roomType);
    }

    [Given(@"an employee of company ""(.*)"", and employee id ""(.*)""")]
    public void GivenAnEmployeeOfCompanyAndEmployeeId(string companyId, string employeeId)
    {
        var companyService = new CompanyService(_companyRepository, _employeeRepository);
        companyService.AddCompany(companyId);
        companyService.AddEmployee(companyId, employeeId);
    }

    [When(@"the employee ""(.*)"" books the room type ""(.*)"" on hotel ""(.*)"" from ""(.*)"" to ""(.*)""")]
    public void WhenTheEmployeeBooksTheRoomTypeOnHotelFromTo(string employeeId, string roomType, string hotelId, string checkIn, string checkOut)
    {
        var bookingService = new BookingService();
        bookingService.Book(employeeId, hotelId, roomType, checkIn, checkOut);
    }

    [Then(@"the result should complete a booking and return confirmation for the employee ""(.*)"" books the room type ""(.*)"" on hotel ""(.*)"" from ""(.*)"" to ""(.*)""")]
    public void ThenTheResultShouldCompleteABookingAndReturnConfirmationForTheEmployeeBooksTheRoomTypeOnHotelFromTo(string employeeId, string roomType, string hotelId, string startDate, string endDate)
    {
        ScenarioContext.StepIsPending();
    }
}