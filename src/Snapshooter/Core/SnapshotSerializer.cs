using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Snapshooter.Extensions;

namespace Snapshooter.Core
{
    /// <summary>
    /// The class <see cref="SnapshotSerializer"/> is responsible to 
    /// serialize an object into a snapshot.
    /// </summary>
    public class SnapshotSerializer : ISnapshotSerializer
    {
        /// <summary>
        /// Serializes an object to a snapshot string.
        /// </summary>
        /// <param name="objectToSnapshot">The object to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        public string SerializeObject(object objectToSnapshot)
        {
            string snapshotData = null;

            if (objectToSnapshot is string snapshotScalarString)
            {
                // handle strings separately
                snapshotData = snapshotScalarString.NormalizeLineEndings();
            }
            else
            {
                // handle objects
                snapshotData = JsonConvert.SerializeObject(objectToSnapshot, _settings);
                snapshotData = snapshotData.NormalizeLineEndings();
            }            

            return snapshotData;
        }

        /// <summary>
        /// Serializes a json token to a snapshot string.
        /// </summary>
        /// <param name="jsonToken">The json token to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        public string SerializeJsonToken(JToken jsonToken)
        {
            return jsonToken.ToString(Formatting.Indented)
                .NormalizeLineEndings();
        }

        /// <summary>
        /// Deserializes a snapshot string to an token object
        /// </summary>
        /// <param name="snapshotJson"></param>
        /// <returns></returns>
        public JToken Deserialize(string snapshotJson)
        {
            bool isValidJson = snapshotJson.IsValidJsonFormat();

            if(!isValidJson)
            {
                snapshotJson = JsonConvert.ToString(snapshotJson);
            }

            var snapshotToken = JToken.Parse(snapshotJson, _jsonLoadSettings);

            return snapshotToken;
        }

        /// <summary>
        /// Snapshot serialization settings.
        /// </summary>
        private static readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include,
                Converters = new JsonConverter[]
                {
                    new StringEnumConverter()
                }
            };

        /// <summary>
        /// Snapshot json load settings.
        /// </summary>
        private static readonly JsonLoadSettings _jsonLoadSettings =
            new JsonLoadSettings
            {
                CommentHandling = CommentHandling.Ignore,
                LineInfoHandling = LineInfoHandling.Ignore,
                DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
            };
    }
}
