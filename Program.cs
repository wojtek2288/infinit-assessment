using System.Diagnostics;
using System.Net.Http.Headers;
using Infinit.Assessment.Github;

namespace Infinit.Assessment;

internal class Program
{
    private const string RepositoryOwner = "lodash";
    private const string RepositoryName = "lodash";

    private static async Task Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        using var httpClient = PrepareGithubHttpClient(args);

        var githubClient = new GithubClient(httpClient);

        Console.WriteLine($"Fetching statistics from the repository...");

        var letterFrequency = await githubClient.FetchLetterFrequencyFromRepositoryAsync(
            RepositoryOwner,
            RepositoryName);

        Console.WriteLine("Letter Frequency (Descending):");

        foreach (var (letter, frequency) in letterFrequency)
        {
            Console.WriteLine($"{letter}: {frequency}");
        }

        stopwatch.Stop();
        Console.WriteLine($"Execution Time: {stopwatch.Elapsed}");
    }

    private static HttpClient PrepareGithubHttpClient(string[] args)
    {
        var token = ReadGithubTokenFromArgs(args);

        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DotNetClient", "1.0"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);

        return httpClient;
    }

    private static string ReadGithubTokenFromArgs(string[] args)
    {
        if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
        {
            throw new ArgumentException("Usage: dotnet run <GitHubToken>");
        }

        return args[0];
    }
}
