using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    public class SnapshotFileInfoResolver : ISnapshotFileInfoResolver
    {
        private readonly ISnapshotFileInfoReader _snapshotFileInfoReader;
        private readonly ISnapshotFileNameBuilder _snapshotFileNameBuilder;
        
        public SnapshotFileInfoResolver(
            ISnapshotFileInfoReader snapshotFileInfoReader,
            ISnapshotFileNameBuilder snapshotFileNameBuilder)
        {
            _snapshotFileInfoReader = snapshotFileInfoReader;
            _snapshotFileNameBuilder = snapshotFileNameBuilder;
        }

        public SnapshotFullName ResolveSnapshotFileInfo()
        {
            return ResolveSnapshotFileInfo(null);
        }

        public SnapshotFullName ResolveSnapshotFileInfo(
            string snapshotName)
        {
            return ResolveSnapshotFileInfo(snapshotName, null);
        }

        public SnapshotFullName ResolveSnapshotFileInfo(
            string snapshotName, string snapshotNameExtension)
        {
            SnapshotFullName snapshotFullName = _snapshotFileInfoReader.ReadSnapshotFileInfo();

            if (snapshotFullName == null)
            {
                throw new SnapshotTestException("The snapshot file infos could not be read.");
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

            string filename = _snapshotFileNameBuilder
                .BuildSnapshotFileName(snapshotName, snapshotNameExtension);
                       
            return new SnapshotFullName(filename, snapshotFullName.FolderPath);
        }
    }
}