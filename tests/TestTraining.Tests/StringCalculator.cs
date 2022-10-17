using System;
using System.Linq;

namespace TestTraining.Tests;

public class StringCalculator
{
    private const char defaultSeparator = ',';
    private const string customSeparatorIndicator = "//";
    
    public int Add(string numbers)
    {
        var separator = defaultSeparator;
        if (ContainsCustomSeparator(numbers))
        {
            separator = GetCustomSeparator(numbers);
            numbers = numbers.Replace($"{customSeparatorIndicator}{separator}\n", string.Empty);
        }
        
        var result = default(int);

        if (IsEmpty(numbers))
            return result;

        return numbers
            .Replace('\n', separator)
            .Split(separator)
            .Sum(ConvertNumber);
    }

    private static bool ContainsCustomSeparator(string numbers)
    {
        return numbers.StartsWith(customSeparatorIndicator);
    }

    private static int ConvertNumber(string x)
    {
        var theNumber = int.Parse(x);
        if (theNumber < 0)
        {
            throw new Exception();
        }

        return theNumber;
    }

    private static char GetCustomSeparator(string numbers)
    {
        return numbers[2];
    }

    private static bool IsEmpty(string numbers)
    {
        return string.Equals(numbers, string.Empty);
    }
}