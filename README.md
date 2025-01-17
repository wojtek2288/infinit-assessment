# Infinit assessment

A .NET 8 console app that calculates the frequency of letters in javascript/typescript files in a GitHub repository's source code.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- GitHub Personal Access Token.

## Getting Started

1. Clone the repo:

   ```bash
   git clone https://github.com/wojtek2288/infinit-assessment.git
   cd infinit-assessment
   ```

2. Run the app:

   ```bash
   dotnet run <GitHubToken>
   ```

   Replace `<GitHubToken>` with your GitHub token.

## Example Output

```text
Fetching statistics from the repository...
Letter Frequency (Descending):
e: 262122
t: 211570
a: 165579
r: 163042
n: 140384
s: 139006
o: 126620
...
Execution Time: 00:00:01.7571887
```

## Customization

To analyze a different repository, modify the `RepositoryOwner` and `RepositoryName` constants in `Program.cs`.
