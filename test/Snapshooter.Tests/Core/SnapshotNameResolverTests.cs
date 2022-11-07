//using Xunit;
//using Snapshooter.Core;
//using System;
//using Snapshooter.Exceptions;
//using Snapshooter.Model;

//namespace Snapshooter.Tests
//{
//    public class SnapshotNameResolverTests
//    {
//        [Fact]
//        public void ResolveSnapshotName_ResolveName_ReturnsSnapshotName()
//        {
//            // arrange
//            var nameResolver = new SnapshotFileNameBuilder();

//            // act
//            string snapshotName = nameResolver.ResolveSnapshotName(
//                nameof(ResolveSnapshotName_ResolveName_ReturnsSnapshotName));

//            // assert
//            Assert.Equal($"{nameof(ResolveSnapshotName_ResolveName_ReturnsSnapshotName)}" +
//                         $"{Wellknown.FileNames.SnapshotFileExtension}",
//                         snapshotName);
//        }

//        [Fact]
//        public void ResolveSnapshotName_ResolveNameWithNameExtensions_ReturnsSnapshotName()
//        {
//            // arrange
//            var nameExtensions = SnapshotNameExtension.Create("5", "6", "Result", "11");
//            var nameResolver = new SnapshotFileNameBuilder();

//            // act
//            string snapshotName = nameResolver.ResolveSnapshotName(nameExtensions,
//                nameof(ResolveSnapshotName_ResolveName_ReturnsSnapshotName));

//            // assert
//            Assert.Equal($"{nameof(ResolveSnapshotName_ResolveName_ReturnsSnapshotName)}" +
//                         $"_5_6_Result_11" +
//                         $"{Wellknown.FileNames.SnapshotFileExtension}",
//                         snapshotName);
//        }

//        [Theory]
//        [InlineData(null)]
//        [InlineData("")]
//        [InlineData(" ")]
//        public void ResolveSnapshotName_NameIsNullOrEmpty_ThrowsArgumentException(string name)
//        {
//            // arrange
//            var nameResolver = new SnapshotFileNameBuilder();

//            // act
//            Func<string> actionTask = () => nameResolver.ResolveSnapshotName(name);

//            // assert
//            Assert.Throws<SnapshotTestException>(actionTask);
//        }
//    }
//}
