using System;
using FluentAssertions;
using NUnit.Framework;
using Snapshooter.Tests.Data;

namespace Snapshooter.NUnit.Tests
{
    public class SnapshotExtensionTests
    {
        [Test]
        public void MatchSnapshot_ShouldFluentAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot();
        }

        [Test]
        public void MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot(nameof(MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject));
        }

        [Test]
        public void MatchSnapshot_PlainExtension_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.MatchSnapshot();
        }

        [Test]
        public void MatchSnapshot_PlainExtensionAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.MatchSnapshot();
        }

        [Test]
        public void MatchSnapshot_ShouldFluentAssertionsAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.Should().MatchSnapshot();
        }

        [Test]
        public void MatchSnapshot_Null_Throws()
        {
            // arrange
            TestPerson? testPerson = null;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => testPerson.MatchSnapshot());
        }
    }
}
