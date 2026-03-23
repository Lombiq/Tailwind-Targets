using System.IO;
using System.Linq;
using System.Reflection;

namespace Lombiq.Tailwind.Targets.Tests;

internal static class TailwindTargetsSample
{
    public static string GeneratedCssPath =>
        Path.Combine(GetSampleProjectDirectory(), "wwwroot", "css", "site.css");

    public static bool GeneratedCssExists() =>
        File.Exists(GeneratedCssPath);

    public static string ReadGeneratedCss() =>
        File.ReadAllText(GeneratedCssPath);

    private static string GetSampleProjectDirectory() =>
        Path.GetFullPath(Path.Combine(
            typeof(TailwindTargetsSample).Assembly
                .GetCustomAttributes<AssemblyMetadataAttribute>()
                .Single(attribute => attribute.Key == "MSBuildProjectDirectory")
                .Value!,
            "..",
            "Lombiq.Tailwind.Targets.Samples"));
}
