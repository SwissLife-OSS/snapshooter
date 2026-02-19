using System;
using System.Collections.Generic;
using FluentAssertions;
using Snapshooter.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Snapshooter.MSTest.Tests
{
    [TestClass]
    public class SnapshotExtensionTests
    {
        [TestMethod]
        public void MatchSnapshot_ShouldFluentAssertions_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot();
        }

        [TestMethod]
        public void MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.Should().MatchSnapshot(nameof(MatchSnapshot_ShouldFluentAssertionsNameOf_RemovesSubject));
        }

        [TestMethod(DisplayName = "Test for issue #118")]
        public void MatchSnapshot_FluentAssertions_StringValue_ShouldRemovesSubject()
        {
            // arrange
            string testValue = "Some text string";

            // act & assert
            testValue.Should().MatchSnapshot();
        }

        [TestMethod(DisplayName = "Test for issue #118")]
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

        [TestMethod]
        public void MatchSnapshot_PlainExtension_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            testPerson.MatchSnapshot();
        }

        [TestMethod]
        public void MatchSnapshot_PlainExtensionAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.MatchSnapshot();
        }

        [TestMethod]
        public void MatchSnapshot_ShouldFluentAssertionsAnonymousType_CorrectSnapshot()
        {
            // arrange
            TestPerson testPerson = TestDataBuilder.TestPersonMarkWalton().Build();

            // act & assert
            new { foo = testPerson }.Should().MatchSnapshot();
        }

        [TestMethod]
        public void MatchSnapshot_Null_Throws()
        {
            // arrange
            TestPerson testPerson = null;

            // act & assert
            Assert.ThrowsExactly<ArgumentNullException>(() => testPerson.MatchSnapshot());
        }
    }
}
