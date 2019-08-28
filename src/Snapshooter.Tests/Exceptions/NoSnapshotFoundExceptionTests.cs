using System;
using Xunit;

namespace Snapshooter.Exceptions
{
    public class SnapshotNotFoundExceptionTests
    {
        [Fact]
        public void Create_Instance()
        {
            // act
            var exception = new SnapshotNotFoundException();

            // assert
            Assert.Equal(
                "Exception of type 'Snapshooter.Exceptions.SnapshotNotFoundException' was thrown.",
                exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Create_Instance_With_Message()
        {
            // act
            var exception = new SnapshotNotFoundException("abc");

            // assert
            Assert.Equal("abc", exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Create_Instance_With_Message_And_Inner_Exception()
        {
            // arrange
            var innerException = new Exception();

            // act
            var exception = new SnapshotNotFoundException("abc", innerException);

            // assert
            Assert.Equal("abc", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
