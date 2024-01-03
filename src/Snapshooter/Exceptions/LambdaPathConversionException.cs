using System;

namespace Snapshooter.Exceptions;

/// <summary>
/// Exception thrown if something went wrong during converting a lambda expression into a path.
/// </summary>
public class LambdaPathConversionException : SnapshotTestException
{
    /// <summary>
    /// Initializes the <see cref="LambdaPathConversionException"/>
    /// </summary>
    public LambdaPathConversionException()
    {
    }

    /// <summary>
    /// Initializes the <see cref="LambdaPathConversionException"/>
    /// <param name="message">The exception message.</param>
    /// </summary>
    public LambdaPathConversionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes the <see cref="LambdaPathConversionException"/>
    /// <param name="message">The exception message.</param>
    /// <param name="inner">The inner exception.</param>
    /// </summary>
    public LambdaPathConversionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
