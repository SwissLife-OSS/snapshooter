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
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                })
                .Trim('\"');
        }

        /// <summary>
        /// Converts a JToken to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="field">The JToken field to convert.</param>
        /// <returns>The converted value.</returns>
        public static T ConvertToType<T>(this JToken field)
        {
            if (typeof(T) == typeof(int))
            {
                // This is a workaround, because the json method ToObject<> rounds
                // decimal values to integer values, which is wrong.
                return JsonConvert.DeserializeObject<T>(field.Value<string>());
            }

            return field.ToObject<T>();
        }
    }
}
