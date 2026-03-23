using Shouldly;
using Xunit;

namespace Lombiq.Tailwind.Targets.Tests.UnitTests;

public class TailwindTargetsTests
{
    [Fact]
    public void GeneratedCssShouldExist() =>
        TailwindTargetsSample.GeneratedCssExists().ShouldBeTrue();

    [Fact]
    public void GeneratedCssShouldContainExpectedUtilities()
    {
        var css = TailwindTargetsSample.ReadGeneratedCss();

        css.ShouldContain(".min-h-screen");
        css.ShouldContain(".bg-slate-950");
        css.ShouldContain(".tracking-\\[0\\.24em\\]");
    }
}
