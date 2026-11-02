# LinqBuilder

.NET implementation of the specification pattern for LINQ queries on `IEnumerable` and `IQueryable`. Three NuGet packages, released together with the same version:

- `LinqBuilder` (netstandard2.0, net8.0, net9.0, net10.0): `ISpecification<TEntity>`, `IOrderSpecification<TEntity>`, `IQuerySpecification<TEntity>`, `IDynamicSpecification<TEntity>` and the LINQ extensions that take them.
- `LinqBuilder.EFCore` (net8.0, net9.0, net10.0): async Entity Framework Core extensions (`AnyAsync`, `CountAsync`, `FirstOrDefaultAsync`, ...) that take a specification.
- `LinqBuilder.EF6` (netstandard2.1): the same extensions for Entity Framework 6.

netstandard2.0 defaults to C# 7.3, so `Directory.Build.props` sets `LangVersion` 8 for it to support nullable reference types.

## Workflow

- Conventional commits (`feat:`, `fix:`, `chore(deps):`, `ci:`, `build:`, `docs:`), one commit per concern, directly on `main`.
- Only push, tag, publish or edit GitHub releases when asked.
- Commit messages must be accurate. If one turns out to be wrong before it is pushed, reword it (replay the commits and check that the tree is unchanged). Never rewrite pushed history.

## Releasing

- **NuGet packages:** push a lightweight tag `vX.Y.Z` on `main`. CI publishes to NuGet on tags. Every push to `main` also publishes a beta to MyGet.
- **Verifying:** NuGet serves a new package for download after about 3 minutes; if a restore doesn't find it yet, clear the cache with `dotnet nuget locals http-cache --clear` and retry. After publishing, install the package from the registry and build a project that uses it.
- **Versioning:** any change to a public interface (the specification interfaces and the public extension methods) is breaking and needs a major version.

### GitHub release notes

Use these sections, omitting empty ones, and end with a compare link:

```markdown
## Breaking changes

## Features

## Fixes

## Known issues

## Other

**Full Changelog**: https://github.com/Baune8D/LinqBuilder/compare/vA...vB
```

- Write for users of the package: name the exact APIs, settings and before/after values. Leave out internal changes (tests, CI, IDE files).
- When a release fixes a bug from an earlier release, add a "Known issues" entry to the earlier release saying "Fixed in vX.Y.Z".

## Dependencies

- Dependabot runs weekly on Monday with a 7-day cooldown. Minor/patch updates are grouped; security updates have their own group.
- Major versions are ignored by Dependabot and upgraded by hand: read the release notes, update, and build/test before committing. If a major breaks the build and fixing it is a design decision, keep the old major and explain why in the commit.
- New exceptions go in `.github/dependabot.yml` with a comment explaining why.
- Before holding a package back, find the root cause and check the package's issue tracker.
- `Directory.Packages.props` gives Entity Framework Core and Microsoft.Extensions.Caching.Memory a version range per target framework (`[8,9)` for net8.0 and so on). The lower bound is the lowest version the library supports, so Dependabot ignores them; raise it by hand when the library needs a newer version. NuGet restores the lowest version in a range, so the tests run against the lowest supported versions.
- `LinqBuilder.EFCore.Tests` references `SQLitePCLRaw.bundle_e_sqlite3` directly, because the lowest EF Core versions depend on a SQLite build with a known vulnerability (GHSA-2m69-gcr7-jv3q). Keep it while they depend on SQLitePCLRaw 2.1.11 or lower.
- Dependabot restores each target framework separately in projects with more than two, so the lock files in its NuGet PRs only keep the last one. `dependabot-lock-files.yml` regenerates them, pushes the fix with `[skip ci]` and starts the pipeline, which reports a `pipeline` commit status on the PR. Dependabot doesn't rebase a PR that has someone else's commit; comment `@dependabot recreate` to update one.
- `build/_build.csproj` pins `NuGet.Packaging` and `System.Security.Cryptography.Xml` to patch vulnerable dependencies of Nuke.Common (NuGet 7.9 also fixes a `NuGet.Frameworks` load error with SDK 10.0.4xx). Remove the pins once Nuke.Common ships fixed versions.

## Building and testing

- `./build.sh Test` builds and runs the tests for all target frameworks. This machine only has the .NET 10 runtime; use `DOTNET_ROLL_FORWARD=Major ./build.sh Test` to run the net8.0/net9.0 tests on it. CI has all three runtimes. The EF6 tests only target net10.0.
- `global.json` is required: DotNet.ReproducibleBuilds 2.x warns without it, and the build treats warnings as errors.
- `WarningsNotAsErrors` keeps NuGet's vulnerability warnings (NU1901 to NU1904) as warnings, so a newly published advisory doesn't fail the restore. Check the restore output for them and fix them like a Dependabot alert.
- Restores use the lock files (`packages.lock.json`) in locked mode. After changing a package, run `dotnet restore LinqBuilder.slnx --force-evaluate` and commit the lock files.

## README

The root `README.md` is shown on GitHub. Each package packs its own `src/**/README.md` into the NuGet package, so keep them in sync when the usage changes. Use absolute links and no raw HTML in them, since nuget.org doesn't render either.
