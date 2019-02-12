using System;

namespace Snapshooter.Exceptions
{
    /// <summary>
    /// Exception thrown if something went wrong during snapshot assertion.
    /// </summary>
    public class SnapshotTestException : Exception
    {
        /// <summary>
        /// Initializes the <see cref="SnapshotTestException"/>
        /// </summary>
        public SnapshotTestException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotTestException"/>
        /// <param name="message">The exception message.</param>
        /// </summary>
        public SnapshotTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotTestException"/>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        /// </summary>
        public SnapshotTestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
