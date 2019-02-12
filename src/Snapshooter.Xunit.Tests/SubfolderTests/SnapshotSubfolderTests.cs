using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Xunit.Tests.Subfolder
{
    public class SnapshotSubfolderTests
    {
        [Fact]
        public void Match_SubfolderSnapshotGeneration_GoodCase()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match<TestPerson>(testPerson);
        }
    }
}
