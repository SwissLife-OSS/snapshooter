using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using Snapshooter.Core.Serialization;
using Xunit;

namespace Snapshooter.Tests.Core
{
    public class JsonSerializerTests
    {
        [Fact]
        public void Default_Serializer_EnumAsStrings()
        {
            var sut = new SnapshotSerializer(new DefaultSnapshotSerializerSettings());

            var myState = new {MyProp = MyEnum.FirstValue, OtherProp = MyEnum.LastValue, MyString = "String"};
            var snapEnumsAsStrings = sut.SerializeObject(myState);

            snapEnumsAsStrings.Should().Be("{\n  \"MyProp\": \"FirstValue\",\n  \"OtherProp\": \"LastValue\",\n  \"MyString\": \"String\"\n}\n");
        }

        [Fact]
        public void Customized_SerializerSettings_WithoutConverter()
        {
            var sut = new SnapshotSerializer(new WithoutEnumConversion());

            var myState = new {MyProp = MyEnum.FirstValue, OtherProp = MyEnum.LastValue, MyString = "String"};
            var snapEnumsAsIntegers = sut.SerializeObject(myState);

            snapEnumsAsIntegers.Should().Be("{\n  \"MyProp\": 1,\n  \"OtherProp\": 99,\n  \"MyString\": \"String\"\n}\n");
        }

        private enum MyEnum
        {
            FirstValue = 1,
            SecondValue = 2,
            LastValue = 99
        }

        internal class DefaultSnapshotSerializerSettings : SnapshotSerializerSettings, ISnapshotSettingsResolver
        {
            public override bool Active => true;

            public IEnumerable<SnapshotSerializerSettings> GetConfiguration()
            {
                return new[] {new DefaultSnapshotSerializerSettings()};
            }

            public override JsonSerializerSettings Extend(JsonSerializerSettings settings)
            {
                return settings;
            }
        }

        internal class WithoutEnumConversion : SnapshotSerializerSettings, ISnapshotSettingsResolver
        {
            public IEnumerable<SnapshotSerializerSettings> GetConfiguration()
            {
                return new[] {new WithoutEnumConversion()};
            }

            public override JsonSerializerSettings Extend(JsonSerializerSettings settings)
            {
                // remove enum to string converter
                settings.Converters = new List<JsonConverter>();
                return settings;
            }
        }
    }
}
