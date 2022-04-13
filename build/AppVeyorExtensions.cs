using Nuke.Common.CI.AppVeyor;

public static class AppVeyorExtensions
{
    public static bool BranchIsMain(this AppVeyor appVeyor)
    {
        return appVeyor.RepositoryBranch == "main";
    }
}
