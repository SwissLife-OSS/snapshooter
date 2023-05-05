using Snapshooter.Core;
using Xunit.Sdk;
using x = Xunit;

namespace Snapshooter.Xunit
{
    /// <summary>
    /// The XunitAssert instance compares two strings with the XUnit assert utility.
    /// </summary>
    public class XunitAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot 
        /// with the XUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            try
            {
                x.Assert.Equal(expectedSnapshot, actualSnapshot);
            }
            catch (EqualException ex)
            {
                string displayableDiff = DiffGenerator.GenerateDiff(expectedSnapshot, actualSnapshot);
                throw new EqualException($"Assert.Equal failed. {displayableDiff}", ex);
            }
        }
    }
}
