using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
                snapshotData = snapshotScalarString
                    .NormalizeLineEndings()
                    .EnsureLineEnding();
            }
            else
            {
                // handle objects                
                snapshotData = SerializeToJson(objectToSnapshot)
                    .EnsureLineEnding();
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
            bool isValidJson = snapshotJson.IsValidJsonFormat();

            if(!isValidJson)
            {
                snapshotJson = JsonConvert.ToString(snapshotJson);
            }

            var snapshotToken = JToken.Parse(snapshotJson, _jsonLoadSettings);

            return snapshotToken;
        }

        /// <summary>
        /// Serializes the object to json and removes the carriage returns.
        /// </summary>
        /// <param name="value">The object value to serialize.</param>
        /// <returns>The serialized object in json.</returns>
        private string SerializeToJson(object value)
        {
            var jsonSerializer = JsonSerializer.CreateDefault(_jsonSerializerSettings);

            var stringBuilder = new StringBuilder(1024);
            var stringWriter = new StringWriter(
                stringBuilder, CultureInfo.InvariantCulture);

            stringWriter.NewLine = "\n";

            using (var jsonWriter = new JsonTextWriterCrRemove(stringWriter))
            {
                jsonWriter.Formatting = jsonSerializer.Formatting;

                jsonSerializer.Serialize(jsonWriter, value);
            }

            return stringWriter.ToString();
        }

        /// <summary>
        /// Snapshot serialization settings.
        /// </summary>
        private static readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Culture = CultureInfo.InvariantCulture,
                ContractResolver = new DefaultContractResolver(),
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
        
        /// <summary>
        /// Json text writer, which removes the carriage returns of the string.
        /// </summary>
        private class JsonTextWriterCrRemove : JsonTextWriter
        {
            /// <summary>
            /// Constructor of the <see cref="JsonTextWriterCrRemove"/> class to create
            /// a new instance.
            /// </summary>
            /// <param name="textWriter">The text writer.</param>
            public JsonTextWriterCrRemove(TextWriter textWriter) 
                : base(textWriter)
            {
            }

            /// <summary>
            /// Writes a string value to the json output.
            /// </summary>
            /// <param name="text">The string value.</param>
            public override void WriteValue(string text)
            {
                string normalisedText = text.NormalizeLineEndings();

                base.WriteValue(normalisedText);
            }
        }
    }
}
