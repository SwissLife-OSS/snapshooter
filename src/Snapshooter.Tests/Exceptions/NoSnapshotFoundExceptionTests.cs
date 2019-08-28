using System;
using Snapshooter.Exceptions;
using Xunit;

namespace Snapshooter.Exceptions
{
    public class NoSnapshotFoundExceptionTests
    {
        [Fact]
        public void Create_Instance()
        {
            // act
            var exception = new NoSnapshotFoundException();

            // assert
            Assert.Equal(
                "Exception of type 'Snapshooter.Exceptions.NoSnapshotFoundException' was thrown.",
                exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void Create_Instance_With_Message()
        {
            // act
            var exception = new NoSnapshotFoundException("abc");

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
            var exception = new NoSnapshotFoundException("abc", innerException);

            // assert
            Assert.Equal("abc", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
