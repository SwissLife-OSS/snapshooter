using System;
using System.Globalization;
using System.IO;

namespace Snapshooter.Tests.Data
{
    public static class TestDataBuilder
    {
        public static TestPersonBuilder TestPersonMarkWalton()
        {
            return TestPersonBuilder.Create()
                    .WithId(Guid.Parse("C78C698F-9EE5-4B4B-9A0E-EF729B1F8EC8"))
                    .WithFirstname("Mark")
                    .WithLastname("Walton")
                    .WithDateOfBirth(DateTime.ParseExact("25.06.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .WithCreationDate(DateTime.ParseExact("06.06.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .WithAge(30)
                    .WithSize(182.5214m)
                    .AddAddress(TestAddressWallisellen().Build())
                    .AddChild(TestChildJames().Build())
                    .AddChild(TestChildNull().Build())
                    .AddChild(TestChildHanna().Build())
                    .AddRelative(TestPersonSandraSchneider().Build());
        }

        public static TestPersonBuilder TestPersonSandraWalton()
        {
            return TestPersonBuilder.Create()
                .WithId(Guid.Parse("4FA8995F-95FC-4BB6-AD71-46F90D088A37"))
                .WithFirstname("Sandra")
                .WithLastname("Walton")
                .WithDateOfBirth(DateTime.ParseExact("11.02.2002", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .WithCreationDate(DateTime.ParseExact("07.07.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .WithAge(28)
                .WithSize(165.5m)
                .AddAddress(TestAddressWallisellen().Build())
                .AddChild(TestChildJames().Build())
                .AddChild(TestChildNull().Build())
                .AddChild(TestChildHanna().Build())
                .AddRelative(TestPersonSandraSchneider().Build())
                .AddRelative(TestPersonMarkWalton().Build());
        }

        public static TestPersonBuilder TestPersonSandraSchneider()
        {
            return TestPersonBuilder.Create()
                    .WithId(Guid.Parse("FCF04CA6-D8F2-4214-A3FF-D0DED5BAD4DE"))
                    .WithFirstname("Sandra")
                    .WithLastname("Schneider")
                    .WithDateOfBirth(DateTime.ParseExact("14.02.1996", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .WithCreationDate(DateTime.ParseExact("01.04.2019", "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .WithAge(null)
                    .WithSize(165.23m)
                    .AddAddress(TestAddressZurich().Build());
        }

        public static TestAddressBuilder TestAddressWallisellen()
        {
            return TestAddressBuilder.Create()
                    .WithStreet("Rohrstrasse")
                    .WithStreetNumber(12)
                    .WithPlz(8304)
                    .WithCity("Wallislellen")
                    .AddCountry(TestCountrySwitzerland().Build());
        }

        public static TestAddressBuilder TestAddressZurich()
        {
            return TestAddressBuilder.Create()
                    .WithStreet("Bahnhofstrasse")
                    .WithStreetNumber(450)
                    .WithPlz(8000)
                    .WithCity("Zurich")
                    .AddCountry(TestCountrySwitzerland().Build());
        }

        public static TestCountryBuilder TestCountrySwitzerland()
        {
            return TestCountryBuilder.Create()
                .WithName("Switzerland")
                .WithCountryCode(CountryCode.CH);
        }

        public static TestChildBuilder TestChildJames()
        {
            return TestChildBuilder.Create()
                .WithName("James")
                .WithDateOfBirth(DateTime.ParseExact("12.02.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture));
        }
        public static TestChildBuilder TestChildHanna()
        {
            return TestChildBuilder.Create()
                .WithName("Hanna")
                .WithDateOfBirth(DateTime.ParseExact("20.03.2012", "dd.MM.yyyy", CultureInfo.InvariantCulture));
        }

        public static TestChildBuilder TestChildNull()
        {
            return TestChildBuilder.Create()
                .WithName(null)
                .WithDateOfBirth(DateTime.ParseExact("12.02.2015", "dd.MM.yyyy", CultureInfo.InvariantCulture));
        }

        public static TestImageBuilder TestImage()
        {
            return TestImageBuilder.Create()
                .WithName("Foo")
                .WithData(File.ReadAllBytes("./Data/mona-lisa.jpg"));
        }
    }
}
