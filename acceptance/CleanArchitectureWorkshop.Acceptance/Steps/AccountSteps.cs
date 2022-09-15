using CleanArchitectureWorkshop.Acceptance.Drivers;
using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;

namespace CleanArchitectureWorkshop.Acceptance.Steps;

[Binding]
public class AccountSteps
{
    private readonly AccountDriver driver;

    public AccountSteps(AccountDriver driver)
    {
        this.driver = driver;
    }

    [Given(@"I make a deposit of (.*) on '(.*)'")]
    public async Task GivenIMakeADepositOfOn(double amount, DateTime date) =>
        await this.driver.DepositAsync(amount, date);

    [Given(@"I make a withdrawal of (.*) on '(.*)'")]
    public async Task GivenIMakeAWithdrawalOfOn(double amount, DateTime date) =>
        await this.driver.WithdrawAsync(amount, date);

    [When(@"I retrieve the account statements")]
    public async Task WhenIRetrieveTheAccountStatements() => await this.driver.RetrieveStatementsAsync();

    [Then(@"I should see these statements:")]
    public async Task ThenIShouldSeeTheseStatements(Table table)
    {
        var response = await this.driver.GetRetrievedStatementsAsync();
        response.History.Should().BeEquivalentTo(table.CreateSet<StatementModel>());
    }

    [Then(@"I should see no statements")]
    public async Task ThenIShouldSeeNoStatements()
    {
        var response = await this.driver.GetRetrievedStatementsAsync();
        response.History.Should().BeEmpty();
    }

    [When(@"I see my account balance")]
    public async Task WhenISeeMyAccountBalance() => await this.driver.RetrieveAccountBalanceAsync();

    [Then(@"The balance should be (.*)")]
    public async Task ThenTheBalanceShouldBe(double inBalance)
    {
        var response = await this.driver.GetRetrievedAccountBalanceAsync();
        response.Should().Be(inBalance);
    }

    [Given(@"The feature '(.*)' is enabled")]
    public void GivenTheFeatureIsEnabled(string inFeatureName)
    {
        this.driver.EnableFeature(inFeatureName);
    }

    [Given(@"The feature '(.*)' is disabled")]
    public void GivenTheFeatureIsDisabled(string inFeatureName)
    {
        this.driver.DisableFeature(inFeatureName);
    }
}