using Nuke.Common.CI.AppVeyor;

public static class AppVeyorExtensions
{
    public static bool BranchIsMaster(this AppVeyor appVeyor)
    {
        return appVeyor.RepositoryBranch == "master";
    }
}
