using Scand.StormPetrel.Generator.Abstraction;
using System;
using System.IO;

namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Implements <see cref="IGeneratorDumper"/> to enable Storm Petrel rewrites against file snapshots.
    /// </summary>
    public sealed class SnapshotDumper : IGeneratorDumper
    {
        private readonly int _xorChecksumLength;
        public SnapshotDumper(int xorChecksumLength = 1024)
        {
            _xorChecksumLength = xorChecksumLength;
        }

        /// <summary>
        /// Dumps <see cref="GenerationDumpContext.Value"/> to a string.
        /// The supported types of <see cref="GenerationDumpContext.Value"/> are:
        /// - <see cref="string"/>, with no value conversion;
        /// - <see cref="byte[]"/>, converted to XorChecksum based on <see cref="SnapshotDumper"/> arguments;
        /// - <see cref="Stream"/>, converted to XorChecksum based on <see cref="SnapshotDumper"/> arguments.
        /// </summary>
        /// <param name="generationDumpContext"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if the type of <see cref="GenerationDumpContext.Value"/> is not supported.</exception>
        public string Dump(GenerationDumpContext generationDumpContext)
        {
            if (generationDumpContext.Value is string valueString)
            {
                // No need to dump twice
                return valueString;
            }
            else if (generationDumpContext.Value is byte[] bytes)
            {
                var checksum = XorChecksum(bytes);
                return BitConverterToString(checksum);
            }
            else if (generationDumpContext.Value is Stream stream)
            {
                var initialPosition = stream.Position;
                byte[] checksum = new byte[_xorChecksumLength];
                byte[] buffer = new byte[65536]; //some size from https://github.com/dotnet/runtime/discussions/74405
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    XorChecksum(checksum, buffer, bytesRead);
                }
                //Reset the position because user cannot explicitely control this method call flow
                stream.Position = initialPosition;
                return BitConverterToString(checksum);
            }
            var typeFullName = generationDumpContext.Value?.GetType().FullName;
            throw new InvalidOperationException($"Value type '{typeFullName}' is not supported. Use custom implementation of {typeof(IGeneratorDumper).FullName} to support the type");
        }

        private byte[] XorChecksum(byte[] data)
        {
            byte[] checksum = new byte[_xorChecksumLength];
            XorChecksum(checksum, data);
            return checksum;
        }

        private static void XorChecksum(byte[] checksum, byte[] data, int dataLength = -1)
        {
            if (dataLength <= 0)
            {
                dataLength = data.Length;
            }
            for (int i = 0; i < dataLength; i++)
            {
                checksum[i % checksum.Length] ^= data[i];
            }
        }

        private static string BitConverterToString(byte[] array) => BitConverter.ToString(array).Replace("-", "");
    }
}
