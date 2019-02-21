using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    public class SnapshotFullNameResolver : ISnapshotFullNameResolver
    {
        private readonly ISnapshotFullNameReader _snapshotFullNameReader;
        private readonly ISnapshotFullNameBuilder _snapshotFullNameBuilder;
        
        public SnapshotFullNameResolver(
            ISnapshotFullNameReader snapshotFullNameReader,
            ISnapshotFullNameBuilder snapshotFullNameBuilder)
        {
            _snapshotFullNameReader = snapshotFullNameReader;
            _snapshotFullNameBuilder = snapshotFullNameBuilder;
        }

        public SnapshotFullName ResolveSnapshotFullName()
        {
            return ResolveSnapshotFullName(null);
        }

        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName)
        {
            return ResolveSnapshotFullName(snapshotName, null);
        }

        public SnapshotFullName ResolveSnapshotFullName(
            string snapshotName, string snapshotNameExtension)
        {
            SnapshotFullName snapshotFullName = _snapshotFullNameReader.ReadSnapshotFullName();

            if (snapshotFullName == null)
            {
                throw new SnapshotTestException("The snapshot full names could not be read.");
            }

            if(string.IsNullOrWhiteSpace(snapshotFullName.Filename) && 
               string.IsNullOrWhiteSpace(snapshotName))
            {
                throw new SnapshotTestException("No snapshot name could be resolved.");
            }

            if (string.IsNullOrWhiteSpace(snapshotName))
            {
                snapshotName = snapshotFullName.Filename;
            }

            string filename = _snapshotFullNameBuilder
                .BuildSnapshotFullName(snapshotName, snapshotNameExtension);
                       
            return new SnapshotFullName(filename, snapshotFullName.FolderPath);
        }
    }
}