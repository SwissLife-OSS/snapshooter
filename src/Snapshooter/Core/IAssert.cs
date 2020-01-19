namespace Snapshooter.Core
{
    /// <summary>
    /// The assert instance is responsible to compare two strings.
    /// </summary>
    public interface IAssert
    {
        /// <summary>
        /// Compares two snapshot strings. Throws an exception if the strings are not equal.
        /// </summary>
        /// <param name="expectedSnapshot">The expected snapshot.</param>
        /// <param name="actualSnapshot">The actual snapshot.</param>
        void Assert(string expectedSnapshot, string actualSnapshot);
    }
}
