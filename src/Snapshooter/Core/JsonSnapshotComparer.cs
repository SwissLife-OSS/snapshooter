using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.Exceptions;

namespace Snapshooter.Core
{
    public class JsonSnapshotComparer : ISnapshotComparer
    {
        private readonly IAssert _snapshotAssert;

        public JsonSnapshotComparer(IAssert snapshotAssert)
        {
            _snapshotAssert = snapshotAssert;
        }

        /// <summary>
        /// Compares the current snapshot with the expected snapshot and applies 
        /// the compare rules of the compare actions.
        /// </summary>
        /// <param name="matchOptions">The compare actions, which will be used for special comparion.</param>
        /// <param name="originalActualSnapshot">The original snapshot of the current result.</param>
        /// <param name="actualSnapshot">The actual (modifiable) snapshot of the current result.</param>
        /// <param name="expectedSnapshot">The expected (modifiable) snaphshot.</param>
        public void CompareSnapshots(
            string expectedSnapshot,
            string actualSnapshot,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            JToken originalActualSnapshotToken = ParseSnapshot(actualSnapshot);
            JToken actualSnapshotToken = ParseSnapshot(actualSnapshot);
            JToken expectedSnapshotToken = ParseSnapshot(expectedSnapshot);

            if (matchOptions != null)
            {
                ExecuteFieldMatchActions(originalActualSnapshotToken,
                    actualSnapshotToken, expectedSnapshotToken, matchOptions);
            }

            string actualSnapshotToCompare = actualSnapshotToken.ToString(Formatting.Indented);
            string expectedSnapshotToCompare = expectedSnapshotToken.ToString(Formatting.Indented);

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
                MatchOptions configuredMatchOptions = matchOptions(new MatchOptions());

                foreach (FieldMatchOperator matchOperator in configuredMatchOptions.MatchOperators)
                {
                    FieldOption fieldOption = matchOperator.ExecuteMatch(originalActualSnapshot);

                    RemoveFieldFromSnapshot(fieldOption.FieldPath, actualSnapshot);
                    RemoveFieldFromSnapshot(fieldOption.FieldPath, expectedSnapshot);
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
        /// <param name="fieldPath">The field path of the field to remove.</param>
        /// <param name="snapshot">The snapshot from which the field shall be removed.</param>
        private static void RemoveFieldFromSnapshot(string fieldPath, JToken snapshot)
        {
            IEnumerable<JToken> actualTokens = snapshot.SelectTokens(fieldPath, false);

            if (actualTokens != null)
            {
                foreach (JToken actual in actualTokens.ToList())
                {
                    if (actual.Parent is JArray)
                    {
                        ((JArray)actual.Parent).Remove(actual);
                    }
                    else
                    {
                        actual.Parent.Remove();
                    }
                }
            }
        }

        private static JToken ParseSnapshot(string snapshotJson)
        {
            var jsonLoadSettings = new JsonLoadSettings
            {
                CommentHandling = CommentHandling.Ignore,
                LineInfoHandling = LineInfoHandling.Ignore
            };

            return JToken.Parse(snapshotJson, jsonLoadSettings);
        }
    }
}
