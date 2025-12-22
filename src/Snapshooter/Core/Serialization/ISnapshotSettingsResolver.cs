using System.Collections.Generic;

namespace Snapshooter.Core.Serialization
{
    public interface ISnapshotSettingsResolver
    {
        IEnumerable<SnapshotSerializerSettings> GetConfiguration();
    }
}
