using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snapshooter.Core;
using MsTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Snapshooter.MSTest
{
    /// <summary>
    /// The MSTestAssert instance compares two strings with the MSTest assert utility.
    /// </summary>
    public class MSTestAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot 
        /// with the MSTest assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            try
            {
                MsTest.Assert.AreEqual(expectedSnapshot, actualSnapshot);
            }
            catch (AssertFailedException ex)
            {
                string displayableDiff = DiffGenerator.GenerateDiff(expectedSnapshot, actualSnapshot);

                throw new AssertFailedException($"Assert.AreEqual failed. {displayableDiff}", ex);
            }
        }
    }
}
