using Snapshooter.Exceptions;
namespace Snapshooter.Core
{
    public class SnapshotFullNameBuilder : ISnapshotFullNameBuilder
    {        
        public string BuildSnapshotFullName(string snapshotName)
        {
            return BuildSnapshotFullName(snapshotName, null);
        }

        public string BuildSnapshotFullName(string snapshotName, string snapshotNameExtension)
        {
            if (string.IsNullOrWhiteSpace(snapshotName))
            {
                throw new SnapshotTestException("No snapshot test name could be found.");
            }

            snapshotName = AddFileNameExtension(snapshotName, snapshotNameExtension);

            snapshotName = EnsureSnapshotFileExtension(snapshotName);            

            return snapshotName;
        }

        private static string EnsureSnapshotFileExtension(string snapshotName)
        {
            if (!snapshotName.EndsWith(FileNames.SnapshotFileExtension))
            {
                snapshotName = string.Concat(
                    snapshotName, FileNames.SnapshotFileExtension);
            }

            return snapshotName;
        }

        private static string AddFileNameExtension(
            string snapshotName, string snapshotNameExtension)
        {
            if (snapshotNameExtension != null)
            {
                snapshotName = string.Concat(snapshotName, snapshotNameExtension);
            }

            return snapshotName;
        }

    }
}
