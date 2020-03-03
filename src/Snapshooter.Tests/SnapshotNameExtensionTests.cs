using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace Snapshooter
{
    public class SnapshotNameExtensionTests
    {
        [Theory]
        [InlineData("en-US")]
        [InlineData("en-GB")]
        [InlineData("de-DE")]
        [InlineData("fr-FR")]
        [InlineData("it-IT")]
        [InlineData("ru-RU")]
        [InlineData("it-CH")]
        [InlineData("de-AT")]
        [InlineData("en-AU")]
        [InlineData("de-LU")]
        [InlineData("en-NZ")]
        [InlineData("es-ES")]
        [InlineData("es-CR")]
        public void ToParamsString_InDifferentCultures_CreatedNameExtensionsSuccessfully(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(new ThreadStart(() =>
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

                var snapshotNameExtension = SnapshotNameExtension.Create(
                "bool", true,
                "char", 'b',
                "byte", (byte)255,
                "sbyte", (sbyte)127,
                "float", 789.6655f,
                "double", 45.987d,
                "decimal", 7834.99000944m,
                "integer", -159485,
                "long", 9223372036854775807,
                "guid", Guid.Parse("{CA9C1C14-839A-4ED2-9763-01FB68FDF49D}"),
                "enum", Importance.Regular,
                "dateTime", DateTime.ParseExact(
                    "12.01.2009 17:05:34", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture),
                "dateTimeOffset", DateTimeOffset.ParseExact(
                    "10/21/2020 18:34 +05:00", "MM/dd/yyyy HH:mm zzz", CultureInfo.CurrentCulture)
                );

                nameExtension = snapshotNameExtension.ToParamsString();
            }));

            // act
            thread.Start();
            thread.Join();
            
            // assert
            Assert.Equal(
                "_bool_True" +
                "_char_b" +
                "_byte_255" +
                "_sbyte_127" +
                "_float_789.6655" +
                "_double_45.987" +
                "_decimal_7834.99000944" +
                "_integer_-159485" +
                "_long_9223372036854775807" +
                "_guid_ca9c1c14-839a-4ed2-9763-01fb68fdf49d" +
                "_enum_Regular" +
                "_dateTime_2009-01-12T17:05:34Z" +
                "_dateTimeOffset_2020-10-21T18:34:00Z",
                nameExtension);
        }
        
        private enum Importance
        {
            Regular
        }
    }
}
