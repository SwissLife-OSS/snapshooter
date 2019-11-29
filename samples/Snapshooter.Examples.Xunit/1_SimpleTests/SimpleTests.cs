using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using System;
using Xunit;

namespace Snapshooter.Quickstarts.Xunit
{
    public class SimpleTests
    {
        /// <summary>
        /// Tests if the new created person is valid.
        /// </summary>
        [Fact]
        public void CreatePerson_VerifyPersonBySnapshot_SuccessfulVerified()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("2292F21C-8501-4771-A070-C79C7C7EF451"), "David", "Mustermann");

            // assert
            Snapshot.Match(person);
        }

        /// <summary>
        /// Tests if the new created person is valid.
        /// In Xunit, the Theory InlineData parameters cannot be read, therefore the parameters
        /// can be added to the snapshot name with the snapshot name extension functionality.
        /// </summary>
        [Theory]
        [InlineData("2292F21C-8501-4771-A070-C79C7C7EF451", "David", "Mustermann")]
        [InlineData("1292F21C-8501-4771-A070-C79C7C7EF452", "Claudia", "Musterfrau")]

        public void CreatePerson_VerifyPersonsBySnapshot_SuccessfulVerified(
            string id, string firstname, string lastname)
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse(id), firstname, lastname);

            // assert
            Snapshot.Match(person, SnapshotNameExtension.Create(firstname, lastname));
        }
    }
}
