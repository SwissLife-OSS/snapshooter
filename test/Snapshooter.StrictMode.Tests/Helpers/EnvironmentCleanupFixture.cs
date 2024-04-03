using System;

namespace Snapshooter.StrictMode.Tests.Helpers
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
