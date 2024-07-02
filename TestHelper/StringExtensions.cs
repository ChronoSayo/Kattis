public static class StringExtensions
{
    public static string NormalizeOutput(this string input)
    {
        return string.Join(" ", input.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
    }
}
