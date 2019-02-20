using System;
using Snapshooter.Core;

namespace Snapshooter.Xunit
{
    /// <summary>
    /// The snapshot class creates and compares snapshots of object.
    /// It creates a json snapshot of the given object and compares it with the 
    /// already existing snapshot of the test. If no snapshot exists already for this
    /// test, then a new snapshot will be created from the current result and saved
    /// in the folder __snapshots__ next to the executing test class file.
    /// </summary>
    public static class Snapshot
    {
        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match<T>(T currentResult,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, null, null, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match<T>(T currentResult,
            SnapshotNameExtension snapshotNameExtension,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, null, snapshotNameExtension, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically from the xunit test name.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match<T>(T currentResult,
            string snapshotName,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, null, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically from the xunit test name.
        /// </param> 
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match<T>(T currentResult,
            string snapshotName,
            SnapshotNameExtension snapshotNameExtension,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <param name="currentResult">The object to match.</param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match(object currentResult,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, null, null, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match(object currentResult,
            SnapshotNameExtension snapshotNameExtension,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, null, snapshotNameExtension, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically from the xunit test name.
        /// </param>  
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison
        /// </param>
        public static void Match(object currentResult,
            string snapshotName,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, null, matchOptions);
        }

        /// <summary>        
        /// Creates a json snapshot of the given object and compares it with the 
        /// already existing snapshot of the test. 
        /// If no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown within the test message.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically from the xunit test name.
        /// </param> 
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the snapshot comparison.
        /// </param>
        public static void Match(object currentResult,
            string snapshotName,
            SnapshotNameExtension snapshotNameExtension,
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }

        private static void AssertSnapshot(
            object currentResult, 
            string snapshotName = null, 
            SnapshotNameExtension snapshotNameExtension = null, 
            Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            if (currentResult == null)
            {
                throw new ArgumentNullException(nameof(currentResult));
            }

            ISnapshotAssert snapshotAssert = CreateSnapshotAssert();

            snapshotAssert.AssertSnapshot(
                currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }

        private static ISnapshotAssert CreateSnapshotAssert()
        {
            return new SnapshotAssert(
                new SnapshotSerializer(),
                new SnapshotFileInfoResolver(
                    new XunitSnapshotFileInfoReader(),
                    new SnapshotFileNameBuilder()),
                new SnapshotFileHandler(), 
                new SnapshotEnvironmentCleaner(
                    new SnapshotFileHandler()),
                new JsonSnapshotComparer(
                    new XunitAssert(), 
                    new SnapshotSerializer()));
        }
    }
}
