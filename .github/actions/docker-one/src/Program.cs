using System.Text;
using CommandLine;
using DotNet.GitHubAction;
using DotNet.GitHubAction.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static CommandLine.Parser;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services.AddGitHubActionServices())
    .Build();

static TService Get<TService>(IHost host)
    where TService : notnull =>
    host.Services.GetRequiredService<TService>();

var parser = Default.ParseArguments<ActionInputs>(() => new(), args);
parser.WithNotParsed(
    errors =>
    {
        Get<ILoggerFactory>(host)
            .CreateLogger("DotNet.GitHubAction.Program")
            .LogError(
                string.Join(
                    Environment.NewLine, errors.Select(error => error.ToString())));

        Environment.Exit(2);
    });

await parser.WithParsedAsync(
    async options => await StartAnalysisAsync(options, host));
await host.RunAsync();

static async ValueTask StartAnalysisAsync(ActionInputs inputs, IHost host)
{
    Console.WriteLine("Starting analysis...");
    Console.WriteLine($"Congrats on running a dotnet app")
    Console.WriteLine("Analysis Complete...");


    var updatedMetrics = true;
    var title = "Updated 3 folders";
    var summary = "Calculated code metrics on three folders.";

    // Do the work here...

    // Write GitHub Action workflow outputs.
    var gitHubOutputFile = Environment.GetEnvironmentVariable("GITHUB_OUTPUT");
    if (!string.IsNullOrWhiteSpace(gitHubOutputFile))
    {
        using StreamWriter textWriter = new(gitHubOutputFile, true, Encoding.UTF8);
        textWriter.WriteLine($"updated-metrics={updatedMetrics}");
        textWriter.WriteLine($"summary-title={title}");
        textWriter.WriteLine($"summary-details={summary}");
    }

    await ValueTask.CompletedTask;

    Environment.Exit(0);
}