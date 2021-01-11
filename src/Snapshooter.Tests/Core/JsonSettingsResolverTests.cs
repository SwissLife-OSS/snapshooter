using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using Snapshooter.Core.Serialization;
using Xunit;

namespace Snapshooter.Tests.Core
{
    public class JsonSettingsResolverTests
    {
        [Fact]
        public void GlobalResolvers_Finds_All_Active_Extensions_WithDefault_Constructors()
        {
            var sut = new GlobalSnapshotSettingsResolver();

            IEnumerable<SnapshotSerializerSettings> resolvedSettings = sut.GetConfiguration();

            resolvedSettings.Should().HaveCount(3)
                .And.ContainSingle(conf => conf is JsonSerializerTests.DefaultSnapshotSerializerSettings)
                .And.ContainSingle(conf => conf is JsonSerializerTests.WithoutEnumConversion)
                .And.ContainSingle(conf => conf is ComesLast);
        }

        public class NotActive : SnapshotSerializerSettings
        {
            public override bool Active => false;

            public override JsonSerializerSettings Extend(JsonSerializerSettings settings)
            {
                return settings;
            }
        }

        public class NoDefaultConstructor : SnapshotSerializerSettings
        {
            // ReSharper disable once UnusedParameter.Local
            public NoDefaultConstructor(int notBeingPassedIn)
            {
            }

            public override JsonSerializerSettings Extend(JsonSerializerSettings settings)
            {
                return settings;
            }
        }

        private class ComesLast : SnapshotSerializerSettings
        {
            public override int Order => int.MaxValue;

            public override JsonSerializerSettings Extend(JsonSerializerSettings settings)
            {
                return settings;
            }
        }
    }
}
