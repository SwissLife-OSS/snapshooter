namespace Snapshooter.Core
{
    public interface ISnapshotSerializer
    {
        string Serialize(object objectToSnapshot);
    }
}