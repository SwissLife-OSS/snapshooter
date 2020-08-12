using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using System;
using Xunit;
using Xunit.Sdk;

namespace Snapshooter.Quickstarts.Xunit
{
    public class MismatchTests
    {
        /// <summary>
        /// Test example of a mismatching snapshot.
        /// </summary>
        [Fact]
        public void CreatePerson_MismatchingSnapshot_ThrowsException()
        {
            // arrange
            var serviceClient = new ServiceClient();

            string lastName = "Snapshot" + "Wrong";

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("5592F21C-8501-4771-A070-C79C7C7EF478"), "Mismatch", lastName);

            // assert
            EqualException exception = 
                Assert.Throws<EqualException>(() => Snapshot.Match(person));
            Assert.Contains("LastName", exception.Message);
        }
    }
}
