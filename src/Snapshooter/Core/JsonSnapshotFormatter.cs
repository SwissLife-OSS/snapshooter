using System;
using System.Collections.Generic;
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
        public string FormatSnapshot(
            string snapshot,
            Func<MatchOptions, MatchOptions>? matchOptions = null)
        {
            if(matchOptions is { })
            {
                string transformedSnapshot = 
                    ExecuteFieldFormatActions(snapshot, matchOptions);

                return transformedSnapshot;
            }

            return snapshot;
        }

        private string ExecuteFieldFormatActions(
            string actualSnapshot,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            MatchOptions configMatchOptions = matchOptions(new MatchOptions());

            if (configMatchOptions.MatchOperators.Count == 0)
            {
                return actualSnapshot;
            }
                        
            // TODO first check if any format action is set, if not return actualSnapshot

            JToken actualSnapshotToken = _snapshotSerializer.Deserialize(actualSnapshot);

            foreach (FieldMatchOperator matchOperator in configMatchOptions.MatchOperators)
            {
                if (matchOperator.IsFormatActionSet())
                {
                    FieldOption fieldOption = matchOperator
                        .GetFieldOption(actualSnapshotToken);

                    TransformSnapshotField(
                        matchOperator, fieldOption, actualSnapshotToken);
                }
            }

            return _snapshotSerializer.SerializeObject(actualSnapshotToken);
        }

        private bool TransformSnapshotField(
            FieldMatchOperator matchOperator,
            FieldOption fieldOption,
            JToken actualSnapshotToken)
        {
            bool formatExecuted = false;

            if (fieldOption.FieldPaths == null)
            {
                return formatExecuted;
            }

            foreach (var fieldPath in fieldOption.FieldPaths)
            {
                IEnumerable<JToken> actualTokens = 
                    actualSnapshotToken.SelectTokens(fieldPath, false);

                if (actualTokens is { })
                {
                    foreach (JToken actual in actualTokens)
                    {
                        matchOperator.FormatField(actual);

                        formatExecuted = true;
                    }
                }
            }

            return formatExecuted;
        }
    }
}
