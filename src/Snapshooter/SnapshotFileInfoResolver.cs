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

        public ISnapshotFileInfo ResolveSnapshotFileInfo()
        {
            return ResolveSnapshotFileInfo(null);
        }

        public ISnapshotFileInfo ResolveSnapshotFileInfo(
			string snapshotName)
        {
            return ResolveSnapshotFileInfo(snapshotName, null);
        }

        public ISnapshotFileInfo ResolveSnapshotFileInfo(
			string snapshotName, string snapshotNameExtension)
        {
            SnapshotFileInfo snapshotFileInfo = _snapshotFileInfoReader.ReadSnapshotFileInfo();

            if (snapshotFileInfo == null)
            {
                throw new SnapshotTestException("The snapshot file infos could not be read.");
            }

            if(string.IsNullOrWhiteSpace(snapshotFileInfo.Filename) && 
			   string.IsNullOrWhiteSpace(snapshotName))
            {
                throw new SnapshotTestException("No snapshot name could be resolved.");
            }

            if (string.IsNullOrWhiteSpace(snapshotName))
            {
                snapshotName = snapshotFileInfo.Filename;				
            }

            snapshotFileInfo.Filename = _snapshotFileNameBuilder
				.BuildSnapshotFileName(snapshotName, snapshotNameExtension);

            return snapshotFileInfo;
        }
    }
}