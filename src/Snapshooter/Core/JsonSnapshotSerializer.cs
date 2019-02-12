using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Snapshooter.Core
{
    /// <summary>
    /// The class <see cref="JsonSnapshotSerializer"/> is responsible to 
    /// serialize an object into a snapshot.
    /// </summary>
    public class JsonSnapshotSerializer : ISnapshotSerializer
    {
        /// <summary>
        /// Serializes an object to a snapshot string.
        /// </summary>
        /// <param name="objectToSnapshot">The object to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        public string Serialize(object objectToSnapshot)
        {
            string snapshotData = JsonConvert.SerializeObject(objectToSnapshot, _settings);

            return snapshotData;
        }
		
        /// <summary>
        /// Snapshot serialization settings
        /// </summary>
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = new[]
            {
                new StringEnumConverter()
            }
        };
    }
}
