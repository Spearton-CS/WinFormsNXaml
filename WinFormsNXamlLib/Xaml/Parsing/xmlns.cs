sharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using HtmlAgilityPack;

public class XmlnsResolver
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public XmlnsResolver(ILogger logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }


    public async Task<string> ResolveLibraryForXmlnsAsync(string xmlnsUri)
    {
        _logger.LogInformation($"Resolving library for xmlns: {xmlnsUri}");

        // 1. Анализ URI
        var uriParts = xmlnsUri.Split('/').ToList();
        var host = uriParts.Count > 2 ? uriParts[2] : "";
        var path = uriParts.Count > 3 ? string.Join("/", uriParts.Skip(3)) : "";

        // 2. Поиск в интернете (используем HTML Agility Pack для парсинга результатов)
        var searchQueries = new List<string> { xmlnsUri, $"{host} {path} xaml", $"{path} xaml library" };
        var webResults = await SearchInternetAsync(searchQueries);
        if(webResults.Count > 0)
            _logger.LogInformation($"Web search found: {string.Join(", ", webResults.Take(3))}");


        // 3. Поиск в документации (не реализовано, требует знания источников документации)

        // 4. Поиск на NuGet
        var nugetResults = await SearchNuGetAsync($"{host} {path}".Replace(".", " "), searchQueries);
       if (nugetResults.Count > 0)
          _logger.LogInformation($"Nuget search found: {string.Join(", ", nugetResults.Take(3))}");

        // 5. Проверка исходного кода (псевдокод - GitHub API или парсинг) - упрощено
       var githubUrl = webResults.FirstOrDefault(r => r.Contains("github.com", StringComparison.OrdinalIgnoreCase));
        string githubResults = null;
        if (githubUrl != null)
        {
             githubResults = await AnalyzeGithubRepoAsync(githubUrl, xmlnsUri);
              if (!string.IsNullOrEmpty(githubResults))
               _logger.LogInformation($"Github analysis found: {githubResults}");
        }

        // 6. Сбор результатов и дедукция
        var allResults = webResults.Concat(nugetResults).ToList();
        if (!string.IsNullOrEmpty(githubResults))
          allResults.Add(githubResults);

        // Логика выбора наиболее подходящей библиотеки
        string libraryName = DetermineLibrary(xmlnsUri, allResults);
         _logger.LogInformation($"Resolved library for xmlns '{xmlnsUri}': {libraryName}");

        return libraryName;
    }

    private string DetermineLibrary(string xmlnsUri, List<string> allResults)
    {
         if(xmlnsUri.Contains("sharpvectors", StringComparison.OrdinalIgnoreCase))
           return "SharpVectors";
       if (xmlnsUri.Contains("schemas.microsoft.com/winfx/2006/xaml/presentation", StringComparison.OrdinalIgnoreCase))
          return "WPF/UWP/Xamarin.Forms";
        
        // Проверка наличия имени библиотеки в результатах
        foreach (var result in allResults)
        {
            var match = Regex.Match(result, @"(?i)(sharpvectors|avalonia|oxyplot|livecharts)", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Value;
        }

        return "unknown";
    }

    private async Task<List<string>> SearchInternetAsync(List<string> queries)
    {
        var results = new List<string>();
        foreach (var query in queries)
        {
            try
            {
               var encodedQuery = System.Net.WebUtility.UrlEncode(query);
               var searchUrl = $"https://www.google.com/search?q={encodedQuery}";
                var response = await _httpClient.GetAsync(searchUrl);
                response.EnsureSuccessStatusCode();
                var htmlContent = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                var links = doc.DocumentNode.SelectNodes("//a[@href]")
                   ?.Select(node => node.GetAttributeValue("href", ""))
                  .Where(href => !string.IsNullOrWhiteSpace(href) && href.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                   .Distinct()
                    .ToList() ?? new List<string>();
                results.AddRange(links);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during web search for '{query}': {ex.Message}");
            }
        }
        return results;
    }


    private async Task<List<string>> SearchNuGetAsync(string keyword, List<string> queries)
    {
        var results = new List<string>();
        try
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3()); // Включаем v3

            var source = new PackageSource("https://api.nuget.org/v3/index.json"); // Обязательно v3
            var sourceProvider = new SourceRepositoryProvider(new PackageSourceProvider(Settings.LoadDefaultSettings(null)), providers);

            var repo = sourceProvider.CreateRepository(source);

             var findResource = await repo.GetResourceAsync<FindPackageByIdResource>();
             var metadataResource = await repo.GetResourceAsync<MetadataResource>(); // Добавили получения метадаты

             var searchMetadata = await metadataResource.GetMetadataAsync(
                 keyword,
                   includePrerelease: false,
                    includeUnlisted:false,
                   new SourceCacheContext(),
                   _logger,
                   CancellationToken.None);
             if(searchMetadata.Any())
             {
                 results.AddRange(searchMetadata.Select(m => $"{m.Identity.Id} - {m.Description}").ToList());
             }
             else {
                foreach (var query in queries)
                {
                     searchMetadata = await metadataResource.GetMetadataAsync(
                        query,
                       includePrerelease: false,
                       includeUnlisted: false,
                       new SourceCacheContext(),
                       _logger,
                       CancellationToken.None);

                      if (searchMetadata.Any())
                         results.AddRange(searchMetadata.Select(m => $"{m.Identity.Id} - {m.Description}").ToList());
                  }
            }


            return results;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during NuGet search: {0}", ex.Message);
            return results;
        }
    }

    private async Task<string> AnalyzeGithubRepoAsync(string githubUrl, string xmlnsUri)
    {
        try
        {
           
            var githubUrlParts = githubUrl.Split('/');
            var repoName = githubUrlParts[^1]; // Gets the last part of url, assuming it is the repo

            _logger.LogDebug($"Analyzing Github repo: {repoName}");
            // Для упрощения предполагаем, что имя репозитория достаточно для идентификации
            return repoName;


             // Дополнительно, можно использовать GitHub API для анализа
             //  (только как пример)

           /*  var repoOwner = githubUrlParts[^2];

            var githubApiUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/contents";

             _logger.LogDebug($"Github API URL: {githubApiUrl}");

            var response = await _httpClient.GetAsync(githubApiUrl);
            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();

            // Анализ JSON, поиск файлов, связанных с XAML и т.д.
            // ...
            */
           // return "Github search is not implemented";


        }
        catch (Exception ex)
        {
           _logger.LogError(ex, $"Error analyzing GitHub repo: {ex.Message}");
           return null;

        }
    }
}

// Пример использования
public class Program
{
    public static async Task Main(string[] args)
    {
         var logger = new Logger(); // Простой логгер
        var resolver = new XmlnsResolver(logger);

       var xmlnsList = new List<string> {
          "http://sharpvectors.codeplex.com/svgc/",
           "http://schemas.microsoft.com/winfx/2006/xaml/presentation",
          "http://schemas.avaloniaui.net/xaml",
          "http://oxyplot.org/xaml",
          "http://livecharts.dev/xaml"
       };

      foreach (var xmlns in xmlnsList) {
          var library = await resolver.ResolveLibraryForXmlnsAsync(xmlns);
          Console.WriteLine($"XMLNS: {xmlns}, Library: {library}");
      }


        Console.ReadKey();
    }
}

// Простой логгер для вывода
public class Logger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine($"{DateTime.Now} - {logLevel}: {formatter(state, exception)}");
       if(exception != null) {
        Console.WriteLine($"{exception.Message}");
       }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
         return true;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
       return new Scope();
    }

    public class Scope : IDisposable {
     public void Dispose() {}
    }
}
