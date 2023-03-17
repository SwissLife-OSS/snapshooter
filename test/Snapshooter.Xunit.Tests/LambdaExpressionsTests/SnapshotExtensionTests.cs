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
            TestPerson testPerson = CreateTestPersonWithRandomData();

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
            TestPerson testPerson = CreateTestPersonWithRandomData();

            // act & assert
            Snapshot.Match(testPerson, options => options
                    .AcceptField(m => m.Firstname)
                    .AcceptField(m => m.DateOfBirth)
                    .IgnoreFields(m => m.Children['*'].Name)
                );
        }

        private static TestPerson CreateTestPersonWithRandomData()
        {
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithFirstname(new Random().Next().ToString())
                .WithDateOfBirth(DateTime.Now)
                .Build();

            foreach (var testPersonChild in testPerson.Children)
            {
                testPersonChild.Name = new Random().Next().ToString();
            }

            return testPerson;
        }

        [Fact]
        public void MatchSnapshot_FieldWithRandomInput_IgnoreField()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton()
                .WithAge(new Random().Next(0, 100)).Build();

            // act & assert
            Snapshot.Match(testPerson, options => options
                .IgnoreField(m => m.Age));
        }
    }
}
