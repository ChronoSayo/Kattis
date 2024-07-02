using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TheKingOfTheNorthTest
{
    [Fact]
    public void Test_ValidInput_Sample()
    {
        // Arrange
        var input = "7 8\n" +
            "42 42 0 0 0 0 0 16\n" +
            "42 11 14 42 42 42 10 16\n" +
            "42 0 42 42 42 42 0 16\n" +
            "42 0 42 42 42 42 0 42\n" +
            "42 0 42 42 42 42 0 42\n" +
            "42 11 42 42 42 5 5 42\n" +
            "42 42 0 0 0 42 42 42\n" +
            "3 4";
        var expectedOutput = "37";
        var stringWriter = new StringWriter();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);
            Console.SetOut(stringWriter);

            // Act
            TheKingOfTheNorth.Program.Main();

            // Assert
            var output = stringWriter.ToString().NormalizeOutput();
            var expectedNormalizedOutput = expectedOutput.NormalizeOutput();
            Assert.Equal(expectedNormalizedOutput, output);
        }
    }
}