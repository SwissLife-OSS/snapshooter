using System;
using Snapshooter.Exceptions;

namespace Snapshooter.Core.Validators
{
    internal static class StrictModeValidator
    {
        public static void CheckStrictMode(bool originalSnapshotExists, MatchOptions matchOptions)
        {
            if (!originalSnapshotExists)
            {
                if (IsStrictModeEnvironmentVariableActive() || matchOptions.UseStrictMode)
                {
                    throw new SnapshotNotFoundException(
                        "Strict mode is enabled and no snapshot has been found " +
                        "for the current test. Create a new snapshot locally and " +
                        "rerun your tests.");
                }
            }
        }

        private static bool IsStrictModeEnvironmentVariableActive()
        {
            var environmentVariableValue = Environment.GetEnvironmentVariable("SNAPSHOOTER_STRICT_MODE");
            return string.Equals(environmentVariableValue, "on", StringComparison.Ordinal) || (bool.TryParse(environmentVariableValue, out bool b) && b);
        }
    }
}
