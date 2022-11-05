using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Snapshooter.Extensions
{
    /// <summary>
    /// JToken Extensions for Snapshooter usage.
    /// </summary>
    public static class JTokenExtensions
    {
        /// <summary>
        /// Converts a JToken to a normalised value string
        /// for snapshooter usage.
        /// </summary>
        /// <param name="jtoken">The JToken to convert.</param>
        /// <returns>The normalised value string.</returns>
        public static string ConvertToValueString(this JToken jtoken)
        {
            return JsonConvert
                .SerializeObject(jtoken, Formatting.None, new JsonSerializerSettings
                {
                    DateFormatString = Wellknown.DateTimeISO8601Format,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                })
                .Trim('\"');
        }
    }
}
