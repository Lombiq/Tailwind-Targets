using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Lombiq.Tailwind.Targets.Tests.UnitTests;

public class TailwindTargetsTests
{
    [Fact]
    public void GeneratedCssShouldExist() =>
        TailwindTargetsSample.GeneratedCssExists().ShouldBeTrue();

    [Fact]
    public async Task GeneratedCssShouldContainExpectedUtilities()
    {
        var css = await TailwindTargetsSample.ReadGeneratedCssAsync(TestContext.Current.CancellationToken);

        css.ShouldContain(".min-h-screen");
        css.ShouldContain(".bg-slate-950");
        css.ShouldContain(".tracking-\\[0\\.24em\\]");
    }
}
