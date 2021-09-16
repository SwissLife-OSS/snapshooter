using Xunit;

namespace Snapshooter.Xunit.Tests
{
    public class IterativeSnapshotTest
    {
        [Fact]
        public void Match_MultipleIterations_Successful()
        {
            const string nameBase = "TestCase";

            for (var i = 0; i < 2; i++)
            {
                var contents = $"this is iteration = {i}";

                var name = $"{nameBase}_{i}";

                Snapshot.Match(contents, Snapshot.FullName(SnapshotNameExtension.Create(name)));
            }
        }
    }
}
