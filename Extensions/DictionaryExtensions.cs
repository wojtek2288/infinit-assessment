namespace Infinit.Assessment.Extensions;

public static class DictionaryExtensions
{
    public static void AddLetterCounts(this Dictionary<char, long> letterCounts, string content)
    {
        foreach (var character in content.ToLower())
        {
            if (char.IsLetter(character))
            {
                if (!letterCounts.TryGetValue(character, out var value))
                {
                    value = 0;
                }

                letterCounts[character] = ++value;
            }
        }
    }
}
