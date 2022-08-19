using System;
using FluentAssertions;
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

        [Fact]
        public void MatchSnapshot_SnapshotMatchAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            Snapshot.Match(testPerson, options => options
                    .AcceptField(m => m.Firstname)
                    .AcceptField(m => m.DateOfBirth)
                    .IgnoreFields(m => m.Children['*'].Name)
                );
        }

        [Fact]
        public void MatchSnapshot_FieldWithRandomInput_IgnoreField()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();
            testPerson.Age = new Random().Next(0, 100);

            // act & assert
            Snapshot.Match(testPerson, options => options
                .IgnoreField(m => m.Age));
        }
    }
}
