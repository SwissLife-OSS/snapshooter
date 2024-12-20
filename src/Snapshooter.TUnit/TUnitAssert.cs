using Snapshooter.Core;
using TUnit.Assertions.Extensions;
using TAssert = TUnit.Assertions.Assert;

namespace Snapshooter.TUnit
{
    /// <summary>
    /// The NunitAssert instance compares two strings with the TUnit assert utility.
    /// </summary>
    public class TUnitAssert : IAssert
    {
        /// <summary>
        /// Asserts the expected snapshot and the actual snapshot 
        /// with the TUnit assert utility.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        public void Assert(string expectedSnapshot, string actualSnapshot)
        {
            // TUnit assertions use an async syntax but this interface is restricted to synchronous calls
#pragma warning disable TUnitAssertions0002
            TAssert.That(actualSnapshot).IsEqualTo(expectedSnapshot).GetAwaiter().GetResult();
#pragma warning restore TUnitAssertions0002
        }
    }
}
