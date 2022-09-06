using static Snapshooter.Xunit.Snapshot;

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
            SnapshotFullName snapshotName = (SnapshotName, SnapshotNameExtension) switch
            {
                ({ }, null) => FullName(SnapshotName),
                (null, { }) => FullName(SnapshotNameExtension),
                ({ }, { }) => FullName(SnapshotName, SnapshotNameExtension),
                _ => FullName()
            };

            Snapshot.Match(Target, snapshotName, ConfigureOptions);
        }
    }
}
