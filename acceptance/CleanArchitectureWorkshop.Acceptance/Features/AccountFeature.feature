Feature: AccountFeature
User can deposit into Account
User can withdraw from an Account
User can retrieve a bank statement to the console

    @Acceptance
    Scenario: Prints all statements with balance from newest to oldest
        Given I make a deposit of 1000 on '10 January 2021'
        And I make a deposit of 2000 on '15 January 2021'
        And I make a withdrawal of 500 on '20 January 2021'
        When I retrieve the account statements
        Then I should see these statements:
          | Date            | Amount | Balance |
          | 20 January 2021 | -500   | 2500    |
          | 15 January 2021 | 2000   | 3000    |
          | 10 January 2021 | 1000   | 1000    |