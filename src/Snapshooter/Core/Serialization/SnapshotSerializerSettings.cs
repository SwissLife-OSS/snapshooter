using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Snapshooter.Extensions;

namespace Snapshooter.Core.Serialization;

/// <summary>
/// </summary>
public abstract class SnapshotSerializerSettings
{
    public virtual int Order { get; } = 1;

    public virtual bool Active { get; } = true;

    /// <summary>
    /// </summary>
    public static JsonSerializerSettings DefaultJsonSerializerSettings =>
        new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Culture = CultureInfo.InvariantCulture,
            ContractResolver = ChildFirstContractResolver.Instance,
            Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new MemoryStreamJsonConverter(),
                    new StreamJsonConverter()
                }
        };

    public abstract JsonSerializerSettings Extend(JsonSerializerSettings settings);

    private class ChildFirstContractResolver : DefaultContractResolver
    {
        public static ChildFirstContractResolver Instance { get; }
            = new ChildFirstContractResolver();

        protected override IList<JsonProperty> CreateProperties(
            Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties =
                base.CreateProperties(type, memberSerialization);

            if (properties != null)
            {
                properties = properties
                    .OrderBy(p => 1000 - ((Type)p.DeclaringType)
                        .BaseTypesAndSelf()
                        .Count())
                    .ToList();
            }

            return properties;
        }
    }
}
