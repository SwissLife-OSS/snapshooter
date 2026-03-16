using static Snapshooter.NUnit.Snapshot;

namespace Snapshooter.NUnit
{
    internal class NUnitSnapshotBuilder : SnapshotBuilder
    {
        public NUnitSnapshotBuilder(object target)
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
