namespace Snapshooter.Core
{
    public interface ISnapshotFileInfo
    {
        string Filename { get; }
        string FolderPath { get; }
    }
}