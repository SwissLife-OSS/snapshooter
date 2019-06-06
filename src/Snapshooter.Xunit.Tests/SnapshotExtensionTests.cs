using Xunit;
using FluentAssertions;
using Snapshooter.Tests.Data;


namespace Snapshooter.Xunit.Tests
{
    public class SnapshotExtensionTests
    {
        [Fact]
        public void MatchSnapshot_ShouldFluentAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot();
        }

        [Fact]
        public void MatchSnapshot_PlainExtension_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot();
        }
    }
}
