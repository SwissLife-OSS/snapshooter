using System;
using System.Collections.Generic;
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

        [Test(Description = "Test for issue #118")]
        public void MatchSnapshot_FluentAssertions_StringValue_ShouldRemovesSubject()
        {
            // arrange
            string testValue = "Some text string";

            // act & assert
            testValue.Should().MatchSnapshot();
        }

        [Test(Description = "Test for issue #118")]
        public void MatchSnapshot_FluentAssertions_DictionaryValue_ShouldRemovesSubject()
        {
            // arrange
            var testCustomer = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 1 },
                    { "Name", "Bla "},
                    { "EmailAddress", "blabla@blabla.com" },
                },
            };
            var testDictionary = new Dictionary<string, List<Dictionary<string, object>>>();
            testDictionary.Add("Customer", testCustomer);

            // act & assert
            testDictionary.Should().MatchSnapshot();
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
