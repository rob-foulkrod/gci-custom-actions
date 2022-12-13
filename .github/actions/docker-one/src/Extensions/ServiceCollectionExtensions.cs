namespace DotNet.GitHubAction.Extensions;

static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGitHubActionServices(
        this IServiceCollection services) =>
        services;
    //Remove actual Analysis
    // services.AddSingleton<ProjectMetricDataAnalyzer>()
    //         .AddDotNetCodeAnalysisServices();
}
