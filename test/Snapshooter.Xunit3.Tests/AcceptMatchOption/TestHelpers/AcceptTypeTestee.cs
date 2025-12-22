using System;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers
{

    public class AcceptTypeTestee<T>
    {
        public Guid Id { get; set; }

        public T? Value { get; set; }

        public AcceptTypeTestee<T>? Copy { get; set; }
    }

    public enum AcceptEnumTestee
    {
        START,
        STOP
    }
}
