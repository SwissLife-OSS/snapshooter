using System;

namespace Snapshooter.Exceptions
{
    /// <summary>
    /// Exception thrown if something went wrong during snapshot assertion.
    /// </summary>
    public class SnapshotFieldException : SnapshotTestException
    {
        /// <summary>
        /// Initializes the <see cref="SnapshotFieldException"/>
        /// </summary>
        public SnapshotFieldException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotFieldException"/>
        /// <param name="message">The exception message.</param>
        /// </summary>
        public SnapshotFieldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotFieldException"/>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        /// </summary>
        public SnapshotFieldException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
