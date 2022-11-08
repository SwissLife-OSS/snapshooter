using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Snapshooter.Tests
{
    public class SnapshooterTests
    {
        [Fact]
        public void AssemblyInfo_SerializeBinaryFieldsAsHashAttribute_Test()
        {
            // arrange
            SerializeBinaryFieldsAsHashAttribute attribute =
                Assembly.GetExecutingAssembly().GetCustomAttribute<SerializeBinaryFieldsAsHashAttribute>();

            // act & assert
            attribute.Value.Should().BeTrue();
        }
    }
}
