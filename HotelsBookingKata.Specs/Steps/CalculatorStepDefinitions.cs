using TechTalk.SpecFlow.UnitTestProvider;

namespace HotelsBookingKata.Hotels.Domain.Specs.Steps;

[Binding]
public sealed class CalculatorStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
    private readonly IUnitTestRuntimeProvider _unitTestRuntimeProvider;
    private readonly ScenarioContext _scenarioContext;

    public CalculatorStepDefinitions(IUnitTestRuntimeProvider unitTestRuntimeProvider, ScenarioContext scenarioContext)
    {
        _unitTestRuntimeProvider = unitTestRuntimeProvider;
        _scenarioContext = scenarioContext;
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
        _unitTestRuntimeProvider.TestIgnore("This scenario is always skipped");
        var class1 = Class1.Hallo;
    }

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        //TODO: implement arrange (precondition) logic
        // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
        // To use the multiline text or the table argument of the scenario,
        // additional string/Table parameters can be defined on the step definition
        // method. 
        _unitTestRuntimeProvider.TestIgnore("This scenario is always skipped");
        _scenarioContext.Pending();
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        //TODO: implement act (action) logic
        _unitTestRuntimeProvider.TestIgnore("This scenario is always skipped");
        _scenarioContext.Pending();
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        //TODO: implement assert (verification) logic
        _unitTestRuntimeProvider.TestIgnore("This scenario is always skipped");
        _scenarioContext.Pending();
    }
}