using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using Xunit;

public class PostcardCollectionTests
{
    [Fact]
    public void Test_ValidInput_YearsAndCities()
    {
        // Arrange
        var input = "2\n3\nstockholm\nberlin\nparis\n1\nlondon\n";
        var expectedOutput = "3 1";
        var stringWriter = new StringWriter();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);
            Console.SetOut(stringWriter);

            // Act
            Program.Main();

            // Assert
            var output = stringWriter.ToString().NormalizeOutput();
            var expectedNormalizedOutput = expectedOutput.NormalizeOutput();
            Assert.Equal(expectedNormalizedOutput, output);
        }
    }

    [Fact]
    public void Test_InvalidInput_Years()
    {
        // Arrange
        var input = $"invalid\n1\n1\nstockholm";
        var expectedOutput = "Invalid number of years. " +
            $"Please enter a positive integer less than or equal to 50. 1";
        var stringWriter = new StringWriter();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);
            Console.SetOut(stringWriter);

            // Act
            Program.Main();

            // Assert
            var output = stringWriter.ToString().NormalizeOutput();
            var expectedNormalizedOutput = expectedOutput.NormalizeOutput();
            Assert.Contains(expectedNormalizedOutput, output);
        }
    }

    [Fact]
    public void Test_InvalidInput_Cities()
    {
        // Arrange
        var input = $"1\ninvalid\n1\nstockholm";
        var expectedOutput = $"Invalid number of cities. " +
            $"Please enter a positive integer less than or equal to 100. 1";
        var stringWriter = new StringWriter();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);
            Console.SetOut(stringWriter);

            // Act
            Program.Main();

            // Assert
            var output = stringWriter.ToString().NormalizeOutput();
            var expectedNormalizedOutput = expectedOutput.NormalizeOutput();
            Assert.Contains(expectedNormalizedOutput, output);
        }
    }
}