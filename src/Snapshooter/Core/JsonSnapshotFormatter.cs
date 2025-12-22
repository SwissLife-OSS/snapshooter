using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

#nullable enable

namespace Snapshooter.Core
{
    /// <summary>
    /// Formats the snapshot output according the match options.
    /// Each match option has a specific format action (or no format action) and
    /// the snapshot fields will be formatted according to this format action of the
    /// match option.
    /// </summary>
    public class JsonSnapshotFormatter : ISnapshotFormatter
    {
        private readonly ISnapshotSerializer _snapshotSerializer;

        /// <summary>
        /// The constructor to create an instance of <see cref="JsonSnapshotFormatter"/>
        /// </summary>
        /// <param name="snapshotSerializer">The snapshot serializer.</param>
        public JsonSnapshotFormatter(ISnapshotSerializer snapshotSerializer)
        {
            _snapshotSerializer = snapshotSerializer;
        }

        /// <summary>
        /// Formats the snapshot output according the match options.
        /// Each match option has a specific format action (or no format action) and
        /// the snapshot fields will be formatted according to this format action of the
        /// match option.
        /// </summary>
        /// <param name="snapshot">
        /// The snapshot to format.
        /// </param>
        /// <param name="matchOptions">
        /// The match options, which contain the format actions.
        /// </param>
        /// <returns>The formatted snapshot.</returns>
        public string FormatSnapshot(string snapshot, MatchOptions matchOptions)
        {
            if (matchOptions.MatchOperators.Count == 0)
            {
                return snapshot;
            }

            if (matchOptions.MatchOperators
                .All(matchop => !matchop.HasFormatAction()))
            {
                return snapshot;
            }

            return FormatSnapshotFields(snapshot, matchOptions);
        }

        private string FormatSnapshotFields(
            string actualSnapshotSerialized,
            MatchOptions matchOptions)
        {
            JToken actualSnapshot = _snapshotSerializer.Deserialize(actualSnapshotSerialized);

            foreach (FieldMatchOperator matchOperator in matchOptions.MatchOperators)
            {
                if (matchOperator.HasFormatAction())
                {
                    matchOperator.FormatFields(actualSnapshot);
                }
            }

            return _snapshotSerializer.SerializeObject(actualSnapshot);
        }        
    }
}
