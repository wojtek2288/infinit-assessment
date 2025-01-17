using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Infinit.Assessment.Github.Responses;
using Infinit.Assessment.Helpers;

namespace Infinit.Assessment.Github;

public partial class GithubClient
{
    private const string GithubApiBaseUrl = "https://api.github.com";
    private const string BlobFileType = "blob";
    private const int BatchSize = 100;

    [GeneratedRegex(@"\.tsx?$|\.jsx?$", RegexOptions.IgnoreCase)]
    private static partial Regex JsTsRegex();
    private readonly HttpClient httpClient;

    public GithubClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Dictionary<char, long>> FetchLetterFrequencyFromRepositoryAsync(
        string repositoryOwner,
        string repositoryName,
        string branchName)
    {
        var files = await GetRepositoryFilesRecursivelyAsync(repositoryOwner, repositoryName, branchName);

        var letterCounts = new Dictionary<char, long>();

        foreach (var batch in files.Chunk(BatchSize))
        {
            var contents = await FetchFileContentsAsync(repositoryOwner, repositoryName, batch);

            foreach (var content in contents)
            {
                LetterCounter.AddCounts(content, letterCounts);
            }
        }

        return letterCounts
            .OrderByDescending(kvp => kvp.Value)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private async Task<List<GithubTreeNode>> GetRepositoryFilesRecursivelyAsync(
        string repositoryOwner,
        string repositoryName,
        string branchName)
    {
        var url = $"{GithubApiBaseUrl}/repos/{repositoryOwner}/{repositoryName}/git/trees/{branchName}?recursive=1";

        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var treeResponse = JsonSerializer.Deserialize<GithubTreeResponse>(content)
            ?? throw new InvalidOperationException("Failed to deserialize the `GithubTreeResponse` response from the GitHub API.");

        return treeResponse.Tree
            .Where(node => node.Type == BlobFileType && JsTsRegex().IsMatch(node.Path))
            .ToList();
    }

    private async Task<List<string>> FetchFileContentsAsync(
        string repositoryOwner,
        string repositoryName,
        IEnumerable<GithubTreeNode> files)
    {
        var tasks = files.Select(async file =>
        {
            var url = $"{GithubApiBaseUrl}/repos/{repositoryOwner}/{repositoryName}/git/blobs/{file.Sha}";

            var response = await httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var blob = JsonSerializer.Deserialize<GithubBlobResponse>(content)
                ?? throw new InvalidOperationException("Failed to deserialize the `GithubBlobResponse` response from the GitHub API.");

            return blob.Content;
        });

        var base64Contents = await Task.WhenAll(tasks);

        return base64Contents
            .Select(content => Encoding.UTF8.GetString(Convert.FromBase64String(content)))
            .ToList();
    }
}
