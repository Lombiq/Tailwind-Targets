using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lombiq.Tailwind.Targets.Tests;

internal static class TailwindTargetsSample
{
    public static string GeneratedCssPath =>
        Path.Join("wwwroot", "css", "site.css");

    public static bool GeneratedCssExists() =>
        File.Exists(GeneratedCssPath);

    public static Task<string> ReadGeneratedCssAsync(CancellationToken cancellationToken) =>
        File.ReadAllTextAsync(GeneratedCssPath, cancellationToken);
}
