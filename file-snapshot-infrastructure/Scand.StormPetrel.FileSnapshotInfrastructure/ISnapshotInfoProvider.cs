namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Declares a contract describing information about a file snapshot.
    /// </summary>
    public interface ISnapshotInfoProvider
    {
        /// <summary>
        /// Gets the full file path of the snapshot without the file extension.
        /// </summary>
        /// <param name="useCaseId">Test use case ID. See <see cref="Attributes.UseCaseIdAttribute"/> for more details.</param>
        /// <param name="callerFilePath">Test file path or its Storm Petrel version.</param>
        /// <param name="callerMemberName">Test method name.</param>
        /// <returns></returns>
        string GetFilePathWithoutExtension(string useCaseId, string callerFilePath, string callerMemberName);

        /// <summary>
        /// Gets the snapshot file extension. It may be null if the snapshot file already exists in the file system before the execution of Storm Petrel test methods.
        /// </summary>
        /// <param name="useCaseId">Test use case ID. See <see cref="Attributes.UseCaseIdAttribute"/> for more details.</param>
        /// <param name="callerFilePath">Test file path or its Storm Petrel version.</param>
        /// <param name="callerMemberName">Test method name.</param>
        /// <returns></returns>
        string GetFileExtension(string useCaseId, string callerFilePath, string callerMemberName);
    }
}
