namespace Snapshooter.Xunit
{
    internal class XUnitSnapshotBuilder : SnapshotBuilder
    {
        public XUnitSnapshotBuilder(object target)
            : base(target.RemoveUnwantedWrappers())
        {
        }

        public override void Match()
        {
            SnapshotFullName snapshotName =
                (_snapshotName: SnapshotName, snapshotNameExtension: SnapshotNameExtension) switch
                {
                    ({ }, null) => Snapshot.FullName(SnapshotName),
                    (null, { }) => Snapshot.FullName(SnapshotNameExtension),
                    ({ }, { }) => Snapshot.FullName(SnapshotName, SnapshotNameExtension),
                    _ => Snapshot.FullName(),
                };

            Snapshot.Match(Target, snapshotName, ConfigureOptions);
        }
    }
}
