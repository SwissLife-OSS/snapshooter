using System;
using System.Collections.Generic;
using System.Linq;

namespace Snapshooter.Core.Serialization
{
    public class GlobalSnapshotSettingsResolver : ISnapshotSettingsResolver
    {
        public IEnumerable<SnapshotSerializerSettings> GetConfiguration()
        {
            Type type = typeof(SnapshotSerializerSettings);

            var typesExtendingSettings = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                    type.IsAssignableFrom(p) && 
                    p.IsClass && 
                    p.GetConstructor(Type.EmptyTypes) != null)
                .ToList();

            var extensionTypes = typesExtendingSettings
                .Select(ext => (SnapshotSerializerSettings)Activator.CreateInstance(ext))
                .Where(ext => ext.Active)
                .OrderBy(ext => ext.Order)
                .ToList();
            return extensionTypes;
        }
    }
}
