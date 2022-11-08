using System;
using Newtonsoft.Json.Linq;

namespace Snapshooter.Environment.Tests.Helpers
{
    public class EnvironmentCleanupFixture : IDisposable
    {
        public EnvironmentCleanupFixture()
        {
        }

        public void Dispose()
        {
            System.Environment.SetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE", false.ToString());
        }
    }
}
