# Lombiq Tailwind MSBuild Targets

[![Lombiq.Tailwind.Targets NuGet](https://img.shields.io/nuget/v/Lombiq.Tailwind.Targets?label=Lombiq.Tailwind.Targets)](https://www.nuget.org/packages/Lombiq.Tailwind.Targets/)

## About

Provides automatic Tailwind CSS compilation before building a .NET project. This allows you to keep your Tailwind input
CSS in source control while generating the final output into _wwwroot_ on build. These operations are optimized by
running only if the corresponding inputs have changed.

Do you want to quickly try out this project and see it in action? Check it out in our
[Open-Source Orchard Core Extensions](https://github.com/Lombiq/Open-Source-Orchard-Core-Extensions) full Orchard Core
solution and also see our other useful Orchard Core-related open-source projects!

## How to use

Install the [NuGet package](https://www.nuget.org/packages/Lombiq.Tailwind.Targets/). If you use the project from a
submodule, add the following lines to the csproj file where the Tailwind input CSS file lives. Make sure that the paths
are pointing to the _Lombiq.Tailwind.Targets.props_ and _Lombiq.Tailwind.Targets.targets_ files of this project.

```xml
<Import Project="path\to\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets.props" />
<Import Project="path\to\Lombiq.Tailwind.Targets\Lombiq.Tailwind.Targets.targets" />
```

Create a Tailwind input CSS file (for Tailwind v4 this typically contains `@import "tailwindcss";` and `@source`
directives). By default the CLI is downloaded from GitHub releases. On Windows the targets use PowerShell, on
Linux/macOS they use `curl`. You can override the download command with `TailwindDownloadCommand` or disable it and point
`TailwindCliPath` at a pre-downloaded binary.

## Useful properties

- `TailwindRunOnBuild` (default: `true`) - disables the build target when set to `false`.
- `TailwindInput` / `TailwindOutput` - input and output CSS paths (relative to project).
- `TailwindMinify` - set to `true` to add `--minify` (defaults to `false`).
- `TailwindContentGlobs` - overrides the default glob list used for incremental build detection.
- `TailwindContentGlobsAdditional` - appends to the default glob list without replacing it.
- `TailwindCliVersion` - pins the Tailwind CLI version (e.g. `v4.1.18`).
- `TailwindCliOs` / `TailwindCliArch` - override the detected OS and architecture for CLI download.
- `TailwindCliDownloadUrl` - override the download URL if you mirror the CLI.
- `TailwindCliCacheDirectory` - where the CLI is cached (default: `obj/tailwind`).
- `TailwindCliPath` - explicit CLI path (skip downloading when set alongside `TailwindCliDownload=false`).
- `TailwindCliDownload` - set to `false` to disable downloading (use a pre-downloaded CLI in that case).
- `TailwindDownloadCommand` - override the download command (useful for restricted environments).
- `TailwindWatchMode` - defaults to `always` to keep watch alive when stdin is closed.
- `TailwindAdditionalArguments` - passed to the Tailwind CLI verbatim.
- `TailwindWorkingDirectory` - working directory for the Tailwind command.

## Watch mode

Run Tailwind in watch mode with:

```bash
dotnet build -t:TailwindWatch
```

By default the targets use `--watch=always` so the process keeps running even when stdin is closed. Add
`TailwindWatchPoll=true` if your file system events are unreliable. Set `TailwindWatchMode` to an empty string if you want
plain `--watch`.

## Notes on scan scope

Only include local sources in `TailwindContentGlobs` or `TailwindContentFiles`. These inputs are used for MSBuild
incremental build detection; Tailwind itself scans based on your `@source` directives or `tailwind.config.js`. For
reusable modules or base themes that ship via NuGet, precompile their CSS and include it directly instead of scanning
their templates at build time.

## Contributing and support

Bug reports, feature requests, comments, questions, code contributions and love letters are warmly welcome. You can send
them to us via GitHub issues and pull requests. Please adhere to our [open-source guidelines](https://lombiq.com/open-source-guidelines)
while doing so.

This project is developed by [Lombiq Technologies](https://lombiq.com/). Commercial-grade support is available through
Lombiq.
