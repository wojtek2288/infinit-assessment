namespace Infinit.Assessment.Helpers;

public static class LetterCounter
{
    public static void AddCounts(string content, Dictionary<char, long> letterCounts)
    {
        foreach (var character in content.ToLower())
        {
            if (char.IsLetter(character))
            {
                if (!letterCounts.TryGetValue(character, out var value))
                {
                    value = 0;
                    letterCounts[character] = value;
                }

                letterCounts[character] = ++value;
            }
        }
    }
}
