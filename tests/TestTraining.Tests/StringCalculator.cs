using System;

namespace TestTraining.Tests;

public class StringCalculator
{
    public int Add(string numbers)
    {
        var result = 0;
        if (string.Equals(numbers, string.Empty))
            return result;
        foreach (var theNumber in numbers.Split(','))
        {
            result += int.Parse(theNumber);
        }
        return result;
    }
}