using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using System;
using Xunit;

namespace Snapshooter.Quickstarts.Xunit
{
    public class IgnoreTests
    {
        /// <summary>
        /// Tests if the new created person is valid. The id will always be newly created,
        /// therefore we have to ignore the id of the person during comparison.
        /// The path to the id is given by a json path.
        /// </summary>
        [Fact]
        public void CreatePerson_VerifyPersonByIgnoringId_SuccessfulVerified()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.NewGuid(), "David", "Mustermann");

            // assert
            Snapshot.Match(person, matchOptions => matchOptions.IgnoreField("Id"));
        }

        /// <summary>
        /// Tests if the new created person is valid. The id will always be newly created,
        /// therefore we have to ignore the id of the person during comparison.
        /// The path to the id is given by a json path.
        /// In Xunit, the Theory InlineData parameters cannot be read, therefore the parameters
        /// can be added to the snapshot name with the snapshot name extension functionality.
        /// </summary>
        [Theory]
        [InlineData("David", "Mustermann")]
        [InlineData("Claudia", "Musterfrau")]

        public void CreatePerson_VerifyPersonsByIgnoringId_SuccessfulVerified(
            string firstname, string lastname)
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.NewGuid(), firstname, lastname);

            // assert
            Snapshot.Match(person, 
                SnapshotNameExtension.Create(firstname, lastname), 
                matchOptions => matchOptions.IgnoreField("Id"));
        }

        /// <summary>
        /// Tests if the new created person is valid. If we want to ignore several
        /// fields of the result object, we can concat the ignore options as many
        /// times as we want.
        /// The path to the id is given by a json path.
        /// </summary>
        [Fact]
        public void CreatePerson_VerifyPersonByIgnoringSeveralFields_SuccessfulVerified()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.NewGuid(), "David", "Mustermann");

            // assert
            Snapshot.Match(person, 
                matchOptions => matchOptions
                    .IgnoreField("Id")
                    .IgnoreField("Address.Plz")
                    .IgnoreField("Relatives[0].Address.Country.Name"));
        }
    }
}
