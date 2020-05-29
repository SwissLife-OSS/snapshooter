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
        public void ToParamsString_BoolExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;
                        
            var thread = new Thread(() => nameExtension = 
                SnapshotNameExtension.Create("bool", true).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            
            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_bool_True", nameExtension);
        }

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
        public void ToParamsString_CharExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("char", 'b').ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_char_b", nameExtension);
        }

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
        public void ToParamsString_ByteExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension = 
                SnapshotNameExtension.Create("byte", (byte)255).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_byte_255", nameExtension);
        }

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
        public void ToParamsString_FloatExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("float", 789.6655f).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_float_789.6655", nameExtension);
        }

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
        public void ToParamsString_DoubleExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("double", 45.987d).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_double_45.987", nameExtension);
        }

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
        public void ToParamsString_DecimalExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("decimal", 7834.99000944m).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_decimal_7834.99000944", nameExtension);
        }

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
        public void ToParamsString_IntegerExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("integer", -159485).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_integer_-159485", nameExtension);
        }

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
        public void ToParamsString_LongExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension.Create("long", 9223372036854775807).ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_long_9223372036854775807", nameExtension);
        }

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
        public void ToParamsString_GuidExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension
                    .Create("guid", Guid.Parse("{CA9C1C14-839A-4ED2-9763-01FB68FDF49D}"))
                    .ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_guid_ca9c1c14-839a-4ed2-9763-01fb68fdf49d", nameExtension);
        }

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
        public void ToParamsString_EnumExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() =>
                nameExtension = SnapshotNameExtension
                    .Create("enum", Importance.Regular)
                    .ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_enum_Regular", nameExtension);
        }

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
        public void ToParamsString_DateTimeExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension
                    .Create("dateTime", DateTime.ParseExact(
                        "12.01.2009 17:05:34", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture))
                    .ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_dateTime_2009-01-12T16-05-34Z", nameExtension);
        }

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
        public void ToParamsString_DateTimeOffsetExtensionInDifferentCultures_AlwaysRightString(
            string cultureName)
        {
            // arrange
            string nameExtension = string.Empty;

            var thread = new Thread(() => nameExtension =
                SnapshotNameExtension
                    .Create("dateTimeOffset", DateTimeOffset.ParseExact(
                        "10/21/2020 18:34 +05:00", "MM/dd/yyyy HH:mm zzz",
                        CultureInfo.CurrentCulture))
                    .ToParamsString());

            thread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);

            // act
            thread.Start();

            // assert
            thread.Join();

            Assert.Equal("_dateTimeOffset_2020-10-21T13-34-00Z", nameExtension);
        }
        
        private enum Importance
        {
            Regular
        }
    }
}
