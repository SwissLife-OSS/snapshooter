using System;
using System.Collections.Generic;
using System.Linq;

namespace Snapshooter
{
    /// <summary>
    /// A builder to configure the snapshot
    /// </summary>
    public abstract class SnapshotBuilder
        : ISnapshotBuilder
    {
        private readonly List<Func<MatchOptions, MatchOptions>> _optionsConfigurations =
            new List<Func<MatchOptions, MatchOptions>>();

        public SnapshotBuilder(object target)
        {
            Target = target;
        }

        /// <summary>
        /// The target that should be snapshoted
        /// </summary>
        protected object Target { get; }

        /// <summary>
        /// The name of the snapshot file
        /// </summary>
        protected string SnapshotName { get; private set; }

        /// <summary>
        /// The name extension of the snapshot
        /// </summary>
        protected SnapshotNameExtension SnapshotNameExtension { get; private set; }

        /// <summary>
        /// Configures the Match options of this snapshot
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public ISnapshotBuilder ConfigureOptions(Func<MatchOptions, MatchOptions> configure)
        {
            _optionsConfigurations.Add(configure);
            return this;
        }

        /// <summary>
        /// Configures the name of this snapshot
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public ISnapshotBuilder Name(string extensions)
        {
            SnapshotName = extensions;
            return this;
        }symotion-F2)

        /// <summary>
        /// Configures the name extension
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public ISnapshotBuilder NameExtension(SnapshotNameExtension extensions)
        {
            SnapshotNameExtension = extensions;
            return this;
        }

        /// <summary>
        /// Takes a snapshot of the Target and validates it
        /// </summary>
        public abstract void Match();

        /// <summary>
        /// Builds the MatchOptions based on the configuration
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected MatchOptions ConfigureOptions(MatchOptions options) =>
            _optionsConfigurations.Aggregate(options, (current, configure) => configure(current));
    }
}
