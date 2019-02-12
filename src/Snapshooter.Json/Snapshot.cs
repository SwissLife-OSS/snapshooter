using System;
using Snapshooter.Core;

namespace Snapshooter.Json
{
    public class Snapshot
    {
        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname will be evaluated automatically.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match<T>(T currentResult,
                                    string snapshotName,
                                    Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, null, matchOptions);
        }

        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        ///  /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
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
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match<T>(T currentResult,
                                    string snapshotName,
                                    SnapshotNameExtension snapshotNameExtension,
                                    Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }
		        
        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match(object currentResult,
                                 string snapshotName,
                                 Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            AssertSnapshot(currentResult, snapshotName, null, matchOptions);
        }

        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        ///  /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
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
        /// Additional compare actions, which can be applied during the comparison
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
			string snapshotName, 
			SnapshotNameExtension snapshotNameExtension = null, 
			Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            if (currentResult == null)
            {
                throw new ArgumentNullException(nameof(currentResult));
            }
			            
            if (string.IsNullOrEmpty(snapshotName))
            {
                throw new ArgumentException($"{nameof(snapshotName)} cannot be null or empty");
            }
			            
            ISnapshotAssert snapshotAssert = CreateSnapshotAssert();

            snapshotAssert.AssertSnapshot(
                currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }

        private static ISnapshotAssert CreateSnapshotAssert()
        {
            return new SnapshotAssert(
                new JsonSnapshotSerializer(),
                new SnapshotFileInfoResolver(
					new JsonSnapshotFileInfoReader(),
					new SnapshotFileNameBuilder()),
                new SnapshotFileHandler(),
                new JsonSnapshotComparer(new JsonAssert()));
        }
    }
}
