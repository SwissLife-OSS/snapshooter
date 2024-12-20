using System;

#nullable enable

namespace Snapshooter.Xunit3.Tests.AcceptMatchOption.TestHelpers
{
    public class AcceptTypeTesteeBuilder
    {
        public static AcceptTypeTestee<T> CreateAcceptTypeDefaultTestee<T>(T typeValue)
        {
            var acceptTypeTestee = new AcceptTypeTestee<T>
            {
                Id = Guid.Parse("B9E62A8B-832C-416E-A8E2-6DA2B4412012"),
                Value = typeValue,
                Copy = null
            };

            return new AcceptTypeTestee<T>
            {
                Id = acceptTypeTestee.Id,
                Value = acceptTypeTestee.Value,
                Copy = acceptTypeTestee
            };
        }
    }
}
