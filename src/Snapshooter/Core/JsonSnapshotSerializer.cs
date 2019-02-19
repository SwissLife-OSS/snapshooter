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

			snapshotData = NormalizeLineEndings(snapshotData);

            return snapshotData;
        }

		/// <summary>
		/// Snapshot serialization settings
		/// </summary>
		private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = new JsonConverter[]
            {
                new StringEnumConverter()
			}
        };

		/// <summary>
		/// Removes the carriage return in string.
		/// </summary>
		/// <param name="snapshotData">The snapshot data string.</param>
		/// <returns>The normalized line ending string.</returns>
		private string NormalizeLineEndings(string snapshotData)
		{
			return snapshotData.Replace("\\r", string.Empty)
							   .Replace("\r", string.Empty);
		}
	}
}
