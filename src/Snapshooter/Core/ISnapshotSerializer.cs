using Newtonsoft.Json.Linq;

namespace Snapshooter.Core
{
    /// <summary>
    /// The class <see cref="ISnapshotSerializer"/> is responsible to
    /// serialize an object into a snapshot and deserialize it.
    /// </summary>
    public interface ISnapshotSerializer
    {
        /// <summary>
        /// Serializes an object to a snapshot string.
        /// </summary>
        /// <param name="objectToSnapshot">The object to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        string SerializeObject(object objectToSnapshot);

        /// <summary>
        /// Serializes a json token to a snapshot string.
        /// </summary>
        /// <param name="jsonToken">The json token to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        string SerializeJsonToken(JToken jsonToken);

        /// <summary>
        /// Deserializes a snapshot string to a json token
        /// </summary>
        /// <param name="snapshotJson"></param>
        /// <returns></returns>
        JToken Deserialize(string snapshotJson);
    }
}
