namespace Test.Integration.Shared
{
    internal static class SharedUtils
    {
        public static string GetProjectDirectoryFullPath()
        {
            var currentDir = System.IO.Directory.GetCurrentDirectory();
            var projectDir = currentDir;
            var di = System.IO.Directory.GetParent(currentDir);
            while (di != null && di.Name != "generator")
            {
                projectDir = di.FullName;
                di = di.Parent;
                if (di == null)
                {
                    throw new System.InvalidOperationException($"Cannot get 'generator' ancestor directory for '{currentDir}' directory");
                }
            }
            return projectDir;
        }
    }
}
