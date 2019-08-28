using System;

namespace Snapshooter.Exceptions
{
    /// <summary>
    /// Exception thrown if something went wrong during snapshot assertion.
    /// </summary>
    public class NoSnapshotFoundException : SnapshotTestException
    {
        /// <summary>
        /// Initializes the <see cref="NoSnapshotFoundException"/>
        /// </summary>
        public NoSnapshotFoundException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="NoSnapshotFoundException"/>
        /// <param name="message">The exception message.</param>
        /// </summary>
        public NoSnapshotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="NoSnapshotFoundException"/>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        /// </summary>
        public NoSnapshotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
