using System;

namespace Snapshooter.Exceptions
{
    /// <summary>
    /// Exception thrown if something went wrong during snapshot assertion.
    /// </summary>
    public class SnapshotNotFoundException : SnapshotTestException
    {
        /// <summary>
        /// Initializes the <see cref="SnapshotNotFoundException"/>
        /// </summary>
        public SnapshotNotFoundException()
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotNotFoundException"/>
        /// <param name="message">The exception message.</param>
        /// </summary>
        public SnapshotNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes the <see cref="SnapshotNotFoundException"/>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        /// </summary>
        public SnapshotNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
