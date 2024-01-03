using NUnit.Framework;
using Snapshooter.Core;
using NAssert = NUnit.Framework.Assert;

namespace Snapshooter.NUnit
{
    /// <summary>
    /// The NunitAssert instance compares two strings with the NUnit assert utility.
    /// </summary>
    public class NUnitAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot 
        /// with the NUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            try
            {
                NAssert.That(actualSnapshot, Is.EqualTo(expectedSnapshot));
            } catch (AssertionException ex)
            {
                string displayableDiff = DiffGenerator.GenerateDiff(expectedSnapshot, actualSnapshot);

                throw new AssertionException($"Assert.That failed. {displayableDiff}", ex);
            }
        }
    }
}
