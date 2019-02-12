using System;
using System.IO;
using Snapshooter.Core;
using Snapshooter.Exceptions;

namespace Snapshooter
{
    /// <summary>
    /// The class <see cref="SnapshotAssert"/> can be used to compare a given object 
    /// against a snapshot. If no snapshot exists, a new snapshot will be created from
    /// the current object and saved under a certain file path, which will shown in the
    /// Assert exception.
    /// </summary>
    public class SnapshotAssert : ISnapshotAssert
    {
        private readonly ISnapshotSerializer _snapshotSerializer;
        private readonly ISnapshotFileInfoResolver _snapshotFileInfoResolver;
        private readonly ISnapshotFileHandler _snapshotFileHandler;
        private readonly ISnapshotComparer _snapshotComparer;

        public SnapshotAssert(ISnapshotSerializer snapshotSerializer,
                              ISnapshotFileInfoResolver snapshotFileInfoResolver,
                              ISnapshotFileHandler snapshotFileHandler,
							  ISnapshotComparer snapshotComparer)
        {
            if (snapshotSerializer == null)
            {
                throw new ArgumentNullException(nameof(snapshotSerializer));
            }

            if (snapshotFileInfoResolver == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileInfoResolver));
            }

            if (snapshotFileHandler == null)
            {
                throw new ArgumentNullException(nameof(snapshotFileHandler));
            }

            if (snapshotComparer == null)
            {
                throw new ArgumentNullException(nameof(snapshotComparer));
            }

            _snapshotSerializer = snapshotSerializer;
            _snapshotFileInfoResolver = snapshotFileInfoResolver;
            _snapshotFileHandler = snapshotFileHandler;
            _snapshotComparer = snapshotComparer;
        }

        /// <summary>
        /// Compares the snapshot against the given result object.
        /// </summary>
        /// <param name="currentResult">
        /// The object to compare.
        /// </param>
        /// <param name="snapshotFileInfo">
        /// The file infos of the snapshot.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional match actions, which can be applied during the comparison
        /// </param>
        public void AssertSnapshot(
			object currentResult, 
			string snapshotName = null, 
			SnapshotNameExtension snapshotNameExtension = null,
			Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            if (currentResult == null)
            {
                throw new ArgumentNullException(nameof(currentResult));
            }                
            
            ISnapshotFileInfo snapshotFileInfo = _snapshotFileInfoResolver
                .ResolveSnapshotFileInfo(snapshotName, snapshotNameExtension?.ToParamsString());

            CleanupSnapshotFiles(snapshotFileInfo);

            string actualSnapshotSerialized = _snapshotSerializer.Serialize(currentResult);
            string savedSnapshotSerialized = _snapshotFileHandler.LoadSnapshot(snapshotFileInfo);

            if (savedSnapshotSerialized == null)
            {
                SaveNewSnapshot(snapshotFileInfo, actualSnapshotSerialized);
            }

            CompareSnapshots(actualSnapshotSerialized, savedSnapshotSerialized,
                snapshotFileInfo, matchOptions);

            CleanupEmptySubfolders(snapshotFileInfo);
        }

        private void CompareSnapshots(
			string actualSnapshotSerialized,
            string savedSnapshotSerialized,
            ISnapshotFileInfo snapshotFileInfo,
            Func<MatchOptions, MatchOptions> matchOptions)
        {
            try
            {
                _snapshotComparer.CompareSnapshots(
                    savedSnapshotSerialized, actualSnapshotSerialized, matchOptions);
            }
            catch (Exception)
            {
                SaveMismatchSnapshot(snapshotFileInfo, actualSnapshotSerialized);
                throw;
            }
        }
		
        private void CleanupSnapshotFiles(ISnapshotFileInfo snapshotFileInfo)
        {
            foreach (SnapshotSubfolder item in Enum.GetValues(typeof(SnapshotSubfolder)))
            {
                _snapshotFileHandler.DeleteSnapshotSubfolderFile(snapshotFileInfo, item);
            }
        }

        private void CleanupEmptySubfolders(ISnapshotFileInfo snapshotFileInfo)
        {
            foreach (SnapshotSubfolder item in Enum.GetValues(typeof(SnapshotSubfolder)))
            {
                _snapshotFileHandler.DeleteEmptySnapshotSubfolder(snapshotFileInfo, item);
            }            
        }

        private void SaveMismatchSnapshot(
			ISnapshotFileInfo snapshotFileInfo, string actualSnapshotSerialized)
        {
            _snapshotFileHandler.SaveSnapshot(snapshotFileInfo,
                                SnapshotSubfolder.Mismatch, actualSnapshotSerialized);
        }
		
        private void SaveNewSnapshot(
            ISnapshotFileInfo snapshotFileInfo, string actualSnapshotSerialized)
        {            
            string savedSnapshotFilename = _snapshotFileHandler
				.SaveSnapshot(snapshotFileInfo, SnapshotSubfolder.New, actualSnapshotSerialized);
			            
            throw new SnapshotTestException(
				$"The expected snapshot does not exist for " +
                $"snapshot test: '{Path.GetFileNameWithoutExtension(snapshotFileInfo.Filename)}'. " +
                $"A new snapshot has been created and saved under the " +
                $"following path {new Uri(Path.GetDirectoryName(savedSnapshotFilename))}. " +
                $"File: {snapshotFileInfo.Filename}");            
        }        
    }
}
