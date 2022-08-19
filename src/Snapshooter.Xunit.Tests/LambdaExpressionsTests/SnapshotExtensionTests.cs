using FluentAssertions.Primitives;
using Snapshooter.Tests.Data;
using Xunit;

namespace Snapshooter.Xunit.Tests.LambdaExpressionsTests
{
    public class SnapshotExtensionTests
    {
        [Fact]
        public void MatchSnapshot_ShouldFluentAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot(o => o
                .AcceptField(m => m.Firstname)
                .AcceptField(m => m.DateOfBirth)
                .IgnoreFields(m => m.Children['*'].Name));
        }
    }
}
