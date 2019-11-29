using Snapshooter.Tests.Data;
using Snapshooter.Xunit;
using System;
using Xunit;

namespace Snapshooter.Examples.Xunit
{
    public class SubfolderTests
    {
        /// <summary>
        /// Test example of saving the snapshot in a test subfolder.
        /// </summary>
        [Fact]
        public void CreatePerson_SnapshotSavedInSubfolder_SuccessfullySaved()
        {
            // arrange
            var serviceClient = new ServiceClient();

            // act
            TestPerson person = serviceClient.CreatePerson(
                Guid.Parse("4492F21C-8501-4771-A070-C79C7C7EF444"), "Sub", "Folder");

            // assert
            Snapshot.Match(person);
        }
    }
}
