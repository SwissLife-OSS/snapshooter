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
        public void SnapshotPerson_IgnoreChangingId_SuccessfullyCompared()
        {
            // Arrange
            var changingId = Guid.NewGuid();

            // Act
            TestPerson person = TestPerson.New()
                .WithId(changingId)
                .WithFirstName("Foo")
                .WithLastName("Bar")
                .WithDateOfBirth(new DateTime(2000, 6, 25))
                .WithCreationDate(new DateTime(2018, 6, 6))
                .WithAge(30)
                .WithSize(182.5214m)
                .Build();

            // Assert
            Snapshot.Match(person, matchOptions => matchOptions.IgnoreField("Id"));
        }

        /// <summary>
        /// Tests if the new created person is valid. The id will always be newly created,
        /// therefore we have to ignore the id of the person during comparison.
        /// The path to the id is given by a json path.
        /// </summary>
        [Fact]
        public void SnapshotPerson_IgnoreChangingId_SuccessfullyCompared1()
        {
            // Arrange
            var changingId = Guid.NewGuid();

            // Act
            var person = new
            {
                Id = changingId,
                FirstName = "Foo",
                LastName = "Bar",
                DateOfBirth = new DateTime(2000, 6, 25),
                Age = 30,
                Size = 182.5214
            };

            // Assert
            Snapshot.Match(person, matchOptions => matchOptions.IgnoreField("Id"));
        }

        /// <summary>
        /// Tests if the new created person is valid. The ids of the relatives
        /// will always be newly created, therefore we have to ignore the ids 
        /// during the snapshot comparison. The path to the ids is given by a json path.
        /// </summary>
        [Fact]
        public void SnapshotPerson_IgnoreChangingIdsWithinAnArray_SuccessfullyCompared1()
        {
            // Arrange
            var fooMamId = Guid.NewGuid();
            var fooDadId = Guid.NewGuid();
            var fooSistaId = Guid.NewGuid();

            // Act
            var person = new
            {
                Id = Guid.Parse("C78C698F-9EE5-4B4B-9A0E-EF729B1F8EC8"),
                FirstName = "Foo",
                LastName = "Bar",
                DateOfBirth = new DateTime(2000, 6, 25),
                Age = 30,
                Size = 182.5214,
                Relatives = new[]
                {
                    new { Id = fooMamId, Name = "Daniela"},
                    new { Id = fooDadId, Name = "Mike"},
                    new { Id = fooSistaId, Name = "Sue"}
                }
            };
            
            // Assert
            Snapshot.Match(person, matchOptions => matchOptions.IgnoreFields("Id"));
        }


        /// <summary>
        /// Tests if the new created person is valid. The ids of the relatives
        /// will always be newly created, therefore we have to ignore the ids 
        /// during the snapshot comparison. The path to the ids is given by a json path.
        /// </summary>
        [Fact]
        public void SnapshotPerson_IgnoreChangingIdsWithinAnArray_SuccessfullyCompared()
        {
            // Arrange
            var fooMam = Guid.NewGuid();
            var fooDad = Guid.NewGuid();
            var fooSista = Guid.NewGuid();

            // Act
            TestPerson person = TestPerson.New()
                .WithId(Guid.Parse("C78C698F-9EE5-4B4B-9A0E-EF729B1F8EC8"))
                .WithFirstName("Foo")
                .WithLastName("Bar")
                .AddRelative(TestPerson.New().WithId(fooMam).WithFirstName("Daniela").Build())
                .AddRelative(TestPerson.New().WithId(fooDad).WithFirstName("Mike").Build())
                .AddRelative(TestPerson.New().WithId(fooSista).WithFirstName("Sue").Build())
                .Build();

            // Assert
            Snapshot.Match(person, matchOptions => matchOptions.IgnoreFields("Id"));
        }

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
