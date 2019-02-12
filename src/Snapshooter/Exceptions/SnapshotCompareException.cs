using System;

namespace Snapshooter.Exceptions
{
    /// <summary>
    /// Exception thrown if something went wrong during snapshot assertion.
    /// </summary>
    public class SnapshotCompareException : SnapshotTestException
    {
        /// <summary>
        /// Initializes the <see cref="SnapshotCompareException"/>
        /// </summary>
        public SnapshotCompareException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotCompareException"/>
        /// <param name="message">The exception message.</param>
        /// </summary>
        public SnapshotCompareException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotCompareException"/>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        /// </summary>
        public SnapshotCompareException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
