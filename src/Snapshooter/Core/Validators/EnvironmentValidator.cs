using System;
using Snapshooter.Exceptions;

namespace Snapshooter.Core.Validators
{
    internal static class EnvironmentValidator
    {
        public static void CheckStrictMode(bool originalSnapshotExists)
        {
            if (!originalSnapshotExists)
            {
                string value = Environment
                    .GetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE");

                if (string.Equals(value, "on", StringComparison.Ordinal)
                    || (bool.TryParse(value, out bool b) && b))
                {
                    throw new SnapshotNotFoundException(
                        "Strict mode is enabled and no snapshot has been found " +
                        "for the current test. Create a new snapshot locally and " +
                        "rerun your tests.");
                }
            }
        }
    }
}
