namespace Snapshooter.Core
{
    public interface ISnapshotFileHandler
    {
        string LoadSnapshot(
            ISnapshotFileInfo snapshotFileInfo);
        void DeleteEmptySnapshotSubfolder(
            ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder);
        void DeleteSnapshotSubfolderFile(
            ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder);
        string SaveSnapshot(
            ISnapshotFileInfo snapshotFileInfo, SnapshotSubfolder subfolder, string snapshotData);        
    }
}