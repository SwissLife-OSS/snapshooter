namespace Snapshooter.Core
{
    /// <summary>
    /// The interface <see cref="ISnapshotFullNameBuilder"/> is responsible to resolve the name of 
    /// the test, in which the class instance is currently running. It finds the current running 
    /// test and constructs the name by class + method + parameters.
    /// </summary>
    public interface ISnapshotFullNameBuilder
    {
        /// <summary>
        /// This method resolve the name of the test, in which the method was called.
        /// The name of the test will be constructed from the test class name + 
        /// test method name + (optional) test parameters.
        /// </summary>       
        /// <param name="snapshotName">The name of the snapshot.</param>
        /// <returns>The test name. Null if no test could be found.</returns>
        string BuildSnapshotFullName(string snapshotName);

        string BuildSnapshotFullName(string snapshotName, string snapshotNameExtension);
    }
}