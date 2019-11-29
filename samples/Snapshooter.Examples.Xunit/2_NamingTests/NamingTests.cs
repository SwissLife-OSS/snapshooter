using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using System;
using Xunit;

namespace Snapshooter.Examples.Xunit
{
    public class NamingTests
    {
        /// <summary>
        /// Test example of default snapshot name creation.
        /// </summary>
        [Fact]
        public void CreatePerson_DefaultSnapshotName_SnapshotWithDefaultName()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("1192F21C-8501-4771-A070-C79C7C7EF411"), "Albert", "Einstein");

            // assert

            // Snapshot name is NamingTests.CreatePerson_DefaultSnapshotName_SnapshotWithDefaultName.snap
            Snapshot.Match(person);
        }

        /// <summary>
        /// Test example of explicitly defined snapshot name creation.
        /// </summary>
        [Fact]
        public void CreatePerson_DefinedSnapshotName_SnapshotWithDefinedName()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("1192F21C-8501-4771-A070-C79C7C7EF411"), "Albert", "Einstein");

            // assert

            // Snapshot name is ExplicitlyDefinedSnapshotName.snap
            Snapshot.Match(person, "ExplicitlyDefinedSnapshotName");
        }

        /// <summary>
        /// Test example of explicitly defined snapshot name creation.
        /// </summary>
        [Fact]
        public void CreatePerson_SnapshotNameWithNameExtensions_SnapshotWithdDefaultNameExtensions()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("1192F21C-8501-4771-A070-C79C7C7EF411"), "Albert", "Einstein");

            // assert

            // Snapshot name is NamingTests.CreatePerson_SnapshotNameWithNameExtensions_SnapshotWithdDefaultNameExtensions_Age_88_Prof.snap
            Snapshot.Match(person, SnapshotNameExtension.Create("Age", 88, "Prof"));
        }
    }
}
