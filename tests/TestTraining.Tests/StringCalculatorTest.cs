using System;
using FluentAssertions;
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
    public void Add_ShouldReturnZero_GivenNumbersIsEmptyString() => this.calculator.Add(string.Empty).Should().Be(0);

    [Fact]
    public void Add_ShouldReturnOne_GivenNumbersIsOne() => this.calculator.Add("1").Should().Be(1);

    [Fact]
    public void Add_ShouldReturnTwo_GivenNumbersIsTwo() => this.calculator.Add("2").Should().Be(2);

    [Fact]
    public void Add_ShouldReturnThree_GivenNumbersIsOneAndTwo() => this.calculator.Add("1,2").Should().Be(3);

    [Fact]
    public void Add_ShouldReturnSix_GivenNumbersIsOneAndTwoAndThree() => this.calculator.Add("1,2,3").Should().Be(6);
    
    [Fact]
    public void Add_ShouldReturnSix_GivenNumbersIsOneAndTwoAndThreeWithBackslashSeparators() => this.calculator.Add("1\n2,3").Should().Be(6);
    
    [Theory]
    [InlineData("//;\n1;2", 3)]
    [InlineData("//-\n1-2", 3)]
    public void Add_ShouldReturnThree_GivenNumbersContainsCustomSeparators(string inNumbers, int inResult) => this.calculator.Add(inNumbers).Should().Be(inResult);
    
    [Fact]
    public void Add_ShouldThrowException_GivenNumbersContainsNegativeNumbers() => Assert.Throws<Exception>(() => calculator.Add("1,-1"));
}