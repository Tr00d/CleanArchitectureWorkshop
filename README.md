# CleanArchitectureWorkshop

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Tr00d_CleanArchitectureWorkshop&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Tr00d_CleanArchitectureWorkshop)

The project initially contains an endpoint to retrieve operations from the current account.

# Exercise

The goal is to make the following acceptance test pass:

```gherkindotnet
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
```

In order to do that, a few additional features are required:

- User can deposit into Account
- User can withdraw from Account

# Extra features

- A user cannot withdraw more than what's available on the account
- A user cannot withdraw more than 2500 on 24hrs
- Add validation for withdraw and deposit actions (ex: amounts must be positive and superior than zero)