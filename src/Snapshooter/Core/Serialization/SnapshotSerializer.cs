using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snapshooter.Extensions;

namespace Snapshooter.Core.Serialization
{
    /// <summary>
    /// The class <see cref="SnapshotSerializer" /> is responsible to
    /// serialize an object into a snapshot.
    /// </summary>
    public class SnapshotSerializer : ISnapshotSerializer
    {
        /// <summary>
        /// Snapshot json load settings.
        /// </summary>
        private static readonly JsonLoadSettings JsonLoadSettings =
            new JsonLoadSettings
            {
                CommentHandling = CommentHandling.Ignore,
                LineInfoHandling = LineInfoHandling.Ignore,
                DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
            };

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// Constructor of <see cref="SnapshotSerializer"/>
        /// </summary>
        /// <param name="settingsResolver">
        /// The snapshot settings resolver to find all snapshot serialization settings.
        /// </param>
        public SnapshotSerializer(ISnapshotSettingsResolver settingsResolver)
        {
            _jsonSerializerSettings = GetSettings(settingsResolver);
        }

        /// <summary>
        /// Serializes an object to a snapshot string.
        /// </summary>
        /// <param name="objectToSnapshot">The object to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        public string SerializeObject(object objectToSnapshot)
        {
            if (objectToSnapshot is byte[] snapshotByteArray)
            {
                // handle byte arrays
                return snapshotByteArray.ToFormattedHex();
            }

            if (objectToSnapshot is string snapshotScalarString)
            {
                // handle strings separately
                return snapshotScalarString
                    .NormalizeLineEndings()
                    .EnsureLineEnding();
            }
            
            // handle objects
            return SerializeToJson(objectToSnapshot)
                .EnsureLineEnding();
        }

        /// <summary>
        /// Serializes a json token to a snapshot string.
        /// </summary>
        /// <param name="jsonToken">The json token to snapshot.</param>
        /// <returns>The serialized snapshot.</returns>
        public string SerializeJsonToken(JToken jsonToken)
        {
            return jsonToken.ToString(Formatting.Indented)
                .NormalizeLineEndings()
                .EnsureLineEnding();
        }

        /// <summary>
        /// Deserializes a snapshot string to an token object
        /// </summary>
        /// <param name="snapshotJson"></param>
        /// <returns></returns>
        public JToken Deserialize(string snapshotJson)
        {
            JsonLoadSettings jsonLoadSettings = JsonLoadSettings;
            var isValidJson = snapshotJson.IsValidJsonFormat(jsonLoadSettings);

            if (!isValidJson)
            {
                snapshotJson = snapshotJson
                    .NormalizeLineEndings()
                    .EnsureLineEnding();

                snapshotJson = JsonConvert.ToString(snapshotJson);
            }

            var snapshotToken = JToken.Parse(snapshotJson, jsonLoadSettings);

            return snapshotToken;
        }

        /// <summary>
        ///     Serializes the object to json and removes the carriage returns.
        /// </summary>
        /// <param name="value">The object value to serialize.</param>
        /// <returns>The serialized object in json.</returns>
        private string SerializeToJson(object value)
        {
            var jsonSerializer = JsonSerializer.CreateDefault(_jsonSerializerSettings);

            var stringBuilder = new StringBuilder(1024);

            var stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture) 
            {
                NewLine = "\n"
            };

            using var jsonWriter = new JsonTextWriterCrRemove(stringWriter)
            {
                Formatting = jsonSerializer.Formatting
            };

            jsonSerializer.Serialize(jsonWriter, value);

            return stringWriter.ToString();
        }

        private static JsonSerializerSettings GetSettings(
            ISnapshotSettingsResolver snapshotSettingsResolver)
        {
            JsonSerializerSettings jsonSettings = 
                SnapshotSerializerSettings.DefaultJsonSerializerSettings;

            IEnumerable<SnapshotSerializerSettings> extensionTypes =
                snapshotSettingsResolver.GetConfiguration();

            foreach (SnapshotSerializerSettings extensionType in extensionTypes)
            {
                jsonSettings = extensionType.Extend(jsonSettings);
            }

            return jsonSettings;
        }

        /// <summary>
        /// Json text writer, which removes the carriage returns of the string.
        /// </summary>
        private class JsonTextWriterCrRemove : JsonTextWriter
        {
            /// <summary>
            /// Constructor of the <see cref="JsonTextWriterCrRemove" /> class to create
            /// a new instance.
            /// </summary>
            /// <param name="textWriter">The text writer.</param>
            public JsonTextWriterCrRemove(TextWriter textWriter)
                : base(textWriter)
            {
            }

            /// <summary>
            ///     Writes a string value to the json output.
            /// </summary>
            /// <param name="text">The string value.</param>
            public override void WriteValue(string text)
            {
                var normalisedText = text.NormalizeLineEndings();

                base.WriteValue(normalisedText);
            }
        }
    }
}
