using System;
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
        public void MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot(nameof(MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject));
        }

        [Fact]
        public void MatchSnapshot_PlainExtension_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.MatchSnapshot();
        }

        [Fact]
        public void MatchSnapshot_PlainExtensionAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.MatchSnapshot();
        }

        [Fact]
        public void MatchSnapshot_ShouldFluentAssertionsAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.Should().MatchSnapshot();
        }

        [Fact]
        public void MatchSnapshot_Null_Throws()
        {
            // arrange
            TestPerson testPerson = null;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => testPerson.MatchSnapshot());
        }
    }
}
