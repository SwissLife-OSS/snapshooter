using Microsoft.VisualStudio.TestTools.UnitTesting;

// Disable parallel execution because MSTestSnapshotFullNameReader uses a static counter
// to track DataRow index, which doesn't work correctly with parallel execution.
// Each DataRow test must run sequentially to ensure the correct snapshot name is resolved.
[assembly: DoNotParallelize]
