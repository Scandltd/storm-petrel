namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Snapshot options.
    /// </summary>
    public sealed class SnapshotOptions
    {
        public ISnapshotInfoProvider SnapshotInfoProvider { get; set; } = new SnapshotInfoProvider();
        public static SnapshotOptions Current { get; set; } = new SnapshotOptions();
    }
}
