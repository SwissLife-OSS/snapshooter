using System.Collections.Generic;

namespace Snapshooter
{
	/// <summary>
    /// A snapshot name extension will be transformed to a string, which will be 
    /// added to the end of the snapshot name.
    /// </summary>
    public class SnapshotNameExtension
    {
        private readonly List<object> _snapshotNameExtensions = new List<object>();

		/// <summary>
        /// Creates a new snapshot name extension with a given text.
        /// </summary>
        /// <param name="snapshotNameExtension">The snapshot name extension text.</param>
        public SnapshotNameExtension(string snapshotNameExtension)
        {
            _snapshotNameExtensions.Add(snapshotNameExtension);
        }

        /// <summary>
        /// Creates a new snapshot name extension with several given strings.
        /// </summary>
        /// <param name="snapshotNameExtensions">The snapshot name extension texts.</param>
        public SnapshotNameExtension(params string[] snapshotNameExtensions)
        {
            _snapshotNameExtensions.AddRange(snapshotNameExtensions);
        }

        /// <summary>
        /// Creates a new snapshot name extension from the given objects.
        /// </summary>
        /// <param name="snapshotNameExtensions">The snapshot name extension objects.</param>
        public SnapshotNameExtension(params object[] snapshotNameExtensions)
        {
            _snapshotNameExtensions.AddRange(snapshotNameExtensions);
        }

        /// <summary>
        /// Creates a new snapshot name extension with a given text.
        /// </summary>
        /// <param name="snapshotNameExtension">The snapshot name extension text.</param>
        public static SnapshotNameExtension Create(string snapshotNameExtension)
        {
            return new SnapshotNameExtension(snapshotNameExtension);
        }

        /// <summary>
        /// Creates a new snapshot name extension with several given strings.
        /// </summary>
        /// <param name="snapshotNameExtensions">The snapshot name extension texts.</param>
        public static SnapshotNameExtension Create(params string[] snapshotNameExtensions)
        {
            return new SnapshotNameExtension(snapshotNameExtensions);
        }

        /// <summary>
        /// Creates a new snapshot name extension from the given objects.
        /// </summary>
        /// <param name="snapshotNameExtensions">The snapshot name extension objects.</param>
        public static SnapshotNameExtension Create(params object[] snapshotNameExtensions)
        {
            return new SnapshotNameExtension(snapshotNameExtensions);
        }

		/// <summary>
        /// Creates the snapshot name extension string from the given parameters.
        /// </summary>
        /// <returns>The snapshot name extension text.</returns>
        public string ToParamsString()
        {            
            string extensionName = string.Join("_", _snapshotNameExtensions);
            if (!string.IsNullOrWhiteSpace(extensionName))
            {
                extensionName = string.Concat("_", extensionName);
            }
            return extensionName;            
        }
    }
}
