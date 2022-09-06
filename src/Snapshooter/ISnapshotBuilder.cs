using System;

namespace Snapshooter
{
    /// <summary>
    /// A builder to configure the snapshot
    /// </summary>
    public interface ISnapshotBuilder
    {
        /// <summary>
        /// Configures the Match options of this snapshot
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        ISnapshotBuilder ConfigureOptions(Func<MatchOptions, MatchOptions> configure);

        /// <summary>
        /// Configures the name of this snapshot
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        ISnapshotBuilder Name(string extensions);

        /// <summary>
        /// Configures the name extension
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        ISnapshotBuilder NameExtension(SnapshotNameExtension extensions);

        /// <summary>
        /// Takes a snapshot of the Target and validates it
        /// </summary>
        void Match();
    }
}
