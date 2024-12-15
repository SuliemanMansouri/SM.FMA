namespace SM.FMA.Extensions
{
    public static class StringExtensions
    {
        public static string ToCommaSeparatedString(this List<string> coAuthors)
        {
            return string.Join(", ", coAuthors);
        }

        public static List<string> ToListFromCommaSeparatedString(this string coAuthors)
        {
            return coAuthors.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim())
                            .ToList();
        }
    }
}
