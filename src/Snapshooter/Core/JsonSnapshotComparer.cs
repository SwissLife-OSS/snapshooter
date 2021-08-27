using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;

namespace Snapshooter.Core
{
    /// <summary>
    /// The snapshot comparer is responsible to compare the actual snapshot with the
    /// existing one and also include the field match options checks.
    /// </summary>
    public class JsonSnapshotComparer : ISnapshotComparer
    {
        private readonly IAssert _snapshotAssert;
        private readonly ISnapshotSerializer _snapshotSerializer;

        /// <summary>
        /// Creates a new instance of the <see cref="JsonSnapshotComparer"/>
        /// </summary>
        /// <param name="snapshotAssert">The snapshot assert.</param>
        /// <param name="snapshotSerializer">The snapshot serializer.</param>
        public JsonSnapshotComparer(
            IAssert snapshotAssert, ISnapshotSerializer snapshotSerializer)
        {
            _snapshotAssert = snapshotAssert;
            _snapshotSerializer = snapshotSerializer;
        }

        /// <summary>
        /// Compares the current snapshot with the expected snapshot and applies
        /// the compare rules of the compare actions.
        /// </summary>
        /// <param name="matchOptions">The compare actions, which will be used for special comparion.</param>
        /// <param name="expectedSnapshot">The original snapshot of the current result.</param>
        /// <param name="actualSnapshot">The actual (modifiable) snapshot of the current result.</param>
        public void CompareSnapshots(
            string expectedSnapshot,
            string actualSnapshot,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            JToken originalActualSnapshotToken = _snapshotSerializer.Deserialize(actualSnapshot);
            JToken actualSnapshotToken = _snapshotSerializer.Deserialize(actualSnapshot);
            JToken expectedSnapshotToken = _snapshotSerializer.Deserialize(expectedSnapshot);

            if (matchOptions != null)
            {
                ExecuteFieldMatchActions(
                    originalActualSnapshotToken,
                    actualSnapshotToken,
                    expectedSnapshotToken,
                    matchOptions);
            }

            string actualSnapshotToCompare = _snapshotSerializer
                .SerializeJsonToken(actualSnapshotToken);
            string expectedSnapshotToCompare = _snapshotSerializer
                .SerializeJsonToken(expectedSnapshotToken);

            _snapshotAssert.Assert(expectedSnapshotToCompare, actualSnapshotToCompare);
        }
        
        private void ExecuteFieldMatchActions(
            JToken originalActualSnapshot,
            JToken actualSnapshot,
            JToken expectedSnapshot,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            try
            {
                MatchOptions configMatchOptions = matchOptions(new MatchOptions());

                foreach (FieldMatchOperator matchOperator in configMatchOptions.MatchOperators)
                {
                    FieldOption fieldOption = matchOperator.ExecuteMatch(originalActualSnapshot);

                    RemoveFieldFromSnapshot(fieldOption, actualSnapshot);
                    RemoveFieldFromSnapshot(fieldOption, expectedSnapshot);
                }
            }
            catch (SnapshotFieldException)
            {
                throw;
            }
            catch (Exception err)
            {
                throw new SnapshotCompareException($"The compare action " +
                    $"has been failed. Error: {err.Message}");
            }
        }

        /// <summary>
        ///  Removes a field from the snapshot.
        /// </summary>
        /// <param name="fieldOption">The field option of the field to remove.</param>
        /// <param name="snapshot">The snapshot from which the field shall be removed.</param>
        private static void RemoveFieldFromSnapshot(FieldOption fieldOption, JToken snapshot)
        {            
            if (snapshot is JValue)
            {                
                throw new NotSupportedException($"No snapshot match options are " +
                    $"supported for snapshots with scalar values. Therefore the " +
                    $"match options are not allowed.");
            }

            foreach (var fieldPath in fieldOption.FieldPaths)
            {
                IEnumerable<JToken> actualTokens = snapshot.SelectTokens(fieldPath, false);
                if (actualTokens != null)
                {
                    foreach (JToken actual in actualTokens.ToList())
                    {
                        if (actual.Parent is JArray array)
                        {
                            array.Remove(actual);
                        }
                        else
                        {
                            actual.Parent.Remove();
                        }
                    }
                }
            }
        }
    }
}
