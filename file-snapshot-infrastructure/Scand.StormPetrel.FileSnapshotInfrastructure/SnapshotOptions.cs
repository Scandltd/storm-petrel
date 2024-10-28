namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Snapshot options.
    /// </summary>
    public sealed class SnapshotOptions
    {
        public ISnapshotInfoProvider SnapshotInfoProvider { get; set; }
        public static SnapshotOptions Current { get; set; } = new SnapshotOptions
        {
            SnapshotInfoProvider = new SnapshotInfoProvider(),
        };
    }
}
