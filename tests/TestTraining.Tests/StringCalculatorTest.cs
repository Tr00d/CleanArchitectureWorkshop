using System;
using Xunit;

namespace TestTraining.Tests;

public class StringCalculatorTest
{
    private readonly StringCalculator calculator;

    public StringCalculatorTest()
    {
        this.calculator = new StringCalculator();
    }

    [Fact]
    public void Add_ShouldReturnZero_GivenNumbersIsEmptyString()
    {
        throw new NotImplementedException();
    }
}