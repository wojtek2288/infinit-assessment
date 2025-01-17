namespace Infinit.Assessment.Extensions;

public static class DictionaryExtensions
{
    /// <summary>
    /// Adds the frequency of each letter in the specified content to the dictionary.
    /// Converts all characters in the content to lowercase and only processes alphabetic characters.
    /// If a letter already exists in the dictionary, its count is incremented; otherwise, it is added with an initial count of 1.
    /// </summary>
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
