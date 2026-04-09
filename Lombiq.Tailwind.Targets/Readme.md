# Lombiq Tailwind MSBuild Targets

[![Lombiq.Tailwind.Targets NuGet](https://img.shields.io/nuget/v/Lombiq.Tailwind.Targets?label=Lombiq.Tailwind.Targets)](https://www.nuget.org/packages/Lombiq.Tailwind.Targets/)

## About

Provides automatic Tailwind CSS compilation before building a .NET project. This allows you to keep your Tailwind input
CSS in source control while generating the final output into _wwwroot_ on build. These operations are optimized by
running only if the corresponding inputs have changed.

If you need an Orchard Core example, check out the `Lombiq.Tailwind.Targets.Samples` theme in this repository.

Do you want to quickly try out this project and see it in action? Check it out in our
[Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core
solution and also see our other useful Orchard Core-related open-source projects!

## How to use

Install the [NuGet package](https://www.nuget.org/packages/Lombiq.Tailwind.Targets/). If you use the project from a
submodule, add the following lines to the csproj file where the Tailwind input CSS file lives. Make sure that the paths
are pointing to the _Lombiq.Tailwind.Targets.props_ and _Lombiq.Tailwind.Targets.targets_ files of this project.

```xml
<Import Project="path\to\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets.props" />
<Import Project="path\to\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets.targets" />
```

Create a Tailwind input CSS file (for Tailwind v4 this typically contains `@import "tailwindcss";` and `@source`
directives). By default the CLI is downloaded from GitHub releases. On Windows, the targets use PowerShell, on
Linux/macOS they use `curl`. You can override the download command with `TailwindDownloadCommand` or disable it and point
`TailwindCliPath` to a pre-downloaded binary.

## Useful properties

For most projects, the defaults should be sufficient. In particular, if your Tailwind input CSS lives at
_Assets/Styles/site.css_, the generated output should go to _wwwroot/css/site.css_, and the relevant templates/scripts
are in the same project, then you likely don't need to set any of the properties below.

The following properties are the ones most commonly customized:

- `TailwindInput` / `TailwindOutput` - input and output CSS paths (relative to project). Set these if you don't use the default _Assets/Styles/site.css_ to _wwwroot/css/site.css_ layout.
- `TailwindMinify` - set to `true` to add `--minify` (defaults to `false`).
- `TailwindContentGlobs` - overrides the default glob list used for incremental build detection. Set this if the relevant files are not covered by the defaults, or if you want full control over the inputs.
- `TailwindContentGlobsAdditional` - appends to the default glob list without replacing it. This is the safer option if you just need to include a few additional files or folders.

The following properties are for advanced or environment-specific scenarios:

- `TailwindRunOnBuild` (default: `true`) - disables the build target when set to `false`.
- `TailwindCliVersion` - pins the Tailwind CLI version (e.g. `v4.2.1`).
- `TailwindCliOs` / `TailwindCliArch` - override the detected OS and architecture for CLI download.
- `TailwindCliDownloadUrl` - override the download URL if you mirror the CLI.
- `TailwindCliCacheDirectory` - where the CLI is cached (default: `obj/tailwind`).
- `TailwindCliPath` - explicit CLI path (skip downloading when set alongside `TailwindCliDownload=false`).
- `TailwindCliDownload` - set to `false` to disable downloading (use a pre-downloaded CLI in that case).
- `TailwindDownloadCommand` - override the download command (useful for restricted environments).
- `TailwindWatchMode` - defaults to `always` so `dotnet build -t:TailwindWatch` keeps the Tailwind watcher running until you stop it with `Ctrl+C` or close the terminal window.
- `TailwindAdditionalArguments` - passed to the Tailwind CLI verbatim.
- `TailwindWorkingDirectory` - working directory for the Tailwind command.

## Watch mode

Run Tailwind in watch mode with:

```bash
dotnet build --target:TailwindWatch
```

By default the targets use `--watch=always`, so `dotnet build -t:TailwindWatch` does not exit immediately after the
initial build work is done. Instead, Tailwind keeps watching for changes until you stop the command with `Ctrl+C` or
close the terminal window. Set the `TailwindWatchPoll` property to `true` if your file system events are unreliable.
Set the `TailwindWatchMode` property to an empty string if you want plain `--watch` instead. For example:

```bash
dotnet build -t:TailwindWatch -p:TailwindWatchPoll=true -p:TailwindWatchMode=
```

## Notes on scan scope

Only include local sources in `TailwindContentGlobs` or `TailwindContentFiles`. These inputs are used by MSBuild only
for incremental build detection, that is, to decide when the Tailwind build should rerun. Tailwind's actual class
detection is driven by your `@source` directives or `tailwind.config.js`. By default, the targets watch Razor/Liquid/JS/TS
files in the project and also every CSS file under the Tailwind input CSS file's directory (for example, if
`Assets/Styles/site.css` imports `theme.css` or `components.css`, changing those files also triggers a rebuild). For
reusable modules or base themes that ship via NuGet, prefer shipping precompiled CSS instead of expecting consuming apps
to scan their templates.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send
them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines)
while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through
Lombiq.
