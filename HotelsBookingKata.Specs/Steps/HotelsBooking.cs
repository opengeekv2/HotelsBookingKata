using HotelsBookingKata.Hotels.Domain.Sepcs.Fakes;

namespace HotelsBookingKata.Hotels.Domain.Specs.Steps;

[Binding]
public sealed class HotelsBooking
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
    private readonly HttpClient client;

    private readonly ScenarioContext _scenarioContext;

    private readonly IHotelRepository _hotelRepository;

    public HotelsBooking(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _hotelRepository = new HotelRepository();
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
    public void GivenAnEmployeeOfCompanyAndEmployeeId(string p0, string p1)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"the employee ""(.*)"" books the room type ""(.*)"" on hotel ""(.*)"" from ""(.*)"" to ""(.*)""")]
    public void WhenTheEmployeeBooksTheRoomTypeOnHotelFromTo(string p0, string @double, string p2, string p3, string p4)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the result should complete a booking and return confirmation for the employee ""(.*)"" books the room type ""(.*)"" on hotel ""(.*)"" from ""(.*)"" to ""(.*)""")]
    public void ThenTheResultShouldCompleteABookingAndReturnConfirmationForTheEmployeeBooksTheRoomTypeOnHotelFromTo(string p0, string @double, string p2, string p3, string p4)
    {
        ScenarioContext.StepIsPending();
    }
}